using Memidlo.Web.Models.Domain;
using Memidlo.Web.Models.View;
using Memidlo.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Memidlo.Web.Pages
{
    public class TopTenModel : PageModel
    {
        private readonly ILikeRepository likeRepository;
        private readonly IMemRepository memRepository;
        private List<int> memsIds;
        private List<Mem> mems;

        [BindProperty]
        public List<Mem> Mems { get; set; }


        public TopTenModel(ILikeRepository likeRepository, IMemRepository memRepository)
        {
            this.likeRepository = likeRepository;
            this.memRepository = memRepository;
            memsIds = new List<int>();
            Mems = new List<Mem>();
        }
        public async Task OnGet()
        {
            var likes = await likeRepository.GetAllLikes();
            mems = (await memRepository.GetAllAsync()).ToList();

            if (likes != null && likes.Any())
            {
                var orderedLikes = likes.GroupBy(x => x.MemId).Select(x => new
                {
                    MemId = x.Key,
                    NumberOfMems = x.Count()
                })
                .OrderByDescending(x => x.NumberOfMems)
                .ToList();

                int loopRepeats = orderedLikes.Count() < 10 ? loopRepeats = orderedLikes.Count() : loopRepeats = 10;

                for (int i = 0; i < loopRepeats; i++)
                {
                    memsIds.Add(orderedLikes[i].MemId);
                }

                if (memsIds != null)
                {
                    for (int i = 0; i < memsIds.Count(); i++)
                    {
                        var mem = mems.FirstOrDefault(x => x.Id == memsIds[i]);
                        Mems.Add(mem);
                    }
                }
            }
        }
    }
}
