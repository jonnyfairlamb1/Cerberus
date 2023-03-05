using CommonData.DTOs;
using DatabaseAccessService.Application.Features.CharacterData.Requests;
using DatabaseAccessService.Domain.Services.Interfaces;
using MediatR;

namespace DatabaseAccessService.Application.Features.CharacterData.Handlers {

    public class CharacterDataHandler : IRequestHandler<CharacterDataRequest, CharacterDataDTO> {
        private readonly IServerManagerService _serverManagerService;

        public CharacterDataHandler(IServerManagerService serverManagerService) {
            _serverManagerService = serverManagerService;
        }

        public async Task<CharacterDataDTO> Handle(CharacterDataRequest request, CancellationToken cancellationToken) {
            CharacterDataDTO dto = new();
            dto.characterData = await _serverManagerService.GetCharacterDataAsync();
            return dto;
        }
    }
}