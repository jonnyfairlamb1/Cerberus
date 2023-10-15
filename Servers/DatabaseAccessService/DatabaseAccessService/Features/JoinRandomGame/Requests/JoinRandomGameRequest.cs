using CommonData.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccessService.Features.JoinRandomGame.Requests {

    public class JoinRandomGameRequest : IRequest<JoinRandomGameDTO> {

        [Required]
        public string SteamID { get; init; }
    }
}