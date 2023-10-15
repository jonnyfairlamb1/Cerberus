using CommonData.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccessService.Features.PlayerLogin.Requests {
    public record PlayerLoginRequest : IRequest<PlayerLoginDTO> {
        [Required]
        public string SteamName { get; init; }
        [Required]
        public string SteamId { get; init; }
        [Required]
        public string IpAddress { get; init; }
    }
}