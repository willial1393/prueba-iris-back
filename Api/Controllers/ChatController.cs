using Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;

        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpGet("{uri}")]
        public async Task<IActionResult> ProcessFile(string uri)
        {
            var chat = await _chatRepository.ProcessFile(uri);
            return Ok(chat);
        }
    }
}