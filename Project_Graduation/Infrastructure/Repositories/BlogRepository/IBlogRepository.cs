using System;
using Infrastructure.Entities;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories.BlogRepository;

public interface IBlogRepository: IBaseRepository<Blog>
{

}