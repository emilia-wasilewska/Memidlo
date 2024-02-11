using Memidlo.Web.Models.Domain;
using Memidlo.Web.Models.View;
using Memidlo.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Memidlo.Web.Pages
{
    public class WaitingRoomModel : PageModel
    {
        private readonly IMemRepository memRepository;
        private readonly ICommentRepository commentRepository;

        public IEnumerable<Mem> Mems { get; set; }

        [BindProperty]
        public int SelectedMemId { get; set; }

        [BindProperty(SupportsGet = true)]
        public PaginatedPage PaginatedPage { get; set; }

        public WaitingRoomModel(IMemRepository memRepository)
        {
            this.memRepository = memRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            PaginatedPage.Count = await memRepository.CountWaitingRoomAsync();

            Mems = await memRepository.GetPaginated(
               pageSize: PaginatedPage.PageSize,
               pageNumber: PaginatedPage.CurrentPage,
               isMain: false
               );

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            await memRepository.MoveToMainAsync(SelectedMemId);

            return RedirectToPage("/WaitingRoom");
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var deletedMem = await memRepository.DeleteAsync(id);

            if (deletedMem)
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
