using System;
using System.Collections.Generic;
using System.Text;

namespace ChikwamaWallet.Models
{
    public class WalletModel
    {
        public string TokenName { get; set;}
        public string TokenAddress { get; set; }
        public string TokenSymbol { get; set; }
        public decimal TokenBalance { get; set; }
    }
}
