using System.ComponentModel.DataAnnotations;

namespace Company.ClientName.PL.Dtos
{
    public class CreateDepartmentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is Required !")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is Required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CreateAt is Required !")]
        public DateTime CreateAt { get; set; }
    }
}
