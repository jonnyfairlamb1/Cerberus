using CommonData.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccessService.Application.Features.PlayerLogout.Requests {
    public record PlayerLogoutRequest : IRequest<PlayerLogoutDTO> {
        [Required]
        public string SteamId { get; init; }
    }
}