using CommonData.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccessService.Application.Features.RegisterServer.Requests {

    public class RegisterServerRequest : IRequest<GameServerDataDTO> {

        [Required]
        public string IPAddress { get; init; }

        [Required]
        public int Port { get; init; }

        [Required]
        public int NumberOfLobbies { get; init; }
    }
}