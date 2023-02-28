using DatabaseAccessService.Application.DTOs;
using DatabaseAccessService.Application.Features.BaseWeapons.Requests;
using DatabaseAccessService.Domain.Services.Interfaces;
using MediatR;

namespace DatabaseAccessService.Application.Features.BaseWeapons.Handlers {

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