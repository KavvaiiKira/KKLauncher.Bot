using KKLauncher.Bot.EF;
using KKLauncher.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

namespace KKLauncher.Bot.Commands.MenuCommands
{
    public class PCsListCommand : IMenuCommand
    {
        public async Task ExecuteAsync(ITelegramBotClient kkBot, long chatId)
        {
            List<PCEntity>? pcs = null;

            using (KKBotDbContext db = new KKBotDbContext())
            {
                pcs = db.PCs.AsNoTracking().ToList();
            }

            if (pcs == null || !pcs.Any())
            {
                await kkBot.SendTextMessageAsync(chatId: chatId, text: "No PCs added.");

                return;
            }
        }
    }
}
