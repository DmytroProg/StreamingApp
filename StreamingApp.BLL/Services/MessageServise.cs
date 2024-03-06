using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Retranslators;

namespace StreamingApp.BLL.Services
{
    public class MessageServise : IService<TextMessage>
    {
        private IMessageRepository _messageRepository;

        public MessageServise(IMessageRepository repository)
        {
            _messageRepository = repository;
        }

        public async Task AddAsync(TextMessage obj)
        {
            await _messageRepository.AddObjectAsync(obj);
        }

        public async Task<TextMessage> GetByIdAsync(int id)
        {
            var message = await _messageRepository.GetObjectByIdAsync(id);
            if (message != null)
                return message;
            return null;
        }

        public Task<IEnumerable<TextMessage>> QueryMany(Predicate<TextMessage> query)
        {
            throw new NotImplementedException();
        }

        public Task<TextMessage> QueryOne(Predicate<TextMessage> query)
        {
            throw new NotImplementedException();
        }
    }
}
