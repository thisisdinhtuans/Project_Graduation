using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.Data;

public class ProjectGraduationContextFactory : IDesignTimeDbContextFactory<Project_Graduation_Context>
{
    public Project_Graduation_Context CreateDbContext(string[] args = null)
    {
        var optionsBuilder = new DbContextOptionsBuilder<Project_Graduation_Context>();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlConnection"));

        return new Project_Graduation_Context(optionsBuilder.Options);
    }
}
