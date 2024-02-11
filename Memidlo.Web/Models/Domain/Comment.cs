using Microsoft.AspNetCore.SignalR;

namespace Memidlo.Web.Models.Domain
{
    public class Comment
    {
        public int Id { get; private set; }
        public string Description{ get; private set; }
        public int MemId { get; private set; }       
        public DateTime Date { get; private set; }
        public Guid UserId { get; private set; }

        private Comment()
        {

        }

        public Comment(string description, Guid userId, int memId)
        {
            Description = description;
            UserId = userId;
            MemId = memId;
            Date = DateTime.Now;
        }
    }

    
}
