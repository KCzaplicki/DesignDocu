using MediatR;

namespace DesignDocu.Common.Application.Queries;

public interface IQuery<out TResult> : IRequest<TResult>;