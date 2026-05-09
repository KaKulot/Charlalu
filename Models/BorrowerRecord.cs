using System;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public class BorrowRecord
    {
        [Key]
        public int BorrowId { get; set; }

        [Required(ErrorMessage = "Borrower name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Borrower Name")]
        public string? BorrowerName { get; set; }

        [Required(ErrorMessage = "Book title is required.")]
        [StringLength(200, ErrorMessage = "Book title cannot exceed 200 characters.")]
        [Display(Name = "Book Title")]
        public string? BookTitle { get; set; }

        [Required(ErrorMessage = "Borrow date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(20)]
        public string Status { get; set; } = "Borrowed";
    }
}
