using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using work.Models;

namespace work
{
    //与后端进行网络通信
    public class APIService
    {

       
       

        private static readonly HttpClient client = new HttpClient();

        public APIService()
        {
            //base api
            //虚拟机上要用物理机可用端口的ip代替本地地址，如en0的inet
            //物理机上直接127.0.0.1：4523即可
            client.BaseAddress = new Uri("http://10.133.157.7:4523/m1/4020303-0-default/fourchess/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        //获取单条历史记录（get）
        //Task<>里面写接口返回的类型
        public async Task<string> getSingleHistory(int index)
        {
              
          var response =  await client.GetAsync($"history{index}");
            //取到的是所有属性的字符串
            string json = await response.Content.ReadAsStringAsync();
            //做格式转换并通过key的方式取某个属性
            var jsonObject = JsonConvert.DeserializeObject<JObject>(json);
            string historyValue = jsonObject["history"].ToString();

            return historyValue;

        }

        //登录（post）
        public async Task<int> login(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            //content代表要传入的参数
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("login", content);
            string res= await response.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<JObject> (res);
            string id = jsonObject["id"].ToString();
            return int.Parse( id);




        }
    }
}
