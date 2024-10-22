using Application.Common.Models;
using MediatR;

namespace Application.Common.Interfaces;

public interface IRequestWrapper<T> : IRequest<ServiceResult<T>>
{

}

public interface IRequestWrapper : IRequest<ServiceResult>
{

}



public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, ServiceResult<TOut>> where TIn : IRequestWrapper<TOut>
{

}

public interface IRequestHandlerWrapper<TIn> : IRequestHandler<TIn, ServiceResult> where TIn : IRequestWrapper
{

}