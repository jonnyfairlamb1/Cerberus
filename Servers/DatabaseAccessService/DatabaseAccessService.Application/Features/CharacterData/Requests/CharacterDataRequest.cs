using CommonData.DTOs;
using MediatR;

namespace DatabaseAccessService.Application.Features.CharacterData.Requests {

    public class CharacterDataRequest : IRequest<CharacterDataDTO> {
    }
}