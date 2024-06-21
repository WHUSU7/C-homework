using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work.Models
{
    public  class History
    {
        public int id;
        public string content;
        public string matchTime;
        public string matchType;
        public string isWin;
        public History(int id, string content, string matchTime, string matchType, string isWin)
        {
            this.id = id;
            this.content = content;
            this.matchTime = matchTime;
            this.matchType = matchType;
            this.isWin = isWin;
        }
    }
}
