using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace BudgetAssistent.Models
{
    public class Expenses
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public ExpensesType Type { get; set; }
        public ExpensesFrequency Frequency { get; set; }
    }

    public enum ExpensesType
    {
        Income,
        Expense
    }


    public enum ExpensesFrequency
    {
        OneTime,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
