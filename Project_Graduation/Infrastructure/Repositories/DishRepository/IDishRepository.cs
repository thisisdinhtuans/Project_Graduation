using System;
using Infrastructure.Entities;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories.DishRepository;

public interface IDishRepository: IBaseRepository<Dish>
{

}