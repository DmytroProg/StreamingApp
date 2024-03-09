using StreamingApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Interfaces.Services;

public interface IMeetingService : IService<Meeting>
{
    Task AddUserToMeetingAsync(int id, User user);
}
