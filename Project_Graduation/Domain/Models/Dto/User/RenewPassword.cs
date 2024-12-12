using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.User
{
    public class RenewPassword
    {
        public string PassWord { get; set; }
        public string TokenRenew { get; set; }
    }
}
