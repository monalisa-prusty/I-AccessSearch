using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IAccessSearch.Models;

namespace IAccessSearch.Models
{
    public class SearchStringContext : DbContext
    {
        public SearchStringContext(DbContextOptions options) : base(options)//taking options from base
        {
        }
        public DbSet<TSearchString> TSearchStrings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TSearchString>().HasData(
                new TSearchString()
                {
                    ID = "3AE69BBF-52FB42AF-8310-DFAAE0C6296A",
                    Content = "Sample Record for nuget migration"
                }
            );
        }
    }
}
