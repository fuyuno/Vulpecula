using System;
using System.IO;

using Windows.Storage;

using Prism.Mvvm;

namespace Vulpecula.Universal.Models.Timelines
{
    public class Column : BindableBase
    {
        public TimelineType Type { get; set; }

        public string ColumnId { get; set; }

        public long UserId { get; set; }

        public string Query { get; set; }

        private Column()
        {
        }

        private Column(TimelineType type, string id, string name, long userId, int row, string query, bool enableNotity)
        {
            Type = type;
            ColumnId = id;
            Name = name;
            UserId = userId;
            Row = row;
            Query = query;
            EnableNotity = enableNotity;
        }

        public static Column CreateColumnInfo(TimelineType type, string name, long userId, int row, string query = null, bool enableNotity = true)
        {
            return new Column(type, $"Column-{Guid.NewGuid()}", name, userId, row, query, enableNotity);
        }

        public static Column RestoreColumnInfo(ApplicationDataCompositeValue adcv)
        {
            TimelineType type;
            if (!Enum.TryParse(adcv[nameof(Type)].ToString(), out type))
                throw new InvalidDataException();
            var info = new Column
            {
                Type = type,
                ColumnId = adcv[nameof(ColumnId)].ToString(),
                Name = adcv[nameof(Name)].ToString(),
                UserId = long.Parse(adcv[nameof(UserId)].ToString()),
                Row = int.Parse(adcv[nameof(Row)].ToString()),
                Query = adcv[nameof(Query)]?.ToString()
                // EnableNotity = bool.Parse(adcv[nameof(EnableNotity)].ToString())
            };

            return info;
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
            set { SetProperty(ref _name, value); }
        }

        #endregion

        #region Row

        private int _row;

        public int Row
        {
            get { return _row; }
            set { SetProperty(ref _row, value); }
        }

        #endregion

        #region EnableNotity

        private bool _enableNotify;

        public bool EnableNotity
        {
            get { return _enableNotify; }
            set { SetProperty(ref _enableNotify, value); }
        }

        #endregion
    }
}