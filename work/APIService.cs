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
using System.Windows.Controls;
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
            client.BaseAddress = new Uri("http://192.168.43.254:8000/m1/4020303-0-default/fourchess/");
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
            string historyValue = jsonObject["content"].ToString();

            return historyValue;

        }
        //获取目标用户的所有历史记录
        public async Task<List<string>> getHistories(int userid)
        {

            var response = await client.GetAsync($"{userid}/histories");
            //取到的是所有属性的字符串
            string json = await response.Content.ReadAsStringAsync();
            //做格式转换并通过key的方式取某个属性
             var jsonObject = JsonConvert.DeserializeObject<JArray>(json);
            List<string> historyArray = new List<string>();
            foreach (JObject item in jsonObject) {
                historyArray.Add(item["content"].ToString());
            }

            return historyArray;

        }

        //登录（post）
        public async Task<int> login(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            //content代表要传入的参数
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("login", content);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(res);
                string id = jsonObject["id"].ToString();
                string name = jsonObject["name"].ToString();
                string password = jsonObject["password"].ToString();
                App.user.id = int.Parse(id);
                App.user.name = name;
                App.user.password = password;
                return int.Parse(id);
            }
            else if ((int)response.StatusCode == 401) {
                MessageBox.Show("用户未注册");
                return -1;
            }
            return -2;
        }

        //注册
        public async Task<int> register(User user)
        {
            
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("register", content);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(res);
                string id = jsonObject["id"].ToString();
                string name = jsonObject["name"].ToString();
                string password = jsonObject["password"].ToString();
                App.user.id = int.Parse(id);
                App.user.name = name;
                App.user.password = password;

                return int.Parse(id);
            }
            else if ((int)response.StatusCode ==400)
            {
                MessageBox.Show("该用户名已被使用");
                return -1;
            }
            return -2;
        }


    }
}
