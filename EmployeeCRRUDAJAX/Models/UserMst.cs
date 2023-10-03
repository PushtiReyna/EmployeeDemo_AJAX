using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCRRUDAJAX.Models;

public partial class UserMst
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter Firstname")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter lastname")]
    public string LastName { get; set; } = null!;


    [Required(ErrorMessage = "Please Select Gender.")]
    public string Gender { get; set; } = null!;

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Please enter Date Of Birth")]
    public DateTime Dob { get; set; }


    [Required(ErrorMessage = "Please Enter Phone No.")]
    [MinLength(10, ErrorMessage = "The Phone No. must be at least 10 characters")]
    [MaxLength(10, ErrorMessage = "The Phone No. cannot be more than 10 characters")]
    public string MobileNo { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter username")]
    public string UserName { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please Enter password")]
    public string Password { get; set; } = null!;


    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Please Enter Email.")]
    [EmailAddress(ErrorMessage = "Please enter valid email Address.")]
    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    [Required(ErrorMessage = "Please select profile photo")]
    public string? Profilephoto { get; set; }

    [NotMapped]
    public IFormFile ProfileImage { get; set; }
}
