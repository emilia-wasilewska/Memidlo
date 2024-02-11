namespace Memidlo.Web.Models.Domain
{
    public class Mem
    {
        public int Id { get; set; }
        public string PageTitle { get; set; }
        public string? Heading { get; set; }
        public string? Content { get; set; }
        public string FeaturedImageUrl { get; set; }       
        public string Author { get; set; }
        public bool Main { get; set; }
        public DateTime PublishDate { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
