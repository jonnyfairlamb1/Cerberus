using CommonData.DTOs;
using MediatR;

namespace DatabaseAccessService.Features.ErrorMessages.Requests {

    public sealed class ErrorMessagesRequest : IRequest<ErrorMessagesDTO> {
    }
}