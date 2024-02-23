using Server.DAL.Repositories;
using StreamingApp.BLL.Interfaces;
using StreamingApp.BLL.Models;
using StreamingApp.BLL.Retranslators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Services
{
    public class MessageServise : IServise<MessageBase>
    {
        private MessageRepository _messageRepository;
        private MessageRetranslator _messageRetranslator;

        public MessageServise()
        {
            _messageRepository = new MessageRepository();
            _messageRetranslator = new MessageRetranslator();
        }

        public async Task AddAsync(MessageBase obj)
        {
            await _messageRepository.AddObjectAsync(_messageRetranslator.TranslateMessageBaseToMessageBaseInfo(obj));
        }

        public async Task<MessageBase> GetByIdAsync(int id)
        {
            var message = await _messageRepository.GetObjectByIdAsync(id);
            if (message != null)
                return _messageRetranslator.TranslateMessageBaseInfoToMessageBase(message);
            return null;
        }
    }
}
