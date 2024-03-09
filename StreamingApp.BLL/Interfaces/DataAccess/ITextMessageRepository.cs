using StreamingApp.BLL.Models;

namespace StreamingApp.BLL.Interfaces.DataAccess;

public interface ITextMessageRepository : IRepository<TextMessage>
{
    Task<IEnumerable<TextMessage>> GetTextMessagesByUserAsync(User user);
}
