using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace work
{

	public static class Board
	{
		public const int NUM_COL = 7;//定义棋盘列数
		public const int NUM_ROW = 6;//定义棋盘行数
		public const int PLAYER = 1;//玩家
		public const int COMPUTER = -1;//AI
		private static int[,] board = {

			{0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0},
		};



		//单例模式，确保只有一个board
		public static int[,] getBoardInstance()
		{
			return board;
		}

		//重置board为全0
		//通过名称来清楚对应界面的按钮显示

		public static void resetBoard(string target)
		{
			for (int i = 0; i < board.GetLength(0); i++)
			{
				for (int j = 0; j < board.GetLength(1); j++)
				{

					switch (target)
					{
						case "Local":
							string mtargetBtn = "Button" + i.ToString() + j.ToString();
							Button mbtn = (Button)App.LocalInstance.FindName(mtargetBtn);
							mbtn.Visibility = Visibility.Hidden;
							break;
						case "AI":
							string atargetBtn = "Button" + i.ToString() + j.ToString();
							Button abtn = (Button)App.AIInstance.FindName(atargetBtn);
							abtn.Visibility = Visibility.Hidden;
							break;
						case "PVP":
							string PtargetBtn = "Button" + i.ToString() + j.ToString();
							Button Pbtn = (Button)App.WebsocketPVPInstance.FindName(PtargetBtn);
							Pbtn.Visibility = Visibility.Hidden;
							break;

					}
					board[i, j] = 0;
				}
			}
		}

		//判断胜利逻辑，看x,y附近是否四连
		//turn =+1表示  人
		//turn =-1表示  AI
		public static bool IsWin(int x, int y, int turn)
		{
			if (turn == -1)//黑子落下，看周围-1
			{
				int count = 0;
				// 1、纵向 只会在下方黑子
				int bottomcount = 0;
				int bx = x + 1;
				while (bx < 6 && board[bx, y] == -1)
				{
					bottomcount++;
					bx++;
				}
				count = bottomcount + 1;
				if (count >= 4)
				{
					return true;
				}

				// 2、横向 
				int leftcount = 0;
				int rightcount = 0;
				int ly = y - 1;
				int ry = y + 1;
				while (ly >= 0 && board[x, ly] == -1)
				{
					leftcount++;
					ly--;
				}
				while (ry < 7 && board[x, ry] == -1)
				{
					rightcount++;
					ry++;
				}
				count = leftcount + rightcount + 1;
				if (count >= 4)
				{
					return true;
				}

				// 3、左上到右下
				int ltcount = 0;
				int rbcount = 0;
				int ltx = x - 1;
				int lty = y - 1;
				int rbx = x + 1;
				int rby = y + 1;
				while (ltx >= 0 && lty >= 0 && board[ltx, lty] == -1)
				{
					ltcount++;
					ltx--;
					lty--;
				}
				while (rbx < 6 && rby < 7 && board[rbx, rby] == -1)
				{
					rbcount++;
					rbx++;
					rby++;
				}
				count = ltcount + rbcount + 1;
				if (count >= 4)
				{
					return true;
				}

				// 4、左下到右上
				int lbcount = 0;
				int rtcount = 0;
				int lbx = x + 1;
				int lby = y - 1;
				int rtx = x - 1;
				int rty = y + 1;
				while (lbx < 6 && lby >= 0 && board[lbx, lby] == -1)
				{
					lbcount++;
					lbx++;
					lby--;
				}
				while (rtx >= 0 && rty < 7 && board[rtx, rty] == -1)
				{
					rtcount++;
					rtx--;
					rty++;
				}
				count = lbcount + rtcount + 1;
				if (count >= 4)
				{
					return true;
				}

				return false;

			}
			else//白子落下，看周围+1
			{
				int count = 0;
				// 1、纵向 只会在下方白子
				int bottomcount = 0;
				int bx = x + 1;
				while (bx < 6 && board[bx, y] == 1)
				{
					bottomcount++;
					bx++;
				}
				count = bottomcount + 1;
				if (count >= 4)
				{
					return true;
				}

				// 2、横向 
				int leftcount = 0;
				int rightcount = 0;
				int ly = y - 1;
				int ry = y + 1;
				while (ly >= 0 && board[x, ly] == 1)
				{
					leftcount++;
					ly--;
				}
				while (ry < 7 && board[x, ry] == 1)
				{
					rightcount++;
					ry++;
				}
				count = leftcount + rightcount + 1;
				if (count >= 4)
				{
					return true;
				}

				// 3、左上到右下
				int ltcount = 0;
				int rbcount = 0;
				int ltx = x - 1;
				int lty = y - 1;
				int rbx = x + 1;
				int rby = y + 1;
				while (ltx >= 0 && lty >= 0 && board[ltx, lty] == 1)
				{
					ltcount++;
					ltx--;
					lty--;
				}
				while (rbx < 6 && rby < 7 && board[rbx, rby] == 1)
				{
					rbcount++;
					rbx++;
					rby++;
				}
				count = ltcount + rbcount + 1;
				if (count >= 4)
				{
					return true;
				}

				// 4、左下到右上
				int lbcount = 0;
				int rtcount = 0;
				int lbx = x + 1;
				int lby = y - 1;
				int rtx = x - 1;
				int rty = y + 1;
				while (lbx < 6 && lby >= 0 && board[lbx, lby] == 1)
				{
					lbcount++;
					lbx++;
					lby--;
				}
				while (rtx >= 0 && rty < 7 && board[rtx, rty] == 1)
				{
					rtcount++;
					rtx--;
					rty++;
				}
				count = lbcount + rtcount + 1;
				if (count >= 4)
				{
					return true;
				}

				return false;

			}

		}
		// 判断当前位置是否是合法的落子位置
		private static bool IsValidMove(int row, int col)
		{
			return row >= 0 && row < board.GetLength(0) && col >= 0 && col < board.GetLength(1) && board[row, col] == 0 && (row == board.GetLength(0) - 1 || (row < board.GetLength(0) - 1 && board[row + 1, col] == 1 || board[row + 1, col] == -1));
		}



		//-------------------------------------------------简单AI代码块--------------------------------------------------------------------
		public static Tuple<int, int> NextMove(int nowturn)
		{
			// 1、如果能胜利，直接下
			Tuple<int, int> winningMove = FindWinningMove(nowturn);
			if (winningMove != null)
			{
				return winningMove;
			}

			// 2、检查是否存在对方离胜利仅差一子的情况，如果有则堵上
			Tuple<int, int> blockingMove = FindBlockingMove(nowturn);
			if (blockingMove != null)
			{
				return blockingMove;
			}

			//3、以上两种都不存在，那么优先找附近有对手棋的地方下
			Tuple<int, int> nearbyMove = FindNearbyMove(nowturn);
			if (nearbyMove != null)
			{
				return nearbyMove;
			}

			//4、最后选择下一步可以避免给对方搭桥的位置
			return FindSafeMove(nowturn);
		}

		// 1、寻找能直接获胜的位置
		private static Tuple<int, int> FindWinningMove(int nowturn)
		{
			for (int col = 0; col < board.GetLength(1); col++)
			{
				for (int row = 0; row < board.GetLength(0); row++)
				{
					if (IsValidMove(row, col))
					{
						// 尝试在当前位置落子，然后检查自己是否即将获胜

						bool opponentWinningMove = IsWin(row, col, nowturn);

						//如果获胜了，直接下
						if (opponentWinningMove)
						{
							return Tuple.Create(row, col); 
						}
					}
				}
			}
			return null; // 如果找不到需要堵上的位置，则返回 null

		}
		// 2、寻找需要堵上的位置
		private static Tuple<int, int> FindBlockingMove(int nowturn)
		{
			for (int col = 0; col < board.GetLength(1); col++)
			{
				for (int row = 0; row < board.GetLength(0); row++)
				{
					if (IsValidMove(row, col))
					{
						// 尝试在当前位置落子，然后检查是否对方即将获胜

						bool opponentWinningMove = IsWin(row, col, -1 * nowturn);

						//如果对面获胜了，堵上
						if (opponentWinningMove)
						{
							return Tuple.Create(row, col); // 返回下一步需要堵上的位置
						}
					}
				}
			}
			return null; // 如果找不到需要堵上的位置，则返回 null

		}
		//3、找最近的下一个点
		private static Tuple<int, int> FindNearbyMove(int nowturn)
		{
			for (int row = board.GetLength(0) - 1; row >= 0; row--)
			{
				for (int col = board.GetLength(1) - 1; col >= 0; col--)
				{
					// 检查当前位置是否为空
					if (IsValidMove(row, col))
					{
						// 检查当前位置的周围是否有对方的棋子
						bool nearbyOpponentPiece = false;
						for (int i = row - 1; i <= row + 1; i++)
						{
							for (int j = col - 1; j <= col + 1; j++)
							{
								if (i >= 0 && i < board.GetLength(0) && j >= 0 && j < board.GetLength(1) &&
									!(i == row && j == col) && board[i, j] == -nowturn)
								{
									nearbyOpponentPiece = true;
									break;
								}
							}
							if (nearbyOpponentPiece)
							{
								break;
							}
						}

						// 如果周围有对方的棋子，则返回当前位置
						if (nearbyOpponentPiece)
						{
							return Tuple.Create(row, col);
						}
					}
				}
			}
			// 如果找不到附近有对方棋子的位置，则返回 null
			return null;
		}
		// 4、寻找安全的位置
		private static Tuple<int, int> FindSafeMove(int nowturn)
		{
			// 在上方有棋子的位置落子，避免给对方搭桥
			for (int col = 0; col < board.GetLength(1); col++)
			{
				for (int row = 0; row < board.GetLength(0); row++)
				{
					if (IsValidMove(row, col) && !IsWin(row - 1, col, -1 * nowturn))
					{
						return Tuple.Create(row, col); // 返回安全的位置
					}
				}
			}
			// 如果找不到安全的位置，则随机选择一个合法的位置
			Random random = new Random();
			while (true)
			{
				int row = random.Next(0, board.GetLength(0));
				int col = random.Next(0, board.GetLength(1));
				if (IsValidMove(row, col))
				{
					return Tuple.Create(row, col); // 返回随机选择的合法位置
				}
			}
		}



		//--------------------------------高级AI代码块--------------------------------------------------------------------------------------------
		//采用了Min-max算法和剪枝
		private static Tuple<int, int> miniMax(int[,] b, int d, int alf, int bet, int p)
		{
			if (d == 0)
			{
				// get current score to return
				return new Tuple<int, int>(tabScore(b, COMPUTER), -1);
			}
			if (p == COMPUTER)
			{ // if AI player
				Tuple<int, int> moveSoFar = new Tuple<int, int>(int.MinValue, -1); // since maximizing, we want lowest possible value
				if (WinningMove(b, PLAYER))
				{ // if player about to win
					return moveSoFar; // force it to say it's worst possible score, so it knows to avoid move
				} // otherwise, business as usual
				for (int c = 0; c < NUM_COL; c++)
				{ // for each possible move
					if (b[0, c] == 0)
					{ // but only if that column is non-full
						int[,] newBoard = CopyBoard(b); // make a copy of the board
						makeMove(newBoard, c, p); // try the move
						int score = miniMax(newBoard, d - 1, alf, bet, PLAYER).Item1; // find move based on that new board state
						if (score > moveSoFar.Item1)
						{ // if better score, replace it, and consider that best move (for now)
							moveSoFar = new Tuple<int, int>(score, (int)c);
						}
						alf = Math.Max(alf, moveSoFar.Item1);
						if (alf >= bet) { break; } // for pruning
					}
				}
				return moveSoFar; // return best possible move
			}
			else
			{
				Tuple<int, int> moveSoFar = new Tuple<int, int>(int.MaxValue, -1); // since PLAYER is minimized, we want moves that diminish this score
				if (WinningMove(b, COMPUTER))
				{
					return moveSoFar; // if about to win, report that move as best
				}
				for (int c = 0; c < NUM_COL; c++)
				{
					if (b[0, c] == 0)
					{
						int[,] newBoard = CopyBoard(b);
						makeMove(newBoard, c, p);
						int score = miniMax(newBoard, d - 1, alf, bet, COMPUTER).Item1;
						if (score < moveSoFar.Item1)
						{
							moveSoFar = new Tuple<int, int>(score, (int)c);
						}
						bet = Math.Min(bet, moveSoFar.Item1);
						if (alf >= bet) { break; }
					}
				}
				return moveSoFar;
			}
		}
		// 复制棋盘函数，便于试探下子
		private static int[,] CopyBoard(int[,] board)
		{
			int[,] newBoard = new int[NUM_ROW, NUM_COL];
			for (int i = 0; i < NUM_ROW; i++)
			{
				for (int j = 0; j < NUM_COL; j++)
				{
					newBoard[i, j] = board[i, j];
				}
			}
			return newBoard;
		}
		// 棋子移动函数，给定列自下而上试探第一个能下的点
		private static void makeMove(int[,] board, int column, int player)
		{
			for (int r = NUM_ROW - 1; r >= 0; r--)
			{
				if (board[r, column] == 0)
				{
					board[r, column] = player;
					break;
				}
			}
		}
		//为适应前端要求给出的X，Y横坐标
		public static Tuple<int, int> showMove()
		{
			int column = aiMove();
			for (int r = NUM_ROW - 1; r >= 0; r--)
			{
				if (board[r, column] == 0)
				{
					return new Tuple<int, int>(r, column);
				}
			}
			return null;
		}
		//MAX_DEPTH 搜索的最大深度，可调节难度，深度越大难度越大
		public const int MAX_DEPTH = 5;
		private static int aiMove()
		{
			return miniMax(board, MAX_DEPTH, 0 - int.MaxValue, int.MaxValue, COMPUTER).Item2;
		}
		//根据局面判断胜负
		private static bool WinningMove(int[,] board, int player)
		{
			int winSequence = 0; // 用于计数连续的棋子数量
			int numRows = board.GetLength(0);
			int numCols = board.GetLength(1);

			// 水平检查
			for (int c = 0; c < numCols - 3; c++) // 对每一列
			{
				for (int r = 0; r < numRows; r++) // 对每一行
				{
					for (int i = 0; i < 4; i++) // 需要连续4个棋子
					{
						if (board[r, c + i] == player) // 如果棋子匹配
						{
							winSequence++; // 增加计数
						}
						if (winSequence == 4) { return true; } // 如果连续4个棋子
					}
					winSequence = 0; // 重置计数
				}
			}

			// 垂直检查
			for (int c = 0; c < numCols; c++)
			{
				for (int r = 0; r < numRows - 3; r++)
				{
					for (int i = 0; i < 4; i++)
					{
						if (board[r + i, c] == player)
						{
							winSequence++;
						}
						if (winSequence == 4) { return true; }
					}
					winSequence = 0;
				}
			}

			// 对角线检查（从左上到右下）
			for (int c = 0; c < numCols - 3; c++)
			{
				for (int r = 3; r < numRows; r++)
				{
					for (int i = 0; i < 4; i++)
					{
						if (board[r - i, c + i] == player)
						{
							winSequence++;
						}
						if (winSequence == 4) { return true; }
					}
					winSequence = 0;
				}
			}

			// 对角线检查（从左下到右上）
			for (int c = 0; c < numCols - 3; c++)
			{
				for (int r = 0; r < numRows - 3; r++)
				{
					for (int i = 0; i < 4; i++)
					{
						if (board[r + i, c + i] == player)
						{
							winSequence++;
						}
						if (winSequence == 4) { return true; }
					}
					winSequence = 0;
				}
			}

			return false; // 否则，没有获胜的移动
		}
		//计算当前局面的分数  b是board棋盘 p是当前行动者 +1/-1
		private static int tabScore(int[,] b, int p)
		{
			int score = 0;
			List<int> rs = new List<int>(new int[NUM_COL]);
			List<int> cs = new List<int>(new int[NUM_ROW]);
			List<int> set = new List<int>(new int[4]);
			/**
             * horizontal checks, we're looking for sequences of 4
             * containing any combination of AI, PLAYER, and empty pieces
             */
			for (int r = 0; r < NUM_ROW; r++)
			{
				for (int c = 0; c < NUM_COL; c++)
				{
					rs[c] = b[r, c]; // this is a distinct row alone
				}
				for (int c = 0; c < NUM_COL - 3; c++)
				{
					for (int i = 0; i < 4; i++)
					{
						set[i] = rs[c + i]; // for each possible "set" of 4 spots from that row
					}
					score += scoreSet(set, p); // find score
				}
			}
			// vertical
			for (int c = 0; c < NUM_COL; c++)
			{
				for (int r = 0; r < NUM_ROW; r++)
				{
					cs[r] = b[r, c];
				}
				for (int r = 0; r < NUM_ROW - 3; r++)
				{
					for (int i = 0; i < 4; i++)
					{
						set[i] = cs[r + i];
					}
					score += scoreSet(set, p);
				}
			}
			// diagonals
			for (int r = 0; r < NUM_ROW - 3; r++)
			{
				for (int c = 0; c < NUM_COL; c++)
				{
					rs[c] = b[r, c];
				}
				for (int c = 0; c < NUM_COL - 3; c++)
				{
					for (int i = 0; i < 4; i++)
					{
						set[i] = b[r + i, c + i];
					}
					score += scoreSet(set, p);
				}
			}
			for (int r = 0; r < NUM_ROW - 3; r++)
			{
				for (int c = 0; c < NUM_COL; c++)
				{
					rs[c] = b[r, c];
				}
				for (int c = 0; c < NUM_COL - 3; c++)
				{
					for (int i = 0; i < 4; i++)
					{
						set[i] = b[r + 3 - i, c + i];
					}
					score += scoreSet(set, p);
				}
			}
			return score;
		}
		private static int scoreSet(List<int> v, int p)
		{
			int good = 0; // points in favor of p
			int bad = 0; // points against p
			int empty = 0; // neutral spots
			for (int i = 0; i < v.Count(); i++)
			{ // just enumerate how many of each
				good += (v[i] == p) ? 1 : 0;
				bad += (v[i] == PLAYER || v[i] == COMPUTER) ? 1 : 0;
				empty += (v[i] == 0) ? 1 : 0;
			}
			// bad was calculated as (bad + good), so remove good
			bad -= good;
			return heurFunction(good, bad, empty);
		}
		private static int heurFunction(int g, int b, int z)
		{
			int score = 0;
			if (g == 4) { score += 500001; } // preference to go for winning move vs. block
			else if (g == 3 && z == 1) { score += 5000; }
			else if (g == 2 && z == 2) { score += 500; }
			else if (b == 2 && z == 2) { score -= 501; } // preference to block
			else if (b == 3 && z == 1) { score -= 5001; } // preference to block
			else if (b == 4) { score -= 500000; }
			return score;
		}




	}
}
