using Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly IS3Repository _s3Repository;

        public ChatController(IChatRepository chatRepository, IS3Repository s3Repository)
        {
            _chatRepository = chatRepository;
            _s3Repository = s3Repository;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessFile(string uriFile)
        {
            if (string.IsNullOrEmpty(uriFile))
            {
                return BadRequest("La uri debe ser valida, ej: s3://fdr-developer-test-22122020/data.txt");
            }

            try
            {
                var file = await _s3Repository.GetFileFromUri(uriFile);
                var chat = await _chatRepository.ProcessFile(file);
                await _s3Repository.DeleteFileFromUri(uriFile);
                return Ok(chat);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}