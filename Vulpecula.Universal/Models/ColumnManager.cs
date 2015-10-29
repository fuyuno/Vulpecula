using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Windows.Storage;

using JetBrains.Annotations;

using Vulpecula.Universal.Helpers;
using Vulpecula.Universal.Models.Dialogs;
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

        public async Task InitializeColumns()
        {
            try
            {
                var columns = App.AppSettings.Columns;
                foreach (var columnComposite in columns)
                {
                    var column = Column.RestoreColumnInfo(columnComposite);
                    if (column.Row >= this.Columns.Count)
                        this.Columns.Add(column);
                    else
                        this.Columns.Insert(column.Row, column);
                    Debug.WriteLine($"Restored column {{ID:{column.ColumnId}, Name:{column.Name}, Query:{column.Query}}}.");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                await MessageDialogWrapper.ShowOkMessageDialogAsync(LocalizationHelper.GetString("CanNotRestored"), "Error");
                var columns = App.AppSettings.Columns;
                foreach (var columnComposite in columns)
                {
                    this.Columns.Clear();
                    App.AppSettings.RemoveValue(columnComposite[nameof(Column.ColumnId)].ToString());
                }
            }
        }

        [UsedImplicitly]
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
            this.AddColumn(Column.CreateColumnInfo(TimelineType.Public, "public", userId, 0));
            this.AddColumn(Column.CreateColumnInfo(TimelineType.Mentions, "mentions", userId, 1));
            this.AddColumn(Column.CreateColumnInfo(TimelineType.DirectMessages, "messages", userId, 2));
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
                [nameof(Column.Row)] = info.Row,
                [nameof(Column.Query)] = info.Query
            };

            this.Columns.Add(info);
            App.AppSettings.AddValues(info.ColumnId, composite);
        }

        public void RemoveColumn(Column info)
        {
            this.Columns.Remove(info);
            App.AppSettings.RemoveValue(info.ColumnId);
            Debug.WriteLine($"Removed column {{ID:{info.ColumnId}, Name:{info.Name}, Query:{info.Query}}}.");
        }
    }
}