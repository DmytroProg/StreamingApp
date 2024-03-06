using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Models
{
    public abstract class MessageBaseInfo
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int MeetingId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
