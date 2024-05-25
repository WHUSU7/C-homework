using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work.Models
{
    public class User
    {
        public int id;
        public string name;
        public string password;
        public string nickname;

        public User(int id, string name, string password, string nickname)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.nickname = nickname;
        }
    }
}
