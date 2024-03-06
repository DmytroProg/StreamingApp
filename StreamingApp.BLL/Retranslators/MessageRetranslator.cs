//using Server.DAL.Models;
using StreamingApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.BLL.Retranslators
{
    public class MessageRetranslator
    {
        //public MessageBase TranslateMessageBaseInfoToMessageBase(MessageBaseInfo messageBaseInfo)
        //{
        //    if (messageBaseInfo is TextMessageInfo)
        //    {
        //        return new TextMessage()
        //        {
        //            Id = messageBaseInfo.Id,
        //            SenderId = messageBaseInfo.SenderId,
        //            ReceiverId = messageBaseInfo.ReceiverId,
        //            MeetingId = messageBaseInfo.MeetingId,
        //            CreatedAt = messageBaseInfo.CreatedAt,
        //            Text = ((TextMessageInfo)messageBaseInfo).Text
        //        };
        //    }
        //    else if (messageBaseInfo is ImageMessageInfo)
        //    {
        //        return new ImageMessage()
        //        {
        //            Id = messageBaseInfo.Id,
        //            SenderId = messageBaseInfo.SenderId,
        //            ReceiverId = messageBaseInfo.ReceiverId,
        //            MeetingId = messageBaseInfo.MeetingId,
        //            CreatedAt = messageBaseInfo.CreatedAt,
        //            ImageData = ((ImageMessageInfo)messageBaseInfo).ImageData,
        //            FileName = ((ImageMessageInfo)messageBaseInfo).FileName
        //        };
        //    }
        //    else if (messageBaseInfo is FileMessageInfo)
        //    {
        //        return new FileMessage()
        //        {
        //            Id = messageBaseInfo.Id,
        //            SenderId = messageBaseInfo.SenderId,
        //            ReceiverId = messageBaseInfo.ReceiverId,
        //            MeetingId = messageBaseInfo.MeetingId,
        //            CreatedAt = messageBaseInfo.CreatedAt,
        //            OriginalFileName = ((FileMessageInfo)messageBaseInfo).OriginalFileName,
        //            UniqueFileName = ((FileMessageInfo)messageBaseInfo).UniqueFileName,
        //            Data = ((FileMessageInfo)messageBaseInfo).Data
        //        };
        //    }
        //    else throw new ArgumentException("Error !");
        //}

        //public MessageBaseInfo TranslateMessageBaseToMessageBaseInfo(MessageBase messageBase)
        //{
        //    if (messageBase is TextMessage)
        //    {
        //        return new TextMessageInfo()
        //        {
        //            Id = messageBase.Id,
        //            SenderId = messageBase.SenderId,
        //            ReceiverId = messageBase.ReceiverId,
        //            MeetingId = messageBase.MeetingId,
        //            CreatedAt = messageBase.CreatedAt,
        //            Text = ((TextMessage)messageBase).Text
        //        };
        //    }
        //    else if (messageBase is ImageMessage)
        //    {
        //        return new ImageMessageInfo()
        //        {
        //            Id = messageBase.Id,
        //            SenderId = messageBase.SenderId,
        //            ReceiverId = messageBase.ReceiverId,
        //            MeetingId = messageBase.MeetingId,
        //            CreatedAt = messageBase.CreatedAt,
        //            ImageData = ((ImageMessage)messageBase).ImageData,
        //            FileName = ((ImageMessage)messageBase).FileName
        //        };
        //    }
        //    else if (messageBase is FileMessage)
        //    {
        //        return new FileMessageInfo()
        //        {
        //            Id = messageBase.Id,
        //            SenderId = messageBase.SenderId,
        //            ReceiverId = messageBase.ReceiverId,
        //            MeetingId = messageBase.MeetingId,
        //            CreatedAt = messageBase.CreatedAt,
        //            OriginalFileName = ((FileMessage)messageBase).OriginalFileName,
        //            UniqueFileName = ((FileMessage)messageBase).UniqueFileName,
        //            Data = ((FileMessage)messageBase).Data
        //        };
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Error !");
        //    }
        //}
    }
}
