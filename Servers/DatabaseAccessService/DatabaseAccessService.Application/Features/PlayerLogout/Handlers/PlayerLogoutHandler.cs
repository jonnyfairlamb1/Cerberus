using DatabaseAccessService.Application.DTOs;
using DatabaseAccessService.Application.Features.PlayerLogout.Requests;
using DatabaseAccessService.Domain.Services.Interfaces;
using MediatR;

namespace DatabaseAccessService.Application.Features.PlayerLogout.Handlers {

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