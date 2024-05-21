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

        public History(int id, string content)
        {
            this.id = id;
            this.content = content;
        }
    }
}
