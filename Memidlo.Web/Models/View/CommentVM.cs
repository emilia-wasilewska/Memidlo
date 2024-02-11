namespace Memidlo.Web.Models.View
{
    public record CommentVM(
        string Description,
        string UserName,
        DateTime PublishDate
    );
}
