namespace EmployeeCRRUDAJAX.ViewModel
{
    public class UserMstViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public DateTime Dob { get; set; }

        public string MobileNo { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public IFormFile? Profilephoto { get; set; }
    }
}
