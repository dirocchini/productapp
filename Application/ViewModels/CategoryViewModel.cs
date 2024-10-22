using Application.AutoMapper;
using Domain.Entity.Database;

namespace Application.ViewModels;

public class CategoryViewModel : IMapProfile<CategoryEntity, CategoryViewModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
