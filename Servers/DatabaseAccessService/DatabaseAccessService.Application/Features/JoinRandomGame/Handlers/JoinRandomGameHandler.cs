using CommonData.ServerData;
using DatabaseAccessService.Application.DTOs;
using DatabaseAccessService.Application.Features.JoinRandomGame.Requests;
using DatabaseAccessService.Domain.Services.Interfaces;
using MediatR;

namespace DatabaseAccessService.Application.Features.JoinRandomGame.Handlers {

    public class JoinRandomGameHandler : IRequestHandler<JoinRandomGameRequest, JoinRandomGameDTO> {
        private readonly IServerManagerService _serverManagerService;
        private readonly IPlayerAccountsService _playerAccountsService;

        public JoinRandomGameHandler(IServerManagerService serverManagerService, IPlayerAccountsService playerAccountsService) {
            _serverManagerService = serverManagerService;
            _playerAccountsService = playerAccountsService;
        }

        public async Task<JoinRandomGameDTO> Handle(JoinRandomGameRequest request, CancellationToken cancellationToken) {
            JoinRandomGameDTO dto = new();

            DBPlayer player = await _playerAccountsService.GetPlayerAsync(request.SteamID);
            Lobby? lobby = await _serverManagerService.JoinRandomGameAsync(player);

            if (lobby == null) throw new InvalidDataException("Lobby not found");

            dto.LobbyID = lobby.LobbyID;
            dto.ServerIp = lobby.ServerIp;
            dto.ServerPort = lobby.ServerPort;
            dto.MapName = lobby.GameMap.MapName;
            dto.Abbreviation = lobby.GameMode.Abbreviation;
            dto.GameMode = lobby.GameMode.GameModeName;

            return dto;
        }
    }
}