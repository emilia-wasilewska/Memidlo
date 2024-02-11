using Memidlo.Web.Models.Domain;
using Memidlo.Web.Models.View;
using Memidlo.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Memidlo.Web.Pages.Profile
{
    public class EditMyMemModel : PageModel
    {
        private readonly IMemRepository memRepository;

        [BindProperty]
        public MemVM MemVM { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        [Required]
        public string Tags { get; set; }

        public EditMyMemModel(IMemRepository memRepository)
        {
            this.memRepository = memRepository;
        }
        public async Task OnGet(int id)
        {
            var mem = await memRepository.GetAsync(id);

            if (mem != null && mem.Tags != null)
            {
                MemVM = new MemVM
                {
                    Id = mem.Id,
                    PageTitle = mem.PageTitle,
                    Heading = mem.Heading,
                    Content = mem.Content,
                    FeaturedImageUrl = mem.FeaturedImageUrl,
                    Author = mem.Author,
                    Main = mem.Main,
                    PublishDate = mem.PublishDate,
                };

                Tags = string.Join('#', mem.Tags.Select(x => x.Name));
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                var memWithEditedProperties = new Mem
                {
                    Id = MemVM.Id,
                    PageTitle = MemVM.PageTitle,
                    Heading = MemVM.Heading != null ? MemVM.Heading.Substring(0, 1).ToUpper() + MemVM.Heading.Substring(1) : null,
                    Content = MemVM.Content != null ? MemVM.Content.Substring(0, 1).ToUpper() + MemVM.Content.Substring(1) : null,
                    FeaturedImageUrl = MemVM.FeaturedImageUrl,
                    Author = User?.Identity?.Name,
                    Main = false,
                    PublishDate = DateTime.Now,
                    Tags = new List<Tag>(Tags.Split('#').Select(x => new Tag() { Name = x.Trim() }))
                };

                await memRepository.UpdateAsync(memWithEditedProperties);

                ViewData["Notification"] = new NotificationVM
                {
                    Message = "Mem zaktualizowano pomyślnie",
                    Type = Enums.NotificationType.Success
                };
            }
            catch (Exception ex)
            {
                ViewData["Notification"] = new NotificationVM
                {
                    Message = "Upss, coś poszlo nie tak",
                    Type = Enums.NotificationType.Error
                };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deletedMem = await memRepository.DeleteAsync(MemVM.Id);

            if(deletedMem)
            {
                ViewData["Notification"] = new NotificationVM
                {
                    Message = "Mem zostal usunięty",
                    Type = Enums.NotificationType.Success
                };
            }
            else
            {
                ViewData["Notification"] = new NotificationVM
                {
                    Message = "Upss, coś poszlo nie tak",
                    Type = Enums.NotificationType.Error
                };
            }

            return Page();
        }

    }
}
