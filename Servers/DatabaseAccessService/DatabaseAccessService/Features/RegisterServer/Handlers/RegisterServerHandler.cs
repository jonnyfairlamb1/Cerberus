using CommonData.DTOs;
using CommonData.ServerData;
using DatabaseAccessService.Domain.Services.Interfaces;
using DatabaseAccessService.Features.RegisterServer.Requests;
using MediatR;

namespace DatabaseAccessService.Features.RegisterServer.Handlers {

    public class RegisterServerHandler : IRequestHandler<RegisterServerRequest, GameServerDataDTO> {
        private readonly IServerManagerService _serverManagerService;

        public RegisterServerHandler(IServerManagerService serverManagerService) {
            _serverManagerService = serverManagerService;
        }

        public async Task<GameServerDataDTO> Handle(RegisterServerRequest request, CancellationToken cancellationToken) {
            GameServerDataDTO dto = new();

            GameServerData data = await _serverManagerService.RegisterGameServerAsync(request.IPAddress, request.Port, request.NumberOfLobbies);

            dto.IpAddress = data.IpAddress;
            dto.Port = data.Port;
            dto.GameServerId = data.GameServerId;
            dto.Lobbies = data.Lobbies;
            return dto;
        }
    }
}