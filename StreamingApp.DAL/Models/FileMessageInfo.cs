using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Models
{
    public class FileMessageInfo : MessageBaseInfo
    {
        public string OriginalFileName { get; set; } = null!;
        public string UniqueFileName { get; set; } = null!;
        public byte[] Data { get; set; } = null!;
    }
}
