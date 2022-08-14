using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class TestService : ITestService
    {
        public string GetString()
        {
            return "我是测试Test类,你有什么事吗?";
        }
    }
}
