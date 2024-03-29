﻿using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MoamasosBotController
{
    public static class TelegramUtilities
    {
        private static TelegramBotClient MainBot = new TelegramBotClient(AppController.Config.TelegramKey)
        {
            IsReceiving = false,
            
        };
       
        internal async static void SendExceptionAdmin(Exception ex)
        {
            try
            {
                await MainBot.SendTextMessageAsync(AppController.Config.AdminId, $"ERROR\nMensaje: {ex.Message}\nObjeto: {ex.Source}\nMetodo: {ex.TargetSite}",disableNotification:true);
            }
            catch (Exception)
            {
                //ignored                
            }
        }
        
        internal static async Task DownloadTelegramFile(string fileId, string path)
        {
            try
            {
                Telegram.Bot.Types.File file = await MainBot.GetFileAsync(fileId);

                using (var saveImageStream = new FileStream(path, FileMode.Create))
                {
                    await MainBot.GetInfoAndDownloadFileAsync(fileId, saveImageStream);
                }
            }
            catch (Exception ex)
            {
                SendExceptionAdmin(ex);
            }
        }

        internal static async Task UploadMomasoFromTelegram(MessageEventArgs e)
        {
            var idfoto = e.Message.Photo.MinBy(m => m.FileSize).First().FileId;
            
            if (await ValidacionesTelegram(e))
            {                
                var nombre = e.Message.Caption;
                var path = $@"{AppController.Config.RutaDescargas}\{nombre}.{AppController.Config.Extension}";
                await DownloadTelegramFile(idfoto, path);                  
                var result = await CloudinaryApp.CloudController.UploadImageAsync(path);                
                await MainBot.DeleteMessageAsync(e.Message.From.Id, e.Message.MessageId);
            }                     
        }
        
        private static async Task<bool> ValidacionesTelegram(MessageEventArgs e)
        {            
            if (string.IsNullOrEmpty(e?.Message?.Caption))
            {
                await MainBot.SendTextMessageAsync(e.Message.From.Id, "Para subir tu momaso, ingresa un nombre en el comentario de tu foto e inténtalo de nuevo 😎");
                return false;
            }
            if (e?.Message?.Caption?.Length > AppController.Config.LimiteCaracteres)
            {
                await MainBot.SendTextMessageAsync(e.Message.From.Id, $"Para subir tu momaso, no puedes exceder los {AppController.Config.LimiteCaracteres} caracteres. Caracteres actuales: {e.Message.Caption.Length} 😎");
                return false;
            }
            return true;
        }
    }
}
