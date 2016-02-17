using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        private static ColumnManager _instance;
        public static ColumnManager Instance => _instance ?? (_instance = new ColumnManager());

        public ObservableCollection<Column> Columns { get; }
        public bool IsInitialized { get; private set; }

        private ColumnManager()
        {
            Columns = new ObservableCollection<Column>();
            IsInitialized = false;
        }

        public async Task InitializeColumns()
        {
            try
            {
                var columns = Configuration.Instance.ColumnsLegacy;
                var tempColumn = new List<Column>();
                foreach (var columnComposite in columns)
                {
                    var column = Column.RestoreColumnInfo(columnComposite);
                    tempColumn.Add(column);
                    Debug.WriteLine($"Restored column {{ID:{column.ColumnId}, Name:{column.Name}, Query:{column.Query}, Row:{column.Row}}}.");
                }
                foreach (var source in tempColumn.OrderBy(w => w.Row))
                    Columns.Add(source);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                await MessageDialogWrapper.ShowOkMessageDialogAsync(LocalizationHelper.GetString("CanNotRestored"), "Error");
                var columns = Configuration.Instance.ColumnsLegacy;
                foreach (var columnComposite in columns)
                {
                    Columns.Clear();
                    Configuration.Instance.RemoveValueLegacy(columnComposite[nameof(Column.ColumnId)].ToString());
                }
                // 初期化
                SetupInitialColumns(AccountManager.Instance.Users.First().Id);
            }
            IsInitialized = true;
        }

        [UsedImplicitly]
        public void ClearColumns()
        {
            var columns = Configuration.Instance.ColumnsLegacy;
            foreach (var column in columns)
                RemoveColumn(Column.RestoreColumnInfo(column));
        }

        /// <summary>
        /// 初期カラムを設定します。
        /// </summary>
        /// <param name="userId"></param>
        public void SetupInitialColumns(long userId)
        {
            AddColumn(Column.CreateColumnInfo(TimelineType.Public, "public", userId, 0, enableNotity: false));
            AddColumn(Column.CreateColumnInfo(TimelineType.Mentions, "mentions", userId, 1));
            AddColumn(Column.CreateColumnInfo(TimelineType.DirectMessages, "messages", userId, 2));
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
                [nameof(Column.Query)] = info.Query,
                [nameof(Column.EnableNotity)] = info.EnableNotity
            };

            Columns.Add(info);
            Configuration.Instance.AddValueLegacy(info.ColumnId, composite);
        }

        public void RemoveColumn(Column info)
        {
            Columns.Remove(info);
            Configuration.Instance.RemoveValueLegacy(info.ColumnId);
            Debug.WriteLine($"Removed column {{ID:{info.ColumnId}, Name:{info.Name}, Query:{info.Query}}}.");
        }

        public void RewriteColumn(Column info)
        {
            var composite = new ApplicationDataCompositeValue
            {
                [nameof(Column.Type)] = info.Type.ToString(),
                [nameof(Column.ColumnId)] = info.ColumnId,
                [nameof(Column.Name)] = info.Name,
                [nameof(Column.UserId)] = info.UserId,
                [nameof(Column.Row)] = info.Row,
                [nameof(Column.Query)] = info.Query,
                [nameof(Column.EnableNotity)] = info.EnableNotity
            };

            Configuration.Instance.RewriteValueLegacy(info.ColumnId, composite);
        }
    }
}