using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buma.UI.ViewModels.Admin
{
    public class CreateUserViewModel
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}