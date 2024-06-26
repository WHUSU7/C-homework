﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace work
{
	public class WebsocketService
	{
		private ClientWebSocket _webSocket;
        APIService apiService = new APIService();
        public int[,] board = Board.getBoardInstance();
		//链接后端
		public async Task ConnectAsync(Uri uri)
		{
			_webSocket = new ClientWebSocket();
			await _webSocket.ConnectAsync(uri, CancellationToken.None);
			Console.WriteLine("Connected to WebSocket server");
		}
		//发送消息
		public async Task SendAsync(object json)
		{
			string message = JsonConvert.SerializeObject(json);
			var messageBuffer = Encoding.UTF8.GetBytes(message);
			var segment = new ArraySegment<byte>(messageBuffer);
			await _webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);

		}
		//接收消息
		//wpf无法自动监听，只能使用循环并在循环中获取信息
		//这个方法不可以返回，不然就视为结束了监听，等效与断开链接！！！
		public async Task StartListeningAsync()
		{
			while (_webSocket.State == WebSocketState.Open)
			{
				var buffer = new byte[1024];
				var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

				if (result.MessageType == WebSocketMessageType.Close)
				{
					await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
				}
				else
				{
					var res = Encoding.UTF8.GetString(buffer, 0, result.Count);
					Console.WriteLine("Listen Message received: " + res);
					//如果是首次链接返回的id
					if (res == "1" || res == "-1")
					{
						if (res == "1")
						{
							TextBlock tb = (TextBlock)App.WebsocketPVPInstance.FindName("leftUserName");
							tb.Text = App.user.nickname;
                            string path = await apiService.getProfilePicture();
							Console.WriteLine(path);
                            if (path != "empty")
                            {
                                // 创建新的位图图像
                                BitmapImage bitmap = new BitmapImage();

                                bitmap.BeginInit();
                                bitmap.UriSource = new Uri(path);
                                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                bitmap.EndInit();

                                var imageControl = App.WebsocketPVPInstance.FindName("leftUserImage") as ImageBrush;
								Console.WriteLine(imageControl);
                                // 将位图图像设置为 Ellipse 的填充
								if(imageControl!=null)
                                imageControl.ImageSource = bitmap;

                            }
                            App.AppMsg.turn = "1";
						}
						else
						{
							TextBlock tb = (TextBlock)App.WebsocketPVPInstance.FindName("rightUserName");
							tb.Text = App.user.nickname;
                            string path = await apiService.getProfilePicture();

                            if (path != "empty")
                            {
                                // 创建新的位图图像
                                BitmapImage bitmap = new BitmapImage();

                                bitmap.BeginInit();
                                bitmap.UriSource = new Uri(path);
                                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                bitmap.EndInit();


								var imageControl = App.WebsocketPVPInstance.FindName("RightUserImage") as ImageBrush;
                                // 将位图图像设置为 Ellipse 的填充
                                if (imageControl != null)
                                    imageControl.ImageSource = bitmap;
								

                            }
                            App.AppMsg.turn = "-1";
						}
					}
					//后续链接返回的坐标
					else
					{
						JObject data = JObject.Parse(res);
						var mes = data["message"];
						JObject text = JObject.Parse(mes["text"].ToString());
						//双重解析后的坐标和轮次
						string position = text["Message"].ToString();
						string Turn = text["Turn"].ToString();
						App.AppMsg.receiveTurn = Turn;
						//执行同步（按钮显示等）
						int x = (int)position[0] - '0';
						int y = (int)position[1] - '0';

						Button btn = (Button)App.WebsocketPVPInstance.FindName("Button" + position);

						//历史记录获取坐标
						GameService.Instance.getPosition(x, y);

						btn.Visibility = Visibility.Visible;
						if (Turn == "1") { board[x, y] = 1; } else { board[x, y] = -1; }
;

						//根据nowTurn显示当前按钮，后续添加逻辑时要注意何时将nowTurn取反
						if (Turn == "1")
						{
							BitmapImage bitmap = new BitmapImage();
							bitmap.BeginInit();
							bitmap.UriSource = new Uri(@"..\..\Images\black.png", UriKind.RelativeOrAbsolute);
							// Console.WriteLine("Image path: " + AppDomain.CurrentDomain.BaseDirectory + @"Images\OIP-C1.jpg");
							bitmap.EndInit();
							// 创建 ImageBrush 并设置其 ImageSource
							ImageBrush imageBrush = new ImageBrush();
							imageBrush.ImageSource = bitmap;
							btn.Background = imageBrush;
						}
						else
						{
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(@"..\..\Images\white.png", UriKind.RelativeOrAbsolute);
                            // Console.WriteLine("Image path: " + AppDomain.CurrentDomain.BaseDirectory + @"Images\OIP-C1.jpg");
                            bitmap.EndInit();
                            // 创建 ImageBrush 并设置其 ImageSource
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = bitmap;
                            btn.Background = imageBrush;
                        }

						await AnimationUtils.allAnimation(btn, x, App.AppCanvasShape.width, (Canvas)App.WebsocketPVPInstance.FindName("myCanvas"));

                        if (Board.IsWin(x, y, int.Parse(App.AppMsg.receiveTurn)))
						{
							Console.WriteLine("someOne win");
							if (App.isPvpWin)
							{
							//	MessageBox.Show("YOU Win!");
                                App.isWin = true;
                                GameService.Instance.winOrfail(true);//传递胜负参数
                                GameService.Instance.getCompeteType("人人对战");//传递对局类型参数
                                Utils.showIsInsertHistoryWindow();
							}
							else
							{

								Console.WriteLine("you lose");
							//	MessageBox.Show("YOU Lose!");
								App.isWin = false;
                                GameService.Instance.winOrfail(false);//传递胜负参数
                                GameService.Instance.getCompeteType("人人对战");//传递对局类型参数
                                Utils.showIsInsertHistoryWindow();
							}

						}
						
						App.WebsocketPVPInstance.setTimerAndClick();



					}



				}
			}
		}



		//关闭链接
		public async Task CloseAsync()
		{
			await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
			Console.WriteLine("WebSocket connection closed");
		}
	}
}
