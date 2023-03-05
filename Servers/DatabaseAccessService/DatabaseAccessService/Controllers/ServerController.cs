using CommonData.DTOs;
using DatabaseAccessService.Application.Features.BaseWeapons.Requests;
using DatabaseAccessService.Application.Features.CharacterData.Requests;
using DatabaseAccessService.Application.Features.ErrorMessages.Requests;
using DatabaseAccessService.Application.Features.JoinRandomGame.Requests;
using DatabaseAccessService.Application.Features.PlayerData.Requests;
using DatabaseAccessService.Application.Features.RegisterServer.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseAccessService.Controllers {

    [ApiController, Route("api/[controller]")]
    public class ServerController : ControllerBase {

        #region Fields

        private readonly ILogger<ServerController> _logger;

        private readonly IMediator _mediator;

        #endregion Fields

        public ServerController(IMediator mediator, ILogger<ServerController> logger) {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get all the error messages from the database, this should only be called from a server
        /// </summary>
        /// <returns>Dictionary of error messages</returns>
        /// <response code="200">Returns the dictionary of error messages correctly</response>
        /// <response code="400">Returns an error message if the dictionary failed to be retrieved</response>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ErrorMessagesDTO>> GetErrorMessages([FromQuery] ErrorMessagesRequest query) {
            try {
                ErrorMessagesDTO dto = await _mediator.Send(query);
                return dto;
            } catch (Exception exception) {
                const string message = "Getting error messages failed for unknown reason";
                _logger.LogCritical(exception, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Get all the base weapons from the database, this should only be called from a server
        /// </summary>
        /// <returns>list of base weapon objeect</returns>
        /// <response code="200">Returns the list of base weapon objects correctly</response>
        /// <response code="400">Returns an error message if the list failed to be retrieved</response>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseWeaponsDTO>> BaseWeapons([FromQuery] BaseWeaponsRequest query) {
            try {
                BaseWeaponsDTO dto = await _mediator.Send(query);
                return dto;
            } catch (Exception exception) {
                const string message = "Getting base weapons failed for unknown reason";
                _logger.LogCritical(exception, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Get character data from the database, this should only be called from a server
        /// </summary>
        /// <returns>Dictionary of character objects</returns>
        /// <response code="200">Returns the dictionary of character objects correctly</response>
        /// <response code="400">Returns an error message if the dictionary failed to be retrieved</response>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CharacterDataDTO>> GetCharacterData([FromQuery] CharacterDataRequest query) {
            try {
                CharacterDataDTO dto = await _mediator.Send(query);
                return dto;
            } catch (Exception exception) {
                const string message = "Getting character data failed for unknown reason";
                _logger.LogCritical(exception, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Register the game server with the service, this should only be called from a game server.
        /// </summary>
        /// <returns>Game server data object, this includes maps and gamemodes for each lobby</returns>
        /// <response code="200">Returns the game server data object correctly</response>
        /// <response code="400">Returns an error message if the registration failed</response>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GameServerDataDTO>> RegisterServerAsync([FromQuery] RegisterServerRequest query) {
            try {
                GameServerDataDTO dto = await _mediator.Send(query);
                return dto;
            } catch (Exception exception) {
                const string message = "registering game server failed for unknown reason";
                _logger.LogCritical(exception, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Finds a lobby with space for the player requesting. This should only be called from the
        /// login server as a proxy for the client.
        /// </summary>
        /// <returns>DB player object that represents all of the account data for that player.</returns>
        /// <response code="200">Returns the DB player object correctly</response>
        /// <response code="400">Returns an error message if the player was failed to be retrieved</response>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JoinRandomGameDTO>> JoinRandomGameAsync([FromQuery] JoinRandomGameRequest query) {
            try {
                JoinRandomGameDTO dto = await _mediator.Send(query);
                return dto;
            } catch (Exception exception) {
                const string message = "joining game failed for unknown reason";
                _logger.LogCritical(exception, message);
                return BadRequest(message);
            }
        }

        /// <summary>
        /// Gets the player data for a specified player. This should only be called from a game server.
        /// </summary>
        /// <returns>DB Player object, this represents all account data of the player.</returns>
        /// <response code="200">Returns the db player object correctly</response>
        /// <response code="400">Returns an error message if the dictionary failed to be retrieved</response>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlayerDataDTO>> GetPlayerDataAsync([FromQuery] PlayerDataRequest query) {
            try {
                PlayerDataDTO dto = await _mediator.Send(query);
                return dto;
            } catch (Exception exception) {
                const string message = "get player data failed for unknown reason";
                _logger.LogCritical(exception, message);
                return BadRequest(message);
            }
        }
    }
}