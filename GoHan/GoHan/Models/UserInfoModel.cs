using System.ComponentModel.DataAnnotations;

namespace GoHan.Models
{
    public class UserInfoModel
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
