using MoamasosBotController;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InlineQueryResults;


namespace MomasosBotController
{
    public class Controller
    {
        private static TelegramBotClient MainBot = new TelegramBotClient(AppController.Config.TelegramKey);

        public static void Iniciar()
        {
            IniciarEventos();
            MainBot.StartReceiving();
        }

        private static void IniciarEventos()
        {
            MainBot.OnMessage += MainBot_OnMessage;
            MainBot.OnInlineQuery += MainBot_OnInlineQuery;            
        }       

        private async static void MainBot_OnInlineQuery(object sender, Telegram.Bot.Args.InlineQueryEventArgs e)
        {
            try
            {
                await AnswerInlineQueryPagination(e);                
            }
            catch (Exception)
            {
                //ignored               
            }
        }

        private static Task AnswerInlineQueryPagination(InlineQueryEventArgs e)
        {
            return Task.Run(async () =>
            {
                var resultsPerRequest = 50;

                List<CloudinaryDotNet.Actions.Resource> results = CloudinaryApp.CloudController.GetImagesByQuery(e.InlineQuery.Query);

                var offset = string.IsNullOrEmpty(e.InlineQuery.Offset) ? "0" : e.InlineQuery.Offset;

                //int.Parse(e.InlineQuery.Offset ?? "0");

                var resultsToSend = new List<InlineQueryResultPhoto>();

                results.Skip(int.Parse(offset)).Take(resultsPerRequest).ToList().ForEach(result =>
                {
                    resultsToSend.Add(new InlineQueryResultPhoto(result.PublicId, result.Uri.AbsoluteUri, result.Uri.AbsoluteUri));
                });

                var nextOffset = int.Parse(offset) + resultsPerRequest > results.Count ? "" : (offset + resultsPerRequest).ToString();

                await MainBot.AnswerInlineQueryAsync(e.InlineQuery.Id, resultsToSend.ToArray(), null, false, nextOffset);
            });

        }

        private async static void MainBot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {
                switch (e.Message.Type)
                {
                    case Telegram.Bot.Types.Enums.MessageType.Photo:
                        await TelegramUtilities.UploadMomasoFromTelegram(e);
                        break;
                }
            }
            catch (Exception ex)
            {
                TelegramUtilities.SendExceptionAdmin(ex);
            }
        }


    }
}
