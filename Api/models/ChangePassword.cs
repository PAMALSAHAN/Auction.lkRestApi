using System.ComponentModel.DataAnnotations;

namespace Api.models
{
    public class ChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        [Compare("NewPassword",ErrorMessage="The password and the confirm password doesnot match")]
        public string ConfirmPassword { get; set; }

    }
}