using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackit.Components.Pages
{
    public enum TransactionType
    {
        Credit,
        Debt,
        Debit
    }

    public class Transaction
    {
        public TransactionType TransactionType { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Tags { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }

        //for debts

        public DateTime? DueDate { get; set; }
        public string Source { get; set; }

        public decimal RemainingAmount { get; set; }  
        public bool IsClear { get; set; }
    }





}
