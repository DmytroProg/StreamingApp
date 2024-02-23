using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Models
{
    public class ImageMessageInfo : MessageBaseInfo
    {
        public byte[] ImageData { get; set; } = null!;
        public string FileName { get; set; } = null!;
    }
}
