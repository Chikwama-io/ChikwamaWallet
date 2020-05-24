using ChikwamaWallet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


using Nethereum.JsonRpc.Client;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.StandardTokenEIP20.Events.DTO;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Contracts;
using Nethereum.StandardTokenEIP20;
using Nethereum.StandardTokenEIP20.ContractDefinition;
using System.Linq;
using System.Numerics;

namespace ChikwamaWallet.Services
{
    public interface IAccountManager
    {
        string DefaultAccountAddress { get; }

        //Task<string[]> GetAccountsAsync();

        Task<decimal> GetTokensAsync(string accountAddress);
        Task<decimal> GetBalanceInETHAsync(string accountAddress);

        Task<TransactionModel[]> GetTransactionsAsync(bool sent = false);

        Task<string> TransferAsync(string from, string to, decimal amount);
    }
    public class AccountManager : IAccountManager
    {

        const string CONTRACT_ADDRESS = "0x5592ec0cfb4dbc12d3ab100b257153436a1f0fea";

        private readonly IWeb3ProviderService _web3ProviderService;
        

        private string DefaultAccount => controller.GetAddress();
        public string DefaultAccountAddress => DefaultAccount;
        readonly NewWalletController controller;
        StandardTokenService DaiTokenService;
        Web3 web3;
        //RpcClient client;

        public AccountManager(NewWalletController controller)
        {
            this.controller = controller;
            _web3ProviderService = new Web3ProviderService();
            var account = controller.GetPKey();
            web3 = _web3ProviderService.GetWeb3(account);
            DaiTokenService = new StandardTokenService(web3, CONTRACT_ADDRESS);
        }

        /*
        public async Task<string[]> GetAccountsAsync()
        {
            return new string[] { DefaultAccountAddress };
        }

        */

        public async Task<decimal> GetTokensAsync(string accountAddress)
        {
            

            var wei = await DaiTokenService.BalanceOfQueryAsync(accountAddress);

            return Web3.Convert.FromWei(wei);
        }
        
        public async Task<decimal> GetBalanceInETHAsync(string accountAddress)
        {
            var balanceInWei = await web3.Eth.GetBalance.SendRequestAsync(accountAddress);

            return Web3.Convert.FromWei(balanceInWei.Value);
        }

        
        public async Task<string> TransferAsync(string from, string to, decimal amount)
        {            
            var sendAmount = Web3.Convert.ToWei(amount);
            var receipt = await DaiTokenService.TransferRequestAndWaitForReceiptAsync(to, sendAmount);
            return receipt.TransactionHash;
        }

        public async Task<TransactionModel[]> GetTransactionsAsync(bool sent = false)
        {

            var transferEventLogs = new List<EventLog<TransferEventDTO>>();

            var web3 = new Web3("https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c");

            Task StoreLogAsync(EventLog<TransferEventDTO> eventLog)
            {
                transferEventLogs.Add(eventLog);
                return Task.CompletedTask;
            }
            //create our processor to retrieve transfers
            //restrict the processor to Transfers
            var processor = web3.Processing.Logs.CreateProcessorForContract<TransferEventDTO>(
            CONTRACT_ADDRESS.ToLower(), StoreLogAsync);

            //if we need to stop the processor mid execution - call cancel on the token
            var cancellationToken = new System.Threading.CancellationToken();
            var blocknumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            var StartofWeek = blocknumber - new BigInteger(500);
            //crawl the required block range
            await processor.ExecuteAsync(
                toBlockNumber: blocknumber,
                cancellationToken: cancellationToken,
                startAtBlockNumberIfNotProcessed: StartofWeek);



            foreach (var eventlog in transferEventLogs)
            
            {
                if (eventlog.Event.To.ToString() == DefaultAccountAddress.ToLower() || eventlog.Event.From.ToString() == DefaultAccountAddress.ToLower())
                {
                    //Console.WriteLine("To: " + eventlog.Event.To + " From: " + eventlog.Event.From + " Value: US$" + Web3.Convert.FromWei(eventlog.Event.Value) + " (DAI)");

                    var block = await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(eventlog.Log.BlockNumber);

                    TransactionModel[] mytransactions = new TransactionModel[]
                     {
                             new TransactionModel{
                            Sender = eventlog.Event.From,
                            Receiver = eventlog.Event.To,
                            Amount = Web3.Convert.FromWei(eventlog.Event.Value),
                            Inward = eventlog.Event.To.ToString() == DefaultAccountAddress.ToLower(),
                            Timestamp = (long)block.Timestamp.Value
                             }
                    };

                    return mytransactions;
                    
                }
            }
            return null;
        }
        
        void Initialize()
        {
            //var client = new RpcClient(new Uri("http://192.168.12.154:8545"));//iOS
            //var client = new RpcClient(new Uri("http://10.0.2.2:8545"));//ANDROID
            //client = new RpcClient(new Uri("https://rinkeby.infura.io/v3/2996efcb421343b38766a7834ff07a9a"));
            //var url = "http://rinkeby.infura.io/v3/2996efcb421343b38766a7834ff07a9a";
            //web3 = _web3ProviderService.GetWeb3();
            //web3 = new Web3(DefaultAccount, client);
            //standardTokenService = new StandardTokenService(web3, CONTRACT_ADDRESS);
            
        }
    }
}