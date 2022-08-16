using System.Text;
using System.Text.Json;

namespace MauiApp_Furion.Services.DingdingRobotServices
{
    public class SendMessage : ISendMessage
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SendMessage(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<string> SendMessageToDingdingAsync(string url, string msg)
        {
            // 创建 HttpClient 对象
            using var httpClient = _httpClientFactory.CreateClient();
            var msgbody =
                new StringContent(
                    JsonSerializer.Serialize(new
                    {
                        msgtype = "text",
                        text = new
                        {
                            content = msg
                        }
                    }),
                    Encoding.UTF8,
                    "application/json");
            var res = await httpClient.PostAsync(url, msgbody);
            var response = await res.Content.ReadAsStringAsync();
            return response;
        }
    }
}
