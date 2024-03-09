using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Interfaces.DataAccess;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Retranslators;

namespace StreamingApp.BLL.Services
{
    public class MessageServise : IService<TextMessage>
    {
        private ITextMessageRepository _messageRepository;
        private readonly ILogger _logger;

        public MessageServise(ITextMessageRepository repository, ILogger logger)
        {
            _messageRepository = repository;
            _logger = logger;
        }

        public async Task AddAsync(TextMessage obj)
        {
            //await _messageRepository.AddObjectAsync(obj);
            try
            {
                await _messageRepository.AddObjectAsync(obj);
                _logger.LogInfo("Text message added well.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }
        }

        public async Task<TextMessage> GetByIdAsync(int id)
        {
            //var message = await _messageRepository.GetObjectByIdAsync(id);
            //if (message != null)
            //    return message;
            //return null;
            try
            {
                var message = await _messageRepository.GetObjectByIdAsync(id);
                if (message != null)
                {
                    _logger.LogInfo($"Text message with ID {id} retrieved well.");
                    return message;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw;
            }
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
