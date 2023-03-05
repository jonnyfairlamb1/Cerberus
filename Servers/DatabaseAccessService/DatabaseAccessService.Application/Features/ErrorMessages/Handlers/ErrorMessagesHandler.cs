using CommonData.DTOs;
using DatabaseAccessService.Application.Features.ErrorMessages.Requests;
using DatabaseAccessService.Domain.Services.Interfaces;
using MediatR;

namespace DatabaseAccessService.Application.Features.ErrorMessages.Handlers {

    public class ErrorMessagesHandler : IRequestHandler<ErrorMessagesRequest, ErrorMessagesDTO> {
        private readonly IServerManagerService _serverManagerService;

        public ErrorMessagesHandler(IServerManagerService serverManagerService) {
            _serverManagerService = serverManagerService;
        }

        public async Task<ErrorMessagesDTO> Handle(ErrorMessagesRequest request, CancellationToken cancellationToken) {
            ErrorMessagesDTO dto = new();
            dto.ErrorMessages = await _serverManagerService.GetErrorMessages();
            return dto;
        }
    }
}