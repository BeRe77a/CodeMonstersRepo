using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMonstersSanityCheck.Models.Response
{
    public class TransactionResponse
    {
        public string unique_id { get; set; }
        public string status { get; set; }
        public string usage { get; set; }
        public double amount { get; set; }
        public DateTime transaction_time { get; set; }
        public string message { get; set; }
    }
}
