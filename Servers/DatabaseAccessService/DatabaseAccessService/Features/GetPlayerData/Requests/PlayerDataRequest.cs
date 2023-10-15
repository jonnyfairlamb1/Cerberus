using CommonData.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccessService.Features.GetPlayerData.Requests {

    public class PlayerDataRequest : IRequest<PlayerDataDTO> {

        [Required]
        public string SteamId { get; init; }
    }
}