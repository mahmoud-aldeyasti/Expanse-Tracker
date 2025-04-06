using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseNavigation.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Range(1 , int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid amount greater than zero.")]
        public int Amount { get; set; }
        [Column( TypeName ="nvarchar(75)")]
        public string? Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]

        public string? TitleWithIcon
        {
            get
            {
                return this.Category == null ? "" : Category.Title +" " + Category.Icon;
            }
        }


        [NotMapped]

        public string AmountWithPrefix
        {
            get
            {
                return (this.Category == null || this.Category.Type == "Expense")? "-" + this.Amount
                    : "+" + this.Amount;

            }
        }
    }
}
