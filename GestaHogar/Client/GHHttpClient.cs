using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaHogar.Client
{
    //Soy consiciente de que muchas rutas son iguales, es para hacer el código más legible
    public static class GHHttpClient
    {
        public static HttpClient Client { get; } = new();

        private static readonly string API_URL = "https://localhost:7224";//modificar la dirección de la api tras deploy

        public const string AUTH_SCHEME = JwtBearerDefaults.AuthenticationScheme;

        public static string TokenKey => "AuthToken";

        public static Uri GetProductsUri => new($"{API_URL}/api/products");
        public static Uri GetProductUri(int id) => new($"{API_URL}/api/products/{id}");
        public static Uri PostProductUri => new($"{API_URL}/api/products");
        public static Uri PutProductUri(int id) => new($"{API_URL}/api/products{id}");
        public static Uri DeleteProductUri(int id) => new($"{API_URL}/api/products/{id}");


        public static Uri GetUserProductsUri => new($"{API_URL}/api/userproducts");
        public static Uri GetUserProductUri(int id) => new($"{API_URL}/api/userproducts/{id}");
        public static Uri PostUserProductUri => new($"{API_URL}/api/userproducts");
        public static Uri PutUserProductUri(int id) => new($"{API_URL}/api/userproducts/{id}");
        public static Uri DeleteUserProductUri(int id) => new($"{API_URL}/api/userproducts/{id}");
        public static Uri PutUpdateAllUserProductsUri => new($"{API_URL}/api/userproducts/updateall");
        public static Uri PutUpdateUserProduct(int id) => new($"{API_URL}/api/userproducts/updateproduct/{id}");


        public static Uri LoginUri => new($"{API_URL}/login");
        public static Uri RegisterUri => new($"{API_URL}/register");


    }
}
