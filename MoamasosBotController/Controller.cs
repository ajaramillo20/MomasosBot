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

        private async static void MainBot_OnInlineQuery(object sender, InlineQueryEventArgs e)
        {
            try
            {
                await AnswerInlineQueryPagination(e);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR INLINE-QUERY");
                Console.WriteLine($"Metodo: {ex.TargetSite}");
                Console.WriteLine($"Linea: {ex.Source}");
                Console.WriteLine($"Mensaje: {ex.Message}");
            }
        }
       
        private static Task AnswerInlineQueryPagination(InlineQueryEventArgs e)
        {
            return Task.Run(async () =>
            {
                int resultsPerRequest = 50;

                List<string> imageIdList = CloudinaryApp.CloudController.GetImagesIdByQuery(e.InlineQuery.Query).OrderBy(r=> r).ToList();

                var offset = string.IsNullOrEmpty(e.InlineQuery.Offset) ? "0" : e.InlineQuery.Offset;                

                var resultsToSend = new List<InlineQueryResultPhoto>();                
                var paginacion = await CloudinaryApp.CloudController.GetImagesByPublicIdsAsync(imageIdList.Skip(int.Parse(offset)).Take(resultsPerRequest).ToList());
                
                paginacion.ForEach(result => {                    
                    resultsToSend.Add(new InlineQueryResultPhoto(result.PublicId, result.Uri.AbsoluteUri, result.Uri.AbsoluteUri));
                });

                var nextOffset = (int.Parse(offset)+ resultsPerRequest) > imageIdList.Count ? "" : (int.Parse(offset) + (int)resultsPerRequest).ToString();
                
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
