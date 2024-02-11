using Memidlo.Web.Mappings;
using Memidlo.Web.Models.Domain;
using Memidlo.Web.Models.View;
using Memidlo.Web.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Memidlo.Web.Pages.Mems
{
    public class MemModel : PageModel
    {
        private readonly IMemRepository memRepository;
        private readonly ILikeRepository likeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICommentRepository commentRepository;
        private readonly IUserRepository userRepository;

        [BindProperty]
        public Mem Mem { get; set; }

        [BindProperty]
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }

        [BindProperty]
        public int MemId { get; set; }

        [BindProperty]
        public List<CommentVM> CommentVMs { get; set; }

        [BindProperty]
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string CommentContent { get; set; }

        public MemModel(IMemRepository memRepository, ILikeRepository likeRepository,
            SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ICommentRepository commentRepository, IUserRepository userRepository)
        {
            this.memRepository = memRepository;
            this.likeRepository = likeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.commentRepository = commentRepository;
            this.userRepository = userRepository;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            Mem = await memRepository.GetAsync(id);

            if (Mem != null)
            {
                MemId = Mem.Id;

                if (signInManager.IsSignedIn(User))
                {
                    var likes = await likeRepository.GetLikesForMem(id);

                    var userID = userManager.GetUserId(User);

                    Liked = likes.Any(x => x.UserId == Guid.Parse(userID));

                }
                await GetComments();

                TotalLikes = await likeRepository.GetTotalLikesForMem(id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(string id)
        {
            if (signInManager.IsSignedIn(User) && !string.IsNullOrWhiteSpace(CommentContent))
            {
                var userId = userManager.GetUserId(User);
                var comment = new Comment(
                    CommentContent,
                    Guid.Parse(userId),
                    MemId); 

                await commentRepository.AddAsync(comment);
            }

            return RedirectToPage("/Mems/Mem", new { id = id });
        }

        private async Task GetComments()
        {
            var memComments = (await commentRepository.GetAllForMemAsync(Mem.Id)).OrderByDescending(x => x.Date).ToList();

            var userNames = await GetUserNames(memComments);

            CommentVMs = memComments.Select(x => x.ToViewModel(userNames)).ToList();
        }

        private async Task<Dictionary<string, string?>> GetUserNames(List<Comment> memComments)
        {
            var userIds = memComments.Select(x => x.UserId.ToString()).Distinct();
            var users = await userRepository.GetByIds(userIds);

            return users.ToDictionary(x => x.Id, x => x.UserName);
        }
    }
}
