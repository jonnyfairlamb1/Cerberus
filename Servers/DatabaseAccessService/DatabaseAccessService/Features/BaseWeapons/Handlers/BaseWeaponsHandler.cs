using CommonData.DTOs;
using DatabaseAccessService.Features.BaseWeapons.Requests;
using MediatR;
using DatabaseAccessService.Domain.Services.Interfaces;

namespace DatabaseAccessService.Features.BaseWeapons.Handlers {

    public class BaseWeaponsHandler : IRequestHandler<BaseWeaponsRequest, BaseWeaponsDTO> {
        private readonly IServerManagerService _serverManagerService;

        public BaseWeaponsHandler(IServerManagerService serverManagerService) {
            _serverManagerService = serverManagerService;
        }

        public async Task<BaseWeaponsDTO> Handle(BaseWeaponsRequest request, CancellationToken cancellationToken) {
            BaseWeaponsDTO dto = new();
            dto.BaseWeapons = await _serverManagerService.GetBaseWeaponsAsync();
            return dto;
        }
    }
}