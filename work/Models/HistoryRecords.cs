using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work.Models
{
    public class HistoryRecords
    {
        public string record;
        public string[] arrRecord;
        public HistoryRecords(string record)
        {
            this.record = record;
            arrRecord= Utils.SplitStringIntoPairs(this.record);
        }
        public void addRecord(string record)
        {
            this.record += record;
            this.arrRecord = Utils.SplitStringIntoPairs(this.record);
        }
        public void clearRecord()
        {
            this.record = "";
            this.arrRecord = null;
        }
    }
}
