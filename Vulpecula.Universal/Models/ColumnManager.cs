using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly ObservableCollection<Column> _columns;
        public static ColumnManager Instance => _instance ?? (_instance = new ColumnManager());

        public ReadOnlyObservableCollection<Column> Columns { get; }
        public bool IsInitialized { get; private set; }

        private ColumnManager()
        {
            _columns = new ObservableCollection<Column>();
            _columns.CollectionChanged += (sender, args) => { Configuration.Instance.AddOrRewriteValue(ConfigurationKeys.ColumnsKey, _columns); };
            Columns = new ReadOnlyObservableCollection<Column>(_columns);
            IsInitialized = false;
        }

        public async Task InitializeColumns()
        {
            try
            {
                var columns = Configuration.Instance.Columns;
                foreach (var source in columns.OrderBy(w => w.Row))
                    _columns.Add(source);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                await MessageDialogWrapper.ShowOkMessageDialogAsync(LocalizationHelper.GetString("CanNotRestored"), "Error");
                ClearColumns();

                // 初期化
                SetupInitialColumns(AccountManager.Instance.Users.First().Id);
            }
            IsInitialized = true;
        }

        [UsedImplicitly]
        public void ClearColumns()
        {
            _columns.Clear();
            Configuration.Instance.RemoveValue(ConfigurationKeys.ColumnsKey);
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
            _columns.Add(info);
        }

        public void RemoveColumn(Column info)
        {
            _columns.Remove(info);
        }

        public void RewriteColumn(Column info)
        {
            for (var i = 0; i < _columns.Count; i++)
            {
                if (_columns[i].ColumnId == info.ColumnId)
                    _columns[i] = info;
            }
        }
    }
}