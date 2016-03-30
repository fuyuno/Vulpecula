using System;
using System.Collections.Generic;
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
    ///     カラムを管理します。
    /// </summary>
    public class ColumnManager
    {
        private readonly AccountManager _accountManager;
        private readonly ObservableCollection<Column> _columns;
        private readonly Configuration _configuration;

        public ColumnManager(Configuration configuration, AccountManager accountManager)
        {
            _columns = new ObservableCollection<Column>();
            _configuration = configuration;
            _accountManager = accountManager;
            _columns.CollectionChanged += (a, b) => _configuration.AddOrRewriteValue(ConfigurationKeys.ColumnsKey, _columns);
            Columns = new ReadOnlyObservableCollection<Column>(_columns);
        }

        public ReadOnlyObservableCollection<Column> Columns { get; }

        public async Task InitializeColumns()
        {
            try
            {
                var columns = _configuration.GetValue<IEnumerable<Column>>(ConfigurationKeys.ColumnsKey, new List<Column>())
                                            .ToArray();
                foreach (var source in columns.OrderBy(w => w.Row))
                {
                    source.Account = _accountManager.Accounts.Single(w => w.User.Id == source.UserId);
                    _columns.Add(source);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                await MessageDialogWrapper.ShowOkMessageDialogAsync(LocalizationHelper.GetString("CanNotRestored"), "Error");
                ClearColumns();

                // 初期化
                SetupInitialColumns(_accountManager.Accounts.First());
            }
        }

        [UsedImplicitly]
        public void ClearColumns()
        {
            _columns.Clear();
            _configuration.RemoveValue(ConfigurationKeys.ColumnsKey);
        }

        /// <summary>
        ///     初期カラムを設定します。
        /// </summary>
        /// <param name="account"></param>
        public void SetupInitialColumns(CroudiaAccount account)
        {
            AddColumn(Column.Create(TimelineType.Public, "public", 0, account));
            AddColumn(Column.Create(TimelineType.Mentions, "mentions", 1, account));
            AddColumn(Column.Create(TimelineType.DirectMessages, "messages", 2, account));
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