using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AvatarImage { get; set; } = null!;

        public override string ToString()
        {
            return $"{Id}| name: {Name} login: {Login} pass: {Password}";
        }
    }
}
