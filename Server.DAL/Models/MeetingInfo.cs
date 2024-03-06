using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Models
{
    public class MeetingInfo
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int UserId { get; set; }
        public List<UserInfo> Users { get; set; } = null!;
        public UserInfo Admin { get; set; } = null!;
        public List<MessageBaseInfo> Messages { get; set; } = null!;
    }
}
