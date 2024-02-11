using Memidlo.Web.Models.Domain;
using Memidlo.Web.Models.View;
using Memidlo.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Memidlo.Web.Pages.Mems
{
    public class RandomMemModel : PageModel
    {
        private readonly IMemRepository memRepository;

        [BindProperty]
        public Mem? Mem { get; set; }
        public RandomMemModel(IMemRepository memRepository)
        {
            this.memRepository = memRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            Mem = await memRepository.GetRandom();

            return Page();
        }
    }
}
