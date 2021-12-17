using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer
{
    public class UserCategoryMapTable
    {
        [Key]
        public long Id { get; set; }
        public User Users { get; set; }
        public Category Categories { get; set; }
    }
}
