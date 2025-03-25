using System.ComponentModel.DataAnnotations;

namespace Company.ClientName.PL.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "UserName is Required !!")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "FristName is Required !!")]
        public string FristName { get; set; }


        [Required(ErrorMessage = "LastName is Required !!")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)] // To Write Password As ****
        public string Password { get; set; }

        [DataType(DataType.Password)] 
        [Required(ErrorMessage = "Confirm Password is Required !!")]
        [Compare(nameof(Password), ErrorMessage ="confirm Password does not match the password !!")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }

    }
}
