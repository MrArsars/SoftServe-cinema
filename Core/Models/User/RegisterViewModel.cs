using System.ComponentModel.DataAnnotations;

namespace Core.Models.User;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }
    
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
 
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
 
    [Required]
    [Compare("Password", ErrorMessage = "Паролі не співпадають")]
    [DataType(DataType.Password)]
    [Display(Name = "Підтвердити пароль")]
    public string PasswordConfirm { get; set; }
}