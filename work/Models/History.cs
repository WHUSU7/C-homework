using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work.Models
{
    public  class History
    {
        public int id { get; set; }
        public string content { get; set; }
        public string matchTime { get; set; }
        public string matchType { get; set; }
        public string isWin { get; set; }
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
