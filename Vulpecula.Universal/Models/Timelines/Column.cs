using System;

using JetBrains.Annotations;

using Newtonsoft.Json;

using Prism.Mvvm;

// ReSharper disable MemberCanBePrivate.Global

namespace Vulpecula.Universal.Models.Timelines
{
    public class Column : BindableBase
    {
        // Called by Json.NET
        [UsedImplicitly]
        public Column()
        {
        }

        public TimelineType Type { get; set; }

        public string ColumnId { get; set; }

        [JsonIgnore]
        public CroudiaAccount Account { get; set; }

        public long UserId { get; set; }

        public string Query { get; set; }

        public static Column Create(TimelineType timelineType, string name, int row, CroudiaAccount account)
        {
            return new Column
            {
                Type = timelineType,
                Name = name,
                Row = row,
                ColumnId = Guid.NewGuid()
                               .ToString(),
                EnableNotity = false,
                Query = string.Empty,
                Account = account,
                UserId = account.User.Id
            };
        }

        /// <summary>
        ///     指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <returns>
        ///     指定したオブジェクトが現在のオブジェクトと等しい場合は true。それ以外の場合は false。
        /// </returns>
        /// <param name="obj">現在のオブジェクトと比較するオブジェクト。 </param>
        public override bool Equals(object obj)
        {
            var info = obj as Column;
            if (info == null)
                return false;
            return info.ColumnId == ColumnId;
        }

        /// <summary>
        ///     既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns>
        ///     現在のオブジェクトのハッシュ コード。
        /// </returns>
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return ColumnId.GetHashCode();
        }

        #region Name

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        #endregion Name

        #region Row

        private int _row;

        public int Row
        {
            get { return _row; }
            set { SetProperty(ref _row, value); }
        }

        #endregion Row

        #region EnableNotity

        private bool _enableNotify;

        public bool EnableNotity
        {
            get { return _enableNotify; }
            set { SetProperty(ref _enableNotify, value); }
        }

        #endregion EnableNotity
    }
}