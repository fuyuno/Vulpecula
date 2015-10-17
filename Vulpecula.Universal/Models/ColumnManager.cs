using System;
using System.Collections.ObjectModel;

using Windows.Storage;

using Vulpecula.Universal.Models.Timelines;

namespace Vulpecula.Universal.Models
{
    /// <summary>
    /// カラムを管理します。
    /// </summary>
    public class ColumnManager
    {
        public ObservableCollection<ColumnInfo> Columns { get; }

        public ColumnManager()
        {
            this.Columns = new ObservableCollection<ColumnInfo>();
        }

        public void InitializeColumns()
        {
            var columns = App.AppSettings.Columns;
            foreach (var column in columns)
                this.Columns.Add(ColumnInfo.RestoreColumnInfo(column));
        }

        public void ClearColumns()
        {
            var columns = App.AppSettings.Columns;
            foreach (var column in columns)
                this.RemoveColumn(ColumnInfo.RestoreColumnInfo(column));
        }

        /// <summary>
        /// 初期カラムを設定します。
        /// </summary>
        /// <param name="userId"></param>
        public void SetupInitialColumns(long userId)
        {
            this.AddColumn(ColumnInfo.CreateColumnInfo(TimelineType.Public, "public", userId));
            this.AddColumn(ColumnInfo.CreateColumnInfo(TimelineType.Mentions, "mentions", userId));
            this.AddColumn(ColumnInfo.CreateColumnInfo(TimelineType.DirectMessages, "messages", userId));
        }

        public void AddColumn(ColumnInfo info)
        {
            if (!info.ColumnId.StartsWith("Column-"))
                throw new ArgumentException(nameof(info));

            var composite = new ApplicationDataCompositeValue
            {
                [nameof(ColumnInfo.Type)] = info.Type.ToString(),
                [nameof(ColumnInfo.ColumnId)] = info.ColumnId,
                [nameof(ColumnInfo.Name)] = info.Name,
                [nameof(ColumnInfo.UserId)] = info.UserId,
                [nameof(ColumnInfo.Query)] = info.Query
            };

            this.Columns.Add(info);
            App.AppSettings.AddValues(info.ColumnId, composite);
        }

        public void RemoveColumn(ColumnInfo info)
        {
            this.Columns.Remove(info);
            App.AppSettings.RemoveValue(info.ColumnId);
        }
    }
}