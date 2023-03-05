using CommonData.DTOs;
using MediatR;

namespace DatabaseAccessService.Application.Features.ErrorMessages.Requests {

    public sealed class ErrorMessagesRequest : IRequest<ErrorMessagesDTO> {
    }
}