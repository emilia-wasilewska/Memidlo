using Memidlo.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Memidlo.Web.Models.View;
using System.ComponentModel.DataAnnotations;
using Memidlo.Web.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Memidlo.Web.Pages.Mems
{
    public class AddMemModel : PageModel
    {
        private readonly IMemRepository memRepository;
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public MemVM MemVM { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        [Required]
        public string Tags { get; set; }

        public AddMemModel(IMemRepository memRepository, SignInManager<IdentityUser> signInManager)
        {
            this.memRepository = memRepository;
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var mem = new Mem
            {
                PageTitle = MemVM.PageTitle,
                Heading = MemVM.Heading!=null?MemVM.Heading.Substring(0, 1).ToUpper() + MemVM.Heading.Substring(1):null,
                Content = MemVM.Content!=null?MemVM.Content.Substring(0, 1).ToUpper() + MemVM.Content.Substring(1):null,
                FeaturedImageUrl = MemVM.FeaturedImageUrl,
                Author = User?.Identity?.Name,
                Main = false,
                PublishDate = DateTime.Now,
                Tags = new List<Tag>(Tags.Split('#').Select(x => new Tag() { Name = x.Trim() }))
            };
            
            await memRepository.AddAsync(mem);

            return Page();

        }
    }
}
