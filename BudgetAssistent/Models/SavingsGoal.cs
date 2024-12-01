using System.ComponentModel.DataAnnotations;

namespace BudgetAssistent.Models
{
    public class SavingsGoal
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Currency)]
        public double TargetAmount { get; set; }
        [Required]
        public DateTime TargetDate { get; set; }
        public double CurrentAmount { get; set; }
    }
}
