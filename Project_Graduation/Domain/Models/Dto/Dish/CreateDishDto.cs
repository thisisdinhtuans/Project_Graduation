using System;

namespace Domain.Models.Dto.Dish;

public class CreateDishDto
{
        public string Name { get; set; }=string.Empty;
        public double Price { get; set; }   
        public string Description { get; set; }=string.Empty;
        public string Type { get; set; }=string.Empty;
        public string Image { get; set; }=string.Empty;
        public int CategoryID { get; set; }
}
