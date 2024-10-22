using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ViewModels;
using Domain.Entity.Repositories;

namespace Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(int Id) : IRequestWrapper;

public class DeleteProductCommandHandler(IProductRepository productRepository) : BaseHandler, IRequestHandlerWrapper<DeleteProductCommand>
{
    public async Task<ServiceResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
            return NotFound<ProductViewModel>("Product", request.Id, null);

        await productRepository.RemoveAsync(product);
        await productRepository.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success("");
    }
}