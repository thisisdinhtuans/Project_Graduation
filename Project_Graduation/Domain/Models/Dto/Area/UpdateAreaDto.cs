using System;

namespace Domain.Models.Dto.Area;

public class UpdateAreaDto
{
    public string AreaName { get; set; }=string.Empty;
        public int RestaurantID { get; set; }
}
