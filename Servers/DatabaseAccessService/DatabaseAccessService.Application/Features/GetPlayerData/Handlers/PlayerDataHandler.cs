using DatabaseAccessService.Application.DTOs;
using DatabaseAccessService.Application.Features.PlayerData.Requests;
using DatabaseAccessService.Domain.Services.Interfaces;
using MediatR;

namespace DatabaseAccessService.Application.Features.PlayerData.Handlers {

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