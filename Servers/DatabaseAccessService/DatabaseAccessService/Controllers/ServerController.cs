using CommonData.DTOs;
using DatabaseAccessService.Features.ErrorMessages.Requests;
using DatabaseAccessService.Features.GetPlayerData.Requests;
using DatabaseAccessService.Features.RegisterServer.Requests;
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