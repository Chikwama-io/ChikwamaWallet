using System;
using System.Collections.Generic;
using System.Text;


namespace ChikwamaWallet.Services
{

    using NBitcoin;
    using KeyPath = NBitcoin.KeyPath;
    using Nethereum.HdWallet;
    using Nethereum.Hex.HexConvertors.Extensions;
    using System.Threading.Tasks;
    using Nethereum.KeyStore;
    using Nethereum.KeyStore.Model;
    using Java.Security;
    using System.IO;
    using Nethereum.Util;
    using Newtonsoft.Json;
    using Nethereum.Signer;
    using Nethereum.Web3.Accounts;

    public interface IWalletManager
    {
        string Account { get; }

        Account PrivateKey { get; }
        Mnemonic Mnemo { get; }
        Task CreateWalletAsync();

        Task SaveWalletAsync(string password);

        Task<bool> UnlockWalletAsync(string password);

        Task<bool> RestoreWallet(string seedWords, string password);
    }

    public class WalletManager : IWalletManager
    {
        const string PASSWORD_KEY = "__wallet__password__";
        const string SEED_KEY = "__wallet__seed__";
        const string SEED_PASSWORD = null;
        const string DEFAULT_PATH = "m/44'/60'/0'/0/x";
        Mnemonic mnemo;
        private string account;
        private Account pkey;

        public Account PrivateKey => pkey;
        public Mnemonic Mnemo => mnemo;
        public string Account => account;
        public string Seed { get; private set; }
        public string myPath { get; }

        public string JSON;
        IDictionary<string, string> Keystore;

        //readonly ISecureStorage secureStorage;


        public WalletManager()
        {
            Keystore = new Dictionary<string, string>();
            myPath = DEFAULT_PATH;
        }

        public Task CreateWalletAsync()
        {
            return Task.Run(delegate
            {
                mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);
            });
        }

        public Task SaveWalletAsync(string password)
        {
            return Task.Run(delegate
            {
                if (mnemo == null) return;

                StoreCredentials(mnemo.ToString(), password);
            });
        }

        public Task<bool> UnlockWalletAsync(string password)
        {
            return Task.Run(delegate
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string filePath = Path.Combine(path, "info.json");
                if (File.Exists(filePath))
                {
                    JSON = "";
                    JSON = File.ReadAllText(filePath);
                    this.Keystore = JsonConvert.DeserializeObject<Dictionary<string,string>>(JSON);

                    string mymnemo;

                    if (Keystore.TryGetValue(password,out mymnemo))
                    {
                        mnemo = new Mnemonic(mymnemo, Wordlist.English);
                        Seed = mnemo.DeriveSeed(password).ToHex();
                        var EthKey = GetEthereumKey(1);
                        pkey = GetAccount(1);
                        var address = EthKey.GetPublicAddress();
                        account = address;
                    }
                    else
                    {
                        return false;
                    }

                    return true;
                }
                return false;
            });

        }

        public Task<bool> RestoreWallet(string seedWords, string password)
        {
            return Task.Run(delegate
            {
                //wallet = new Wallet(seedWords, SEED_PASSWORD);

                //var storedSeed = secureStorage.GetValue(wallet.GetAccount(0).Address);
                return false;
                //return false == string.IsNullOrWhiteSpace(storedSeed);
            });
        }


        void StoreCredentials(string mnemo, string password)
        {
            Mnemonic mymnemo = new Mnemonic(mnemo);
            Seed = mymnemo.DeriveSeed(password).ToHex();
            var EthKey = GetEthereumKey(1);
            pkey = GetAccount(1);
            var address = EthKey.GetPublicAddress();
            account = address;

            Keystore.Add(password, mnemo);
            
            JSON = JsonConvert.SerializeObject(Keystore, Formatting.Indented);

            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, "info.json");
            using (var file = File.Open(filePath, FileMode.Create, FileAccess.Write))
            using (var strm = new StreamWriter(file))
            {
                strm.Write(JSON);
            }

        
        }

        private string GetIndexPath(int index)
        {
            return myPath.Replace("x", index.ToString());
        }
        private ExtKey GetKey(int index)
        {
            var masterKey = new ExtKey(Seed);
            var keyPath = new KeyPath(GetIndexPath(index));
            return masterKey.Derive(keyPath);
        }

        public byte[] GetPrivateKey(int index)
        {
            var key = GetKey(index);
            return key.PrivateKey.ToBytes();
        }
        private EthECKey GetEthereumKey(int index)
        {
            var privateKey = GetPrivateKey(index);
            return new EthECKey(privateKey, true);
        }

        public byte[] GetPublicKey(int index)
        {
            var key = GetEthereumKey(index);
            return key.GetPubKey();
        }

        public Account GetAccount(int index)
        {
            var privateyKey = GetPrivateKey(index);
            if (privateyKey != null)
                return new Account(privateyKey);
            return null;
        }
    }
}
