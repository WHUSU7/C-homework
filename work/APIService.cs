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
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Xml.Linq;
using work.Models;

namespace work
{
	//与后端进行网络通信
	public  class APIService
	{

		public int[,] board = Board.getBoardInstance();


		private readonly HttpClient client = new HttpClient();

		public  APIService()
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

			var response = await client.GetAsync($"history{index}");
			//取到的是所有属性的字符串
			string json = await response.Content.ReadAsStringAsync();
			//做格式转换并通过key的方式取某个属性
			var jsonObject = JsonConvert.DeserializeObject<JObject>(json);
			string historyValue = jsonObject["content"].ToString();

			return historyValue;

		}
		//获取目标用户的所有历史记录
		public async Task<List<History>> getHistories(int userid)
		{

			var response = await client.GetAsync($"{userid}/histories");
			//取到的是所有属性的字符串
			string json = await response.Content.ReadAsStringAsync();
			
            //做格式转换并通过key的方式取某个属性
            //var jsonObject = JsonConvert.DeserializeObject<JArray>(json);
            List<History> historyArray = new List<History>();
            json = json.Replace("Infinity", "null"); 
            var jsonObject = JsonConvert.DeserializeObject<JArray>(json);
           
			foreach (JObject item in jsonObject)
			{
				History history = new History((int)item["id"], item["content"].ToString(), item["matchTime"].ToString(), item["matchType"].ToString(), item["isWin"].ToString());
				historyArray.Add(history);
            
			}

			return historyArray;

		}

		//插入历史记录

		public async Task<string> insertHistory(History history, int userid)
		{
			var json = JsonConvert.SerializeObject(history);
			var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
			var response = await client.PostAsync($"{userid}/insertHistory", content);

			if (response.IsSuccessStatusCode)
			{
				string res = await response.Content.ReadAsStringAsync();
				var jsonObject = JsonConvert.DeserializeObject<JObject>(res);
				string isSuccess = jsonObject["isSuccess"].ToString();
				return isSuccess;
			}
			return "";

		}

		//登录（post）
		public async Task<int> login(User user)
		{
			var json = JsonConvert.SerializeObject(user);
			//content代表要传入的参数
			var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

			var response = await client.PostAsync("login", content);
			Console.WriteLine(response);
			if (response.IsSuccessStatusCode)
			{
				string res = await response.Content.ReadAsStringAsync();
				var jsonObject = JsonConvert.DeserializeObject<JObject>(res);
				string id = jsonObject["id"].ToString();
				string name = jsonObject["name"].ToString();
				string password = jsonObject["password"].ToString();
				string nickname = jsonObject["nickname"].ToString();
				App.user.id = int.Parse(id);
				App.user.name = name;
				App.user.password = password;
				App.user.nickname = nickname;
				return int.Parse(id);
			}
			else if ((int)response.StatusCode == 401)
			{
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
				string nickname = jsonObject["nickname"].ToString();
				App.user.id = int.Parse(id);
				App.user.name = name;
				App.user.password = password;
				App.user.nickname = nickname;

				return int.Parse(id);
			}
			else if ((int)response.StatusCode == 400)
			{
				MessageBox.Show("该账号已经存在");
				return -1;
			}
			return -2;
		}

        //获取头像路径（get）
        public async Task<string> getProfilePicture()
        {

            var response = await client.GetAsync($"{App.user.id}/getProfilePicture");
           
            string json = await response.Content.ReadAsStringAsync();
           
            var jsonObject = JsonConvert.DeserializeObject<JObject>(json);
            string profilePicturePath = jsonObject["profilePicture"].ToString();

            return profilePicturePath;

        }

		//修改头像路径
		public async Task<string> updateProfilePicture(string newPath) {
			ProfilePicturePath profilePicturePath = new ProfilePicturePath(newPath);
            var json = JsonConvert.SerializeObject(profilePicturePath);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

          var response = await client.PostAsync($"{App.user.id}/updateProfilePicture", content);

            string res = await response.Content.ReadAsStringAsync();
          
		

            var jsonObject = JsonConvert.DeserializeObject<JObject>(res);

            string isSuccess = jsonObject["isSuccess"].ToString();

			return isSuccess; 
        }


