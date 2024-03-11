using StreamingApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.WPF;

public static class UserInfo
{
    public static User CurrentUser { get; set; }
    public static int MeetingId { get; set; }
}
