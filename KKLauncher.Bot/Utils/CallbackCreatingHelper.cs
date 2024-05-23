using KKLauncher.Bot.Constants;
using Newtonsoft.Json.Linq;

namespace KKLauncher.Bot.Utils
{
    public class CallbackCreatingHelper
    {
        public static JObject CreateSelectCallbackData(string callbackType, Guid itemId)
        {
            return new JObject()
            {
                [CallbackConstants.Keys.Key] = callbackType,
                [CallbackConstants.Keys.PCSelect] = itemId.ToString()
            };
        }
        public static JObject CreatePageCallbackData(string callbackType, int page)
        {
            return new JObject()
            {
                [CallbackConstants.Keys.Key] = callbackType,
                [CallbackConstants.Keys.Page] = page.ToString(),
            };
        }
    }
}
