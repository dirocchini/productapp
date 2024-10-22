using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ViewModels;
using AutoMapper;
using Domain.Entity.Repositories;
using System.Text.Json.Serialization;

namespace Application.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommand : IRequestWrapper<ProductViewModel>
    {
        [JsonIgnore]
        public int ProductId { get; set; }

        public string Name { get; set; }
        public decimal Value { get; set; }
    }

    public class UpdateProductCommandHandler(IProductRepository productRepository, IMapper _mapper) : BaseHandler, IRequestHandlerWrapper<UpdateProductCommand, ProductViewModel>
    {
        public async Task<ServiceResult<ProductViewModel>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);

            if (product == null)
                return NotFound<ProductViewModel>("Product", request.ProductId, null);

            if (request.Name != product.Name)
                product.Name = request.Name;

            if (request.Value != product.Value)
                product.Value = request.Value;

            var validations = product.Validate();

            if (!validations.IsValid)
                return BadRequest<ProductViewModel>(validations, null);

            await productRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ProductViewModel>(product));
        }
    }
}
