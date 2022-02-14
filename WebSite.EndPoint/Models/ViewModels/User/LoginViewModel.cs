using System.ComponentModel.DataAnnotations;

namespace WebSite.EndPoint.Models.ViewModels.User
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "لطفا ایمیل خود را وارد کنید")]
        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [Required(ErrorMessage ="لطفا پسورد خود را وارد کنید")]
        [Display(Name = "پسورد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "RememberMe")] 
         public bool IsPersistent { get; set; } = false;


        public string ReturnUrl { get; set; }
    }


}