        //发送消息
        //turn是开始对局的时候返回的数，作为双方身份的唯一标识
        //public async Task<string> clientSendMsg(Msg msg, int userid)
        //{
        //	var json = JsonConvert.SerializeObject(msg);
        //	var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        //	var response = await client.PostAsync($"{userid}/clientSendMsg", content);
        //	if (response.IsSuccessStatusCode)
        //	{
        //		string res = await response.Content.ReadAsStringAsync();
        //		var jsonObject = JsonConvert.DeserializeObject<JObject>(res);
        //		string isSuccess = jsonObject["isSuccess"].ToString();
        //		return isSuccess;

        //	}
        //	return "";
        //}
        //接收消息
        //public async void clientGetMsg(int userid)
        //{

        //	var response = await client.GetAsync($"{userid}/clientGetMsg");
        //	if (response.IsSuccessStatusCode)
        //	{
        //		string res = await response.Content.ReadAsStringAsync();

        //		var jsonObject = JsonConvert.DeserializeObject<JObject>(res);
        //		string msg = jsonObject["msg"].ToString();
        //		string turn = jsonObject["turn"].ToString();

        //		App.AppMsg.msg = msg;
        //		App.AppMsg.turn = turn;
        //		//执行同步（按钮显示等）
        //		int x = (int)msg[0] - '0';
        //		int y = (int)msg[1] - '0';

        //		Button btn = (Button)App.WebsocketPVPInstance.FindName("Button" + msg);

        //历史记录获取坐标
        //GameService.Instance.getPosition(x, y);

        //btn.Visibility = Visibility.Visible;
        //if (App.AppMsg.turn == "1") { board[x, y] = 1; } else { board[x, y] = -1; }

        // AnimationUtils.allAnimation(btn, x, App.AppCanvasShape.width, (Canvas)App.WebsocketPVPInstance.FindName("myCanvas"));

        //根据nowTurn显示当前按钮，后续添加逻辑时要注意何时将nowTurn取反
        //if (App.AppMsg.turn == "1")
        //{
        //	BitmapImage bitmap = new BitmapImage();
        //	bitmap.BeginInit();
        //	bitmap.UriSource = new Uri(@"..\..\Images\OIP-C1.jpg", UriKind.RelativeOrAbsolute);
        //	// Console.WriteLine("Image path: " + AppDomain.CurrentDomain.BaseDirectory + @"Images\OIP-C1.jpg");
        //	bitmap.EndInit();
        // 创建 ImageBrush 并设置其 ImageSource
        //	ImageBrush imageBrush = new ImageBrush();
        //	imageBrush.ImageSource = bitmap;
        //	btn.Background = imageBrush;
        //}
        //else
        //{
        //	btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBD26A"));

        //}

        //同步完成后把turn取反
        //		if (App.AppMsg.turn == "1")
        //		{
        //			App.AppMsg.turn = "-1";
        //		}
        //		else
        //		{
        //			App.AppMsg.turn = "1";
        //		}

        //		clientGetMsg(userid);
        //	}
        //	else if ((int)response.StatusCode == 400)
        //	{

        //		clientGetMsg(userid);
        //	}

        //}
        //开始pvp
        //public async Task<string> createPvp(int userid)
        //{

        //	var response = await client.GetAsync($"{userid}/createPvp");
        //	if (response.IsSuccessStatusCode)
        //	{
        //		string res = await response.Content.ReadAsStringAsync();
        // var jsonObject = JsonConvert.DeserializeObject<JObject>(res);
        // string turn = jsonObject["turn"].ToString();
        //	App.AppMsg.turn = res;

        //	return res;

        //}
        //return "";

        //}

    }
}
