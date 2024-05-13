using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace work
{
    public static class Board
    {
        private static int[,] board = { 
           
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0},
        };
        

        //单例模式，确保只有一个board
        public static int[,] getBoardInstance() { 
            return board;
        }
        
        

        //判断胜利逻辑，看x,y附近是否四连
        //白字 turn =0 +1表示
        //黑子 turn =1 -1表示
        public static bool IsWin(int x,int y,int turn)
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


        //AI落子逻辑
        public static Tuple<int, int> NextMove(int nowturn)
        {
            // 先检查是否存在对方离胜利仅差一子的情况，如果有则堵上
            Tuple<int, int> blockingMove = FindBlockingMove(nowturn);
            if (blockingMove != null)
            {
                return blockingMove;
            }

            Tuple<int, int> nearbyMove = FindNearbyMove(nowturn);
            if (nearbyMove != null)
            {
                return nearbyMove;
            }

            // 最后选择下一步可以避免给对方搭桥的位置
            return FindSafeMove(nowturn);
        }

        //找最近的下一个点
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


        // 寻找需要堵上的位置
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

        // 判断是否需要认输
        private static bool ShouldResign(int nowturn)
        {
            List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();
            for (int col = 0; col < board.GetLength(1); col++)
            {
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    if (IsValidMove(row, col))
                    {
                        possibleMoves.Add(Tuple.Create(row, col));
                    }
                }
            }
            return possibleMoves.Count <= 2; // 如果对方有2个以上的落子点可以获胜，则返回 true
        }

        // 寻找安全的位置
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

        // 判断当前位置是否是合法的落子位置
        private static bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < board.GetLength(0) && col >= 0 && col < board.GetLength(1) && board[row, col] == 0 && (row == board.GetLength(0) - 1 || (row < board.GetLength(0) - 1 && board[row + 1, col] == 1 || board[row + 1, col] == -1));
        }

    }
}
