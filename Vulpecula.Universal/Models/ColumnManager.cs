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
        public ObservableCollection<Column> Columns { get; }

        public ColumnManager()
        {
            this.Columns = new ObservableCollection<Column>();
        }

        public void InitializeColumns()
        {
            var columns = App.AppSettings.Columns;
            foreach (var column in columns)
                this.Columns.Add(Column.RestoreColumnInfo(column));
        }

        public void ClearColumns()
        {
            var columns = App.AppSettings.Columns;
            foreach (var column in columns)
                this.RemoveColumn(Column.RestoreColumnInfo(column));
        }

        /// <summary>
        /// 初期カラムを設定します。
        /// </summary>
        /// <param name="userId"></param>
        public void SetupInitialColumns(long userId)
        {
            this.AddColumn(Column.CreateColumnInfo(TimelineType.Public, "public", userId));
            this.AddColumn(Column.CreateColumnInfo(TimelineType.Mentions, "mentions", userId));
            this.AddColumn(Column.CreateColumnInfo(TimelineType.DirectMessages, "messages", userId));
        }

        public void AddColumn(Column info)
        {
            if (!info.ColumnId.StartsWith("Column-"))
                throw new ArgumentException(nameof(info));

            var composite = new ApplicationDataCompositeValue
            {
                [nameof(Column.Type)] = info.Type.ToString(),
                [nameof(Column.ColumnId)] = info.ColumnId,
                [nameof(Column.Name)] = info.Name,
                [nameof(Column.UserId)] = info.UserId,
                [nameof(Column.Query)] = info.Query
            };

            this.Columns.Add(info);
            App.AppSettings.AddValues(info.ColumnId, composite);
        }

        public void RemoveColumn(Column info)
        {
            this.Columns.Remove(info);
            App.AppSettings.RemoveValue(info.ColumnId);
        }
    }
}