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

        public long Id => this._originalModel.Id;

        public DateTime CreatedAt => this._originalModel.CreatedAt;

        public string Text => this._originalModel.Text;

        public Entities Entities => this._originalModel.Entities;

        public bool IsFavorited => this._status.IsFavorited;

        public long FavoritedCount => this._status.FavoritedCount;

        public bool IsSpread => this._status.IsSpread;

        public long SpreadCount => this._status.SpreadCount;

        public bool HasInReply => this._status.InReplyToStatusId.HasValue;

        public long? InReplyToStatusId => this._status.InReplyToStatusId;

        public long? InReplyToUserId => this._status.InReplyToUserId;

        public Source Source => this._status.Source;

        public User User => this.IsDirectMessage ? this._secretMail.Sender : this._status.User;

        public User Recipient => this.IsDirectMessage ? this._secretMail.Recipient : new User();

        public StatusModel SpreadStatus => new StatusModel(this._status.SpreadStatus);

        public StatusModel(StatusBase @base)
        {
            this._originalModel = @base;
            this.IsDirectMessage = (@base is SecretMail);
            if (this.IsDirectMessage)
                this._secretMail = (SecretMail)@base;
            else
                this._status = (Status)@base;
        }
    }
}