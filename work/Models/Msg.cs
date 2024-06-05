using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work.Models
{
    public class Msg
    {

        public string msg;
        public string turn;
        public string receiveTurn;
        public bool isDropChess;
        public Msg(string msg, string turn, string receiveTurn, bool isDropChess)
        {
            this.msg = msg;
            this.turn = turn;
            this.receiveTurn = receiveTurn;
            this.isDropChess = isDropChess;
        }
    }
}
