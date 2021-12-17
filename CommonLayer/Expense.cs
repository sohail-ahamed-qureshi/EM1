using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class ExpenseTable
    {
        [Key]
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime? Date { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public string Note { get; set; }
    }
}
