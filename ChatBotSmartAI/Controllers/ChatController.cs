using Microsoft.AspNetCore.Mvc;
using ChatBotSmartAI.Engines;

namespace ChatBotSmartAI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly ChatOrchestrator _orchestrator;

    public ChatController(ChatOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] string question)
    {
        var response = await _orchestrator.GenerateResponseAsync(question);
        return Ok(new { answer = response });
    }
}