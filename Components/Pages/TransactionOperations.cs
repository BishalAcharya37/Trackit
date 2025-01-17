using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Trackit.Components.Pages
{
    class TransactionOperations
    {
       //filepath to store transaction 
        private static string filePath = @"C:\Users\Achar\source\repos\Trackit\transactions.json";

        // method to check if the directory exists or not
        public static void CheckDirectory()
        {
            var directory = Path.GetDirectoryName(filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);  // Create directory if it doesn't exist
            }
        }

        // Method to read the JSON file and load transactions
        public static async Task<List<Transaction>> LoadTransactionsAsync()
        {
            CheckDirectory();  // Ensure the directory exists before reading the file

            if (File.Exists(filePath))
            {
                var json = await File.ReadAllTextAsync(filePath);
                return JsonConvert.DeserializeObject<List<Transaction>>(json) ?? new List<Transaction>();
            }

            return new List<Transaction>(); // Return empty list if the file does not exist
        }

        // Method to calculate the total forall type of transactions
        public static decimal GetTotal(List<Transaction> transactions, TransactionType type)
        {
            return transactions.Where(t => t.TransactionType == type).Sum(t => t.Amount);
        }


        public static async Task SaveTransactionsAsync(List<Transaction> transactions)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);  // Ensure the directory exists
            }

            var json = JsonConvert.SerializeObject(transactions, Formatting.Indented);
            await File.WriteAllTextAsync(filePath, json);
        }

        // Method to calculate the total balance
        public static decimal GetTotalBalance(List<Transaction> transactions)
        {
            decimal totalCredit = GetTotal(transactions, TransactionType.Credit);
            decimal totalDebit = GetTotal(transactions, TransactionType.Debit);
            decimal totalDebt = GetTotal(transactions, TransactionType.Debt);

            return (totalCredit + totalDebt) - totalDebit;
        }
      

        // Method to calculate the remaining debt 
        public static decimal GetRemainingDebt(List<Transaction> transactions)
        {
            return transactions.Where(t => t.TransactionType == TransactionType.Debt).Sum(t => t.RemainingAmount);
        }

        // Get pending debts i.e. IsClear false
        public static List<Transaction> GetPendingDebts(List<Transaction> transactions)
        {
            return transactions
                .Where(t => t.TransactionType == TransactionType.Debt && !t.IsClear)
                .ToList();
        }

        public static List<Transaction> GetClearDebts(List<Transaction> transactions)
        {
            return transactions
                .Where(t => t.TransactionType == TransactionType.Debt && t.IsClear)
                .ToList();
        }


        public static int GetClearedDebtsCount(List<Transaction> transactions)
        {
            return transactions.Count(t => t.TransactionType == TransactionType.Debt && t.IsClear);
        }

        public static List<Transaction> GetTopTransactions(List<Transaction> transactions, int count = 5)
        {
            return transactions
                .OrderByDescending(t => t.Amount)
                .Take(count)
                .ToList();
        }
        public static List<Transaction> GetLowTransactions(List<Transaction> transactions, int count = 5)
        {
            return transactions
                .OrderBy(t => t.Amount)
                .Take(count) 
                .ToList();
        }


    }






}