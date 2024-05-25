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
        public Msg(string msg, string turn)
        {
            this.msg = msg;
            this.turn = turn;
        }
    }
}
