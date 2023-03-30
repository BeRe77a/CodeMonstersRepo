using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMonstersSanityCheck.Models.Request
{
    public class Payment_transaction
    {
        public string card_number { get; set; }
        public string cvv { get; set; }
        public string expiration_date { get; set; }
        public double amount { get; set; }
        public string usage { get; set; }
        public string transaction_type { get; set; }
        public string reference_id { get; set; }
        public string card_holder { get; set; }
        public string email { get; set; }
        public string address { get; set; }
    }

    public class Payment
    {
        public Payment_transaction payment_transaction { get; set; }
    }


}
