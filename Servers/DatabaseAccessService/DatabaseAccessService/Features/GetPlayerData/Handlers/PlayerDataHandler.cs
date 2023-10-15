using CommonData.DTOs;
using DatabaseAccessService.Domain.Services.Interfaces;
using DatabaseAccessService.Features.GetPlayerData.Requests;
using MediatR;

namespace DatabaseAccessService.Features.GetPlayerData.Handlers {

    public class PlayerDataHandler : IRequestHandler<PlayerDataRequest, PlayerDataDTO> {
        private readonly IPlayerAccountsService _playerAccountsService;

        public PlayerDataHandler(IPlayerAccountsService playerAccountsService) {
            _playerAccountsService = playerAccountsService;
        }

        public async Task<PlayerDataDTO> Handle(PlayerDataRequest request, CancellationToken cancellationToken) {
            PlayerDataDTO dto = new();
            dto.Player = await _playerAccountsService.GetPlayerAsync(request.SteamId);
            return dto;
        }
    }
}