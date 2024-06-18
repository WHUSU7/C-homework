using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work.Models
{
    public class mainWindowPositon
    {

       public double mainWindowLeft;
        public double mainWindowTop;
        public double mainWindowHeight;
        public double mainWindowWidth;

        public mainWindowPositon(double mainWindowLeft, double mainWindowTop, double mainWindowHeight, double mainWindowWidth)
        {
            this.mainWindowLeft = mainWindowLeft;
            this.mainWindowTop = mainWindowTop;
            this.mainWindowHeight = mainWindowHeight;
            this.mainWindowWidth = mainWindowWidth;
        }
    }
}
