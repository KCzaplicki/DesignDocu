using MediatR;

namespace DesignDocu.Common.Application.Commands;

public interface ICommand<out TResult> : IRequest<TResult>;

public interface ICommand : IRequest;