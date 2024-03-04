using Server.DAL.Repositories;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Retranslators;

namespace StreamingApp.BLL.Services
{
    public class MessageServise //: IService<MessageBase>
    {
        private MessageRepository _messageRepository;
        private MessageRetranslator _messageRetranslator;

        //public MessageServise()
        //{
        //    _messageRepository = new MessageRepository();
        //    _messageRetranslator = new MessageRetranslator();
        //}

        //public async Task AddAsync(MessageBase obj)
        //{
        //    await _messageRepository.AddObjectAsync(_messageRetranslator.TranslateMessageBaseToMessageBaseInfo(obj));
        //}

        //public async Task<MessageBase> GetByIdAsync(int id)
        //{
        //    var message = await _messageRepository.GetObjectByIdAsync(id);
        //    if (message != null)
        //        return _messageRetranslator.TranslateMessageBaseInfoToMessageBase(message);
        //    return null;
        //}

        //public Task<IEnumerable<MessageBase>> QueryMany(Predicate<MessageBase> query)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<MessageBase> QueryOne(Predicate<MessageBase> query)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
