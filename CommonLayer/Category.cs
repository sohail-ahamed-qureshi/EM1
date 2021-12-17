using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class Category
    {
        [Key]
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDefault { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

}
