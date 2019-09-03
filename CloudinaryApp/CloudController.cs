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
        private static Cloudinary Api = new Cloudinary
        (
            new Account(
                        AppController.Config.Cloud,
                        AppController.Config.ApiKey,
                        AppController.Config.ApiSecret
                        )
         );

        public async static Task<ImageUploadResult> UploadImageAsync(string path)
        {
            FileInfo fileinfo = new FileInfo(path);
            var nombreArchivo = Path.GetFileNameWithoutExtension(path);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(path),
                PublicId = nombreArchivo,
                UseFilename = true,
                Tags = "momos"
            };

            var para = new RawUploadParams();

            return await Api.UploadAsync(uploadParams);
        }

        //public static string GetThumbnailAsync(string publicId, int height = 100, int width = 150)
        //{
        //    //var transformation = new Transformation().Height(height).Width(width).Crop("limit");
        //    //var url =Api.Api.UrlImgUp.Transform(transformation).BuildImageTag($"{publicId}.jpg");
        //    var url = $"http://res.cloudinary.com/momasosbot/image/upload/t_media_lib_thumb/{publicId}.jpg";
        //    return url;
        //}

        public static List<Resource> GetImagesByQuery(string query)
        {
            //if (query=="/" || query =="//")
            //{
            //    var param = new ListResourcesParams();
            //    param.MaxResults = 1000;                
            //    return Api.ListResources(param).Resources.ToList();
            //}
            //else
            //{
            //string expression = string.IsNullOrEmpty(query)?"":$"public_id LIKE {query}";
            string expression = string.IsNullOrEmpty(query) ? "" : $"public_id LIKE {query}";
            List<SearchResource> result = Api.Search().Expression(expression).MaxResults(5000).Execute().Resources.ToList();
            var publicIds = new List<string>();

            foreach (var r in result)
                publicIds.Add(r.PublicId);

            var resultado = Api.ListResourceByPublicIds(publicIds, false, false, false).Resources.ToList();

            return resultado;
        }

        public static void GetImagesByTag(string tag)
        {
        }
    }
}
