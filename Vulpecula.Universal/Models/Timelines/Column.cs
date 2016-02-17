using System;

using JetBrains.Annotations;

using Prism.Mvvm;

// ReSharper disable MemberCanBePrivate.Global

namespace Vulpecula.Universal.Models.Timelines
{
    public class Column : BindableBase
    {
        public TimelineType Type { get; set; }

        public string ColumnId { get; set; }

        public long UserId { get; set; }

        public string Query { get; set; }

        // Called by Json.NET
        [UsedImplicitly]
        public Column() {}

        private Column(TimelineType type, string id, string name, long userId, int row, string query, bool enableNotity)
        {
            Type = type;
            ColumnId = id;
            _name = name;
            UserId = userId;
            _row = row;
            Query = query;
            _enableNotify = enableNotity;
        }

        public static Column CreateColumnInfo(TimelineType type, string name, long userId, int row, string query = null, bool enableNotity = true)
        {
            return new Column(type, Guid.NewGuid().ToString(), name, userId, row, query, enableNotity);
        }

        private void Resave()
        {
            ColumnManager.Instance.RewriteColumn(this);
        }

        /// <summary>
        /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <returns>
        /// 指定したオブジェクトが現在のオブジェクトと等しい場合は true。それ以外の場合は false。
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
        /// 既定のハッシュ関数として機能します。
        /// </summary>
        /// <returns>
        /// 現在のオブジェクトのハッシュ コード。
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
            set
            {
                if (SetProperty(ref _name, value))
                    Resave();
            }
        }

        #endregion

        #region Row

        private int _row;

        public int Row
        {
            get { return _row; }
            set
            {
                if (SetProperty(ref _row, value))
                    Resave();
            }
        }

        #endregion

        #region EnableNotity

        private bool _enableNotify;

        public bool EnableNotity
        {
            get { return _enableNotify; }
            set
            {
                if (SetProperty(ref _enableNotify, value))
                    Resave();
            }
        }

        #endregion
    }
}