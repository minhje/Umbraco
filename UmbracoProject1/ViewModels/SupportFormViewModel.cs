using System.ComponentModel.DataAnnotations;

namespace UmbracoProject1.ViewModels;

public class SupportFormViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email adress")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email adress")]
    public string Email { get; set; } = null!;
}
