using KKLauncher.Bot.Constants;
using KKLauncher.Bot.EF;
using KKLauncher.Bot.Utils;
using KKLauncher.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace KKLauncher.Bot.Commands.CallbackCommands
{
    public class PCsListCallbackCommand : ICallbackCommand
    {
        public async Task ExecuteAsync(ITelegramBotClient kkBot, JObject callbackObj, long chatId, int messageId)
        {
            var currentPage = callbackObj[CallbackConstants.Keys.Page]?.Value<int>();
            if (currentPage == null)
            {
                return;
            }

            List<PCEntity>? pcs = null;

            var haveBack = currentPage.Value > 0;
            var haveNext = false;

            using (KKBotDbContext db = new KKBotDbContext())
            {
                var skip = DbQueryConstants.PCTake * currentPage.Value;
                pcs = db.PCs
                    .AsNoTracking()
                    .Skip(skip)
                    .Take(DbQueryConstants.PCTake)
                    .ToList();

                haveNext = pcs.Count == DbQueryConstants.PCTake ?
                    await db.PCs.CountAsync() - skip - pcs.Count > 0 :
                    false;
            }

            var menu = new List<List<InlineKeyboardButton>>();
            var menuRow = new List<InlineKeyboardButton>();
            var itemsInRow = 0;

            foreach (var pc in pcs)
            {
                if (itemsInRow == 2)
                {
                    menu.Add(menuRow);
                    itemsInRow = 0;

                    menuRow = new List<InlineKeyboardButton>();
                }

                menuRow.Add(
                    InlineKeyboardButton.WithCallbackData(
                        text: pc.Name,
                        callbackData: JsonConvert.SerializeObject(
                            CallbackCreatingHelper.CreateSelectCallbackData(CallbackConstants.PC.PCKey, pc.Id)))
                );

                itemsInRow++;
            }

            if (menuRow.Count > 0)
            {
                menu.Add(menuRow);
            }

            var pagingMenu = new List<InlineKeyboardButton>();

            if (haveBack)
            {
                pagingMenu.Add(
                    InlineKeyboardButton.WithCallbackData(
                        text: CallbackConstants.Page.Back,
                        callbackData: JsonConvert.SerializeObject(
                            CallbackCreatingHelper.CreatePageCallbackData(CallbackConstants.PC.PCKey, currentPage.Value - 1)))
                );
            }

            if (haveNext)
            {
                pagingMenu.Add(
                    InlineKeyboardButton.WithCallbackData(
                        text: CallbackConstants.Page.Next,
                        callbackData: JsonConvert.SerializeObject(
                            CallbackCreatingHelper.CreatePageCallbackData(CallbackConstants.PC.PCKey, currentPage.Value + 1)))
                );
            }

            if (pagingMenu.Any())
            {
                menu.Add(pagingMenu);
            }

            await kkBot.EditMessageReplyMarkupAsync(
                chatId: chatId,
                messageId: messageId,
                replyMarkup: new InlineKeyboardMarkup(menu));
        }
    }
}
