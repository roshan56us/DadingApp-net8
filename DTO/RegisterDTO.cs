using System.ComponentModel.DataAnnotations;

namespace API.DTO;

public class RegisterDTO
{

[Required]
//[MaxLength(100)]
public required string Username { get; set; }
public required string Password {get;set;}
}
