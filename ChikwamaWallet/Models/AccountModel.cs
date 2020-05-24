using System;
using System.Collections.Generic;
using System.Text;

namespace ChikwamaWallet.Models
{
    public class AccountModel
    {
        public string AccountName { get; set; }
        public string DefaultPubKey { get; set; }
        public bool isAgent { get; set; }
        public bool isFullAgent { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
