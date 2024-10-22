using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ViewModels;
using AutoMapper;
using Domain.Entity.Repositories;

namespace Application.Products.Queries.GetAllProducts;

public sealed record GetAllProductsQuery : IRequestWrapper<List<ProductViewModel>>;

public class GetAllProductsQueryHandler(IProductRepository productRepository, IMapper _mapper) : IRequestHandlerWrapper<GetAllProductsQuery, List<ProductViewModel>>
{
    public async Task<ServiceResult<List<ProductViewModel>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllWithCategoriesAsync(cancellationToken);

        return ServiceResult.Success(_mapper.Map<List<ProductViewModel>>(products));
    }
}