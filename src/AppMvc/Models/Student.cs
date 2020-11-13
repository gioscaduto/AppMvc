using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppMvc.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(100, ErrorMessage = "Name cannot be greater than 100 characters")]
        public string Name { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "E-mail invalid")]
        public string Email { get; set; }

        [DisplayName("Registration Date")]
        public DateTime RegistrationDate { get; set; }

        public bool Active { get; set; }
    }
}