using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CloudinaryApp
{
    public static class CloudController
    {
        private static Cloudinary Api;

        public static void Iniciar(string cloud, string apiKey, string apiSecert)
        {            
            Api = new Cloudinary(new Account()
            {
                Cloud = cloud,
                ApiKey = apiKey,
                ApiSecret = apiSecert
            });
        }

        public async static Task<ImageUploadResult> UploadImageAsync(string path)
        {
            FileInfo fileinfo = new FileInfo(path);
            var nombreArchivo = Path.GetFileNameWithoutExtension(path);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(path),
                PublicId = nombreArchivo,
                UseFilename = true,
                Tags = "momasosBot"
            };

            var para = new RawUploadParams();

            return await Api.UploadAsync(uploadParams);
        }

        public static string GetThumbnailAsync(string publicId, int height = 100, int width = 150)
        {
            var url = $"http://res.cloudinary.com/momasosbot/image/upload/t_media_lib_thumb/{publicId}.jpg";
            return url;
        }
       
        public static List<string> GetImagesIdByQuery(string query)
        {
            string expression = string.IsNullOrEmpty(query) || query == "//" ? "" : $"public_id LIKE {query}";
            List<SearchResource> result = Api.Search().Expression(expression).MaxResults(5000).Execute().Resources.ToList();
            var publicIds = new List<string>();

            result.ForEach(r => { publicIds.Add(r.PublicId); });

            return publicIds;
        }

        public static async Task<List<string>> GetImagesIdByQueryAsync(string query)
        {
            return await Task.Run(() => 
            {
                string expression = string.IsNullOrEmpty(query) || query == "//" ? "" : $"public_id LIKE {query}";
                List<SearchResource> result = Api.Search().Expression(expression).MaxResults(5000).Execute().Resources.ToList();
                var publicIds = new List<string>();

                result.ForEach(r => { publicIds.Add(r.PublicId); });

                return publicIds;
            });            
        }

        public static List<Resource> GetImagesByPublicIds(List<string> publicIds)
        {
            List<Resource> result = new List<Resource>();
            result = Api.ListResourceByPublicIds(publicIds, false, false, false).Resources.ToList();            
            return result;
        }

        public static async Task<List<Resource>> GetImagesByPublicIdsAsync(List<string> publicIds)
        {
            return await Task.Run(() =>
            {
                List<Resource> result = new List<Resource>();
                result = Api.ListResourceByPublicIds(publicIds, false, false, false).Resources.ToList();
                return result;
            });            
        }
        
        public static GetResourceResult GetImagesByPublicId(string publicId)
        {
            var result = Api.GetResource(publicId);
            return result;
        }        
    }
}
