using Memidlo.Web.Models.Domain;
using Memidlo.Web.Models.View;

namespace Memidlo.Web.Mappings
{
    public static class ViewModelsMappings
    {
        public static CommentVM ToViewModel(this Comment from, IDictionary<string, string?> userNames)
        {
            return new CommentVM(
                from.Description,
                userNames.TryGetValue(from.UserId.ToString(), out var value) ? value : string.Empty,
                from.Date);
        }
    }
}
