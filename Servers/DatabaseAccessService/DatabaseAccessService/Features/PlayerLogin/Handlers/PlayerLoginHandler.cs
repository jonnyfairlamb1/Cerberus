using CommonData.DTOs;
using CommonData.ServerData;
using DatabaseAccessService.Domain.Services.Interfaces;
using DatabaseAccessService.Features.PlayerLogin.Requests;
using MediatR;

namespace DatabaseAccessService.Features.PlayerLogin.Handlers {

    public class PlayerLoginHandler : IRequestHandler<PlayerLoginRequest, PlayerLoginDTO> {
        private readonly IPlayerAccountsService _playerAccountsService;

        public PlayerLoginHandler(IPlayerAccountsService playerAccountsService) {
            _playerAccountsService = playerAccountsService;
        }

        public async Task<PlayerLoginDTO> Handle(PlayerLoginRequest request, CancellationToken cancellationToken) {
            PlayerLoginDTO dto = new();
            DBPlayer? player = await _playerAccountsService.PlayerLoginAsync(request.SteamName, request.SteamId, request.IpAddress);
            if (player == null) throw new InvalidDataException("Unable to get player");
            dto.Player = player;
            return dto;
        }
    }
}