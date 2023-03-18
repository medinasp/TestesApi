using Data.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace TestProjectApi
{
    [TestClass]
    public class UnitTest3
    {
        public static string Token { get; set; }

        [TestMethod]
        public void TestMethod3()
        {
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

        private async Task<HttpResponseMessage> ChamaApiPost(string url, object data)
        {
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