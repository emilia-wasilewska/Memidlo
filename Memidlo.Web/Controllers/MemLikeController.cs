using Memidlo.Web.Models.View;
using Memidlo.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Memidlo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemLikeController : Controller
    {
        private readonly ILikeRepository likeRepository;
        public MemLikeController(ILikeRepository likeRepository)
        {
            this.likeRepository = likeRepository;
        }

        [HttpGet]
        [Route("{memId:int}/totalLikes")]
        public async Task<IActionResult> GetTotalLikes([FromRoute] int memId)
        {
            var totalLikes = await likeRepository.GetTotalLikesForMem(memId);

            return Ok(totalLikes);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] LikeVM likeVM)
        {
            await likeRepository.AddLikeForMem(likeVM.MemId, likeVM.UserId);

            return Ok();
        }
    }
}
