using CommonData.DTOs;
using DatabaseAccessService.Domain.Services.Interfaces;
using DatabaseAccessService.Features.PlayerLogout.Requests;
using MediatR;

namespace DatabaseAccessService.Features.PlayerLogout.Handlers {

    public class PlayerLogoutHandler : IRequestHandler<PlayerLogoutRequest, PlayerLogoutDTO> {
        private readonly IPlayerAccountsService _playerAccountsService;

        public PlayerLogoutHandler(IPlayerAccountsService playerAccountsService) {
            _playerAccountsService = playerAccountsService;
        }

        public async Task<PlayerLogoutDTO> Handle(PlayerLogoutRequest request, CancellationToken cancellationToken) {
            PlayerLogoutDTO dto = new();
            dto.PlayerLoggedIn = await _playerAccountsService.PlayerLogoutAsync(request.SteamId);
            return dto;
        }
    }
}