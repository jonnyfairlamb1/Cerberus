using DatabaseAccessService.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccessService.Application.Features.PlayerData.Requests {

    public class PlayerDataRequest : IRequest<PlayerDataDTO> {

        [Required]
        public string SteamId { get; init; }
    }
}