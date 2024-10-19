using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Table
{
    public class CreateTableDto
    {
        public int TableNumber { get; set; }
        public int Status { get; set; }
        public string NumberOfDesk { get; set; }
        public int AreaID { get; set; }
    }
}
