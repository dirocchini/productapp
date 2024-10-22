using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ViewModels;
using AutoMapper;
using Domain.Entity.Repositories;

namespace Application.Categories.Queries.GetAllCategories;

public sealed record GetAllCategoriesQuery : IRequestWrapper<List<CategoryViewModel>>;

public class GetAllCategoriesQueryHandler (ICategoryRepository categoryRepository, IMapper mapper): BaseHandler, IRequestHandlerWrapper<GetAllCategoriesQuery, List<CategoryViewModel>>
{
    public async Task<ServiceResult<List<CategoryViewModel>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync(cancellationToken);
        return ServiceResult.Success(mapper.Map<List<CategoryViewModel>>(categories));
    }
}
