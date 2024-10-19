using System;

namespace Domain.Models.Dto.Blog;

public class CreateBlogDto
{
        public string? Image {get;set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }    
}
