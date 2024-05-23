using KKLauncher.Bot.Constants;
using KKLauncher.Bot.EF;
using KKLauncher.Bot.Utils;
using KKLauncher.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace KKLauncher.Bot.Commands.MenuCommands
{
    public class PCsListCommand : IMenuCommand
    {
        public async Task ExecuteAsync(ITelegramBotClient kkBot, long chatId)
        {
            List<PCEntity>? pcs = null;
            var isPaging = false;

            using (KKBotDbContext db = new KKBotDbContext())
            {
                isPaging = await db.PCs.CountAsync() > DbQueryConstants.PCTake;
                pcs = db.PCs.AsNoTracking().Take(DbQueryConstants.PCTake).ToList();
            }

            if (pcs == null || !pcs.Any())
            {
                await kkBot.SendTextMessageAsync(chatId: chatId, text: "No PCs added.");

                return;
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

            if (isPaging)
            {
                menu.Add(new List<InlineKeyboardButton>()
                {
                    InlineKeyboardButton.WithCallbackData(
                        text: CallbackConstants.Page.Next,
                        callbackData: JsonConvert.SerializeObject(
                            CallbackCreatingHelper.CreatePageCallbackData(CallbackConstants.PC.PCKey, 1)))
                });
            }

            await kkBot.SendTextMessageAsync(
                chatId,
                text: CallbackConstants.MessageText.PCList,
                replyMarkup: new InlineKeyboardMarkup(menu));
        }
    }
}
