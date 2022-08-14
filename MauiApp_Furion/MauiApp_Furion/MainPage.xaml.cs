using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MauiApp_Furion;

public partial class MainPage : ContentPage
{
    string hello = "";
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        var api = "https://oapi.dingtalk.com/robot/send?access_token=";
        //机器人Token
        var token = "3b2191cd9565f021dbe4b80b918f918b7ef2fc1be101417bf31a6b22074538e4";
        //加签密钥
        var secret = "SEC621ae2d4061059270a5c80dfff336cb2870f5db5f7abed7720eebdacbda265c3";

        #region 加签
        //把timestamp + "\n" + 密钥当做签名字符串，使用HmacSHA256算法计算签名，然后进行Base64 encode，
        //最后再把签名参数再进行urlEncode，得到最终的签名（需要使用UTF-8字符集）。
        long timestamp = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;

        string stringToSign = timestamp + "\n" + secret;

        var b64 = getHmac(stringToSign, secret);

        string sign = HttpUtility.UrlEncode(Convert.ToBase64String(b64));
        #endregion

        var robotUrl = $"{api}{token}&timestamp={timestamp}&sign={sign}";
        String textMsg = "{ \"msgtype\": \"text\", \"text\": {\"content\": \"" + hello + "\"}}";
        string s = Post(robotUrl, textMsg, null);

        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        entry.Text = "";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void OnTextCompleted(object sender, EventArgs e)
    {
        hello = entry.Text;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        hello = entry.Text;
    }

    #region HMACSHA256加密
    static byte[] getHmac(string message, string secret)
    {
        byte[] keyByte = Encoding.UTF8.GetBytes(secret);
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return hashmessage;
        }
    }
    #endregion

    #region Post  
    /// <summary>  
    /// 以Post方式提交命令  
    /// </summary>  
    /// <param name="apiurl">请求的URL</param>  
    /// <param name="jsonString">请求的json参数</param>  
    /// <param name="headers">请求头的key-value字典</param>  
    private static String Post(string apiurl, string jsonString, Dictionary<String, String> headers = null)
    {
        WebRequest request = WebRequest.Create(@apiurl);
        request.Method = "POST";
        request.ContentType = "application/json";
        if (headers != null)
        {
            foreach (var keyValue in headers)
            {
                if (keyValue.Key == "Content-Type")
                {
                    request.ContentType = keyValue.Value;
                    continue;
                }
                request.Headers.Add(keyValue.Key, keyValue.Value);
            }
        }

        if (String.IsNullOrEmpty(jsonString))
        {
            request.ContentLength = 0;
        }
        else
        {
            byte[] bs = Encoding.UTF8.GetBytes(jsonString);
            request.ContentLength = bs.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(bs, 0, bs.Length);
            newStream.Close();
        }


        WebResponse response = request.GetResponse();
        Stream stream = response.GetResponseStream();
        Encoding encode = Encoding.UTF8;
        StreamReader reader = new StreamReader(stream, encode);
        string resultJson = reader.ReadToEnd();
        return resultJson;
    }
    #endregion

}

