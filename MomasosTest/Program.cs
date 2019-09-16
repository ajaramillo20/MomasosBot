using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppController;

namespace MomasosTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var cloud = "momasosbot";
            var apikey = "548739216139167";
            var apisecret = "OC5Ajc-g9sAa7lFtcEdsbarFUVM";

            Iniciar(cloud,apikey,apisecret);


            var resultados = Api.ListResources(new ListResourcesParams() { MaxResults = 50});

            
            //cloud momasosbot
            //apikey 548739216139167
            //apisecret OC5Ajc-g9sAa7lFtcEdsbarFUVM
        }
        private static Cloudinary Api;

        public static void Iniciar(string cloud, string apiKey, string apiSecert)
        {
            var account = new Account()
            {
                Cloud = cloud,
                ApiKey = apiKey,
                ApiSecret = apiSecert
            };

            Api = new Cloudinary(account);
        }

        public static List<Resource> GetImagesByQuery(string query)
        {

            string expression = string.IsNullOrEmpty(query) ? "" : $"public_id LIKE {query}";

            if (string.IsNullOrEmpty(expression))
            {
                var listResourcesParams = new ListResourcesParams()
                {
                    Type = "upload",
                    MaxResults = 5000
                };
                var listResourcesResult = Api.ListResources(listResourcesParams).Resources.ToList();
                
                return listResourcesResult;
            }
            else
            {
                List<SearchResource> result = Api.Search().Expression(expression).MaxResults(5000).Execute().Resources.ToList();
                var publicIds = new List<string>();

                foreach (var r in result)
                {
                    publicIds.Add(r.PublicId);
                }

                var resultado = Api.ListResourceByPublicIds(publicIds, false, false, false).Resources.ToList();
                return resultado;
            }
        }

    }
}
