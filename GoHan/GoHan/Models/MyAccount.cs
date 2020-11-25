using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoHan.Models
{
    public class MyAccount
    {
		[Required]
		[StringLength(20, ErrorMessage = "帳號不能大於20碼!")]
		[Display(Name = "帳號")]
		public string UserName { get; set; }
		[Required]
		[StringLength(64, ErrorMessage = "密碼不能大於64碼!")]
		[Display(Name = "密碼")]
		public string Password { get; set; }
	}
}
