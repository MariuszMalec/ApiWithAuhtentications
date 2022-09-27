using System.ComponentModel.DataAnnotations;

namespace WepAppAccessToApi.Models
{
    public class AuthenticationBearerModel
    {
        //[Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
