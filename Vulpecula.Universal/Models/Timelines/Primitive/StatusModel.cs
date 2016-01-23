using System;

using Vulpecula.Models;
using Vulpecula.Models.Base;

namespace Vulpecula.Universal.Models.Timelines.Primitive
{
    /// <summary>
    /// <para>Wrapper for Vulpecula.Models.StatusBase</para>
    /// </summary>
    public class StatusModel
    {
        private readonly StatusBase _originalModel;
        private readonly SecretMail _secretMail;
        private readonly Status _status;
        public bool IsDirectMessage { get; }

        public long Id => _originalModel.Id;

        public DateTime CreatedAt => _originalModel.CreatedAt;

        public string Text => _originalModel.Text;

        public Entities Entities => _originalModel.Entities;

        public bool IsFavorited => _status.IsFavorited;

        public long FavoritedCount => _status.FavoritedCount;

        public bool IsSpread => _status.IsSpread;

        public long SpreadCount => _status.SpreadCount;

        public bool HasInReply => _status.InReplyToStatusId.HasValue;

        public long? InReplyToStatusId => _status.InReplyToStatusId;

        public long? InReplyToUserId => _status.InReplyToUserId;

        public string InReplyToScreenName => _status.InReplyToScreenName;

        public Source Source => _status?.Source;

        public User User => IsDirectMessage ? _secretMail.Sender : _status.User;

        public User Recipient => IsDirectMessage ? _secretMail.Recipient : new User();

        public StatusModel SpreadStatus => _status.SpreadStatus != null ? new StatusModel(_status.SpreadStatus) : null;

        public StatusModel QuotedStatus => _status.QuotedStatus != null ? new StatusModel(_status.QuotedStatus) : null;

        public StatusModel(StatusBase @base)
        {
            _originalModel = @base;
            IsDirectMessage = @base is SecretMail;
            if (IsDirectMessage)
                _secretMail = (SecretMail) @base;
            else
                _status = (Status) @base;
        }
    }
}