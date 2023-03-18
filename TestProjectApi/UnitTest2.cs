using Data.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace TestProjectApi
{
    [TestClass]
    public class UnitTest2
    {
        public static string Token { get; set; }

        [TestMethod]
        public void TestMethod2()
        {
            //var urlApiAdicionarProduto = "https://localhost:7102/api/AdicionarProduto";
            //var produto = new Produto { Nome = "J Coltrane", Imagem = "" };
            //var resultTask = ChamaApiPost(urlApiAdicionarProduto, produto);
            //var result = resultTask.Result;
            //var listaProd = JsonConvert.DeserializeObject<Produto[]>(result).ToString();
            //Assert.IsTrue(!string.IsNullOrWhiteSpace(listaProd));

            var urlApiAdicionarProduto = "https://localhost:7102/api/AdicionarProduto";
            var produto = new Produto { Nome = "J Coltrane", Imagem = "" };
            var responseTask = ChamaApiPost(urlApiAdicionarProduto, produto);
            var response = responseTask.Result;
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

        }

        private async Task<string> GetToken()
        {
            string urlApiGeraToken = "https://localhost:7102/api/CreateToken";

            using (var cliente = new HttpClient())
            {
                string login = "administrador@adm.com";
                string senha = "@Dm1234";
                var dados = new
                {
                    Email = login,
                    Password = senha,
                    cpf = "string"
                };
                string JsonObjeto = JsonConvert.SerializeObject(dados);
                var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");

                var resultado = await cliente.PostAsync(urlApiGeraToken, content);
                if (resultado.IsSuccessStatusCode)
                {
                    var tokenJson = await resultado.Content.ReadAsStringAsync();
                    Token = JsonConvert.DeserializeObject(tokenJson).ToString();
                }
            }

            return Token;
        }

        //private async Task<string> GetToken()
        //{
        //    var urlApiToken = "https://localhost:7102/api/Token";
        //    var request = new HttpRequestMessage(HttpMethod.Post, urlApiToken);
        //    request.Content = new StringContent(JsonConvert.SerializeObject(new { Username = "user", Password = "password" }),
        //        Encoding.UTF8, "application/json");
        //    var client = new HttpClient();
        //    var response = await client.SendAsync(request);
        //    var result = await response.Content.ReadAsStringAsync();
        //    var token = JsonConvert.DeserializeObject<JObject>(result)?.GetValue("access_token")?.ToString();
        //    return token;
        //}

        public async Task<string> ChamaApiGet(string url)
        {
            GetToken(); // Gerar token
            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Clear();
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var response = await cliente.GetAsync(url);
                    var content = await response.Content.ReadAsStringAsync(); // lê o conteúdo da resposta
                    return content;
                }
            }

            return null;

        }

        //public async Task<string> ChamaApiPost(string url, object dados = null)
        //{

        //    string JsonObjeto = dados != null ? JsonConvert.SerializeObject(dados) : "";
        //    var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");

        //    GetToken(); // Gerar token
        //    if (!string.IsNullOrWhiteSpace(Token))
        //    {
        //        using (var cliente = new HttpClient())
        //        {
        //            cliente.DefaultRequestHeaders.Clear();
        //            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        //            var response = cliente.PostAsync(url, content);
        //            response.Wait();
        //            if (response.Result.IsSuccessStatusCode)
        //            {
        //                var retorno = await response.Result.Content.ReadAsStringAsync();

        //                return retorno;
        //            }
        //        }
        //    }

        //    return null;

        //}

        private async Task<HttpResponseMessage> ChamaApiPost(string url, object data)
        {
            //Task<string> tokenTask = GetToken();
            //var token = tokenTask.Result;

            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            //var response = await client.PostAsync(url, content);
            //Console.WriteLine(response.StatusCode);
            //var retorno = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(retorno);

            //return retorno;

            await GetToken();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            Console.WriteLine(response.StatusCode);
            var retorno = await response.Content.ReadAsStringAsync();
            Console.WriteLine(retorno);

            return response;
        }

    }
}