using Microsoft.AspNetCore.Mvc;
using BursOtomasyon.Web.Services;

namespace BursOtomasyon.Web.Controllers
{
    [ApiController]
    [Route("api/assistant")]
    public class AIAssistantController : ControllerBase
    {
        private readonly AIAnalizService _aiAnalizService;

        public AIAssistantController(AIAnalizService aiAnalizService)
        {
            _aiAnalizService = aiAnalizService;
        }

        public class AskRequest
        {
            public string? Message { get; set; }
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] AskRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new { error = "Mesaj boş olamaz." });
            }

            var response = await _aiAnalizService.AskAssistantAsync(request.Message.Trim());
            return Ok(new { success = true, answer = response });
        }
    }
}

