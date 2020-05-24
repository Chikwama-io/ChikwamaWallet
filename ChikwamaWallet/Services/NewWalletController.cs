using NBitcoin;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChikwamaWallet.Services
{
    public class NewWalletController
    {
        readonly IWalletManager walletManager;

        string passcode;
        Mnemonic seed;
        string recipient;

        public NewWalletController(IWalletManager walletManager)
        {
            this.walletManager = walletManager;
        }

        public async Task CreateWalletAsync()
        {
            await walletManager.CreateWalletAsync();
            SetSeed(walletManager.Mnemo);
        }

        public async Task SaveWalletAsync()
        {
            await walletManager.SaveWalletAsync(passcode);
        }

        public async Task<bool> UnlockWallet(string pin)
        {
            bool x = await walletManager.UnlockWalletAsync(pin);
            return x;
        }

        public string[] GetSeedWords()
        {
            return seed.Words;
        }

        public string GetAddress()
        {
            var address = walletManager.Account;
            return address;
        }

        public string GetReciever()
        {
            if (recipient != null)
                return recipient;
            else
                return "";
        }

        public Account GetPKey()
        {
            return walletManager.PrivateKey;
        }

        public IWalletManager GetWalletManager()
        {
            return walletManager;
        }
        public void SetPasscode(string passcode)
        {
            this.passcode = passcode;
        }

        public void SetRecipient(string reciever)
        {
            this.recipient = reciever;
        }

        public void SetSeed(Mnemonic mnemo)
        {
            this.seed = mnemo;
        }

     
        public bool VerifyPasscode(string passcodeConfirmation)
        {
            return string.Equals(passcode, passcodeConfirmation);
        }
    }
}
