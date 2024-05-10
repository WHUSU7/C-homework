using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace work
{
    public  static class Utils
    {
        public static int getIndex(double inch,double pos) {
          
            int res = 1;
            while (inch*res <=pos) {
                res++;
            }
            return res-1;
        }
    }
}
