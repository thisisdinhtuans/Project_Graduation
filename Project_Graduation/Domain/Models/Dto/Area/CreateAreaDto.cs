using System;

namespace Domain.Models.Dto.Area;

public class CreateAreaDto
{
        public string AreaName { get; set; }=string.Empty;
        public int RestaurantID { get; set; }
}
