using CommonLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class ExpenseManagerContext : DbContext
    {
        public ExpenseManagerContext( DbContextOptions<ExpenseManagerContext> options ): base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategoryMapTable> UserCategoryMap { get; set; }

    }
}
 