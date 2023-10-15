using CommonData.DTOs;
using MediatR;

namespace DatabaseAccessService.Features.CharacterData.Requests {

    public class CharacterDataRequest : IRequest<CharacterDataDTO> {
    }
}