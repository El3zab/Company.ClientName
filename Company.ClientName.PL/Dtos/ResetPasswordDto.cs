using System.ComponentModel.DataAnnotations;

namespace Company.ClientName.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password is Required !!")]
        [DataType(DataType.Password)] // To Write Password As ****
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is Required !!")]
        [Compare(nameof(NewPassword), ErrorMessage = "confirm Password does not match the password !!")]
        public string ConfirmPassword { get; set; }
    }
}
