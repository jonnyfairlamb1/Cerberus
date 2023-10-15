using CommonData.DTOs;
using DatabaseAccessService.Domain.Services.Interfaces;
using DatabaseAccessService.Features.ErrorMessages.Requests;
using MediatR;

namespace DatabaseAccessService.Features.ErrorMessages.Handlers {

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