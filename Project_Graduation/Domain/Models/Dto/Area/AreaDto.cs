using System;

namespace Domain.Models.Dto.Area;

public class AreaDto
{
        public int AreaID { get; set; }
        public string AreaName { get; set; }=string.Empty;
        public int RestaurantID { get; set; }
}
