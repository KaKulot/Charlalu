using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;

namespace LibrarySystem.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowRecord>().HasData(
                new BorrowRecord
                {
                    BorrowId = 1,
                    BorrowerName = "Juan dela Cruz",
                    BookTitle = "Introduction to Programming",
                    BorrowDate = new System.DateTime(2026, 4, 20),
                    ReturnDate = new System.DateTime(2026, 4, 27),
                    Status = "Returned"
                },
                new BorrowRecord
                {
                    BorrowId = 2,
                    BorrowerName = "Maria Santos",
                    BookTitle = "Database Management Systems",
                    BorrowDate = new System.DateTime(2026, 4, 28),
                    ReturnDate = null,
                    Status = "Borrowed"
                },
                new BorrowRecord
                {
                    BorrowId = 3,
                    BorrowerName = "Pedro Reyes",
                    BookTitle = "Web Development with ASP.NET",
                    BorrowDate = new System.DateTime(2026, 5, 1),
                    ReturnDate = null,
                    Status = "Borrowed"
                }
            );
        }
    }
}
