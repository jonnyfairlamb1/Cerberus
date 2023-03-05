using CommonData.DTOs;
using DatabaseAccessService.Application.Features.PlayerLogin.Requests;
using DatabaseAccessService.Application.Features.PlayerLogout.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseAccessService.Controllers {

    [ApiController, Route("api/[controller]")]
    public class AccountsController : ControllerBase {

        #region Fields

        private readonly ILogger<AccountsController> _logger;
        private readonly IMediator _mediator;

        #endregion Fields

        public AccountsController(ILogger<AccountsController> logger, IMediator mediator) {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("[action]")]
        public async Task<ActionResult<PlayerLoginDTO>> PlayerLoginAsync([FromQuery] PlayerLoginRequest query) {
            try {
                PlayerLoginDTO dto = await _mediator.Send(query);
                return dto;
            } catch (Exception exception) {
                const string message = "Player Login failed for unknown reason";
                _logger.LogCritical(exception, message);
                return BadRequest(message);
            }
        }

        [HttpGet, Route("[action]")]
        public async Task<ActionResult<PlayerLogoutDTO>> PlayerLogoutAsync([FromQuery] PlayerLogoutRequest query) {
            try {
                PlayerLogoutDTO dto = await _mediator.Send(query);
                return dto;
            } catch (Exception exception) {
                const string message = "Player Logout messages failed for unknown reason";
                _logger.LogCritical(exception, message);
                return BadRequest(message);
            }
        }
    }
}