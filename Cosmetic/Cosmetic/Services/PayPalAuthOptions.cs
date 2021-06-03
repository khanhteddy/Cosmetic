using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMarket.Services.PayPal
{
    public class PayPalAuthOptions
    {
        public string PayPalClientID { get; set; }
        public string PayPalSecret { get; set; }
        public string PayeeEmail { get; set; }
        public string PayeeId { get; set; }
    }
}
