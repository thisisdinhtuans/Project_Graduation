using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Table
{
    internal class UpdateTableDto
    {
        public int TableNumber { get; set; }
        public int Status { get; set; }
        public string NumberOfDesk { get; set; }
        public int AreaID { get; set; }
    }
}
