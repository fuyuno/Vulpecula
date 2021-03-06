﻿using System.Threading.Tasks;

using Vulpecula.Universal.Models;
using Vulpecula.Universal.Services.Primitive;

namespace Vulpecula.Universal.Services
{
    public class StatusDeleteService : AsyncService
    {
        private readonly bool _isDm;
        private readonly CroudiaAccount _provider;
        private readonly long _statusId;

        public StatusDeleteService(CroudiaAccount provider, long statusId, bool isDm)
        {
            _provider = provider;
            _statusId = statusId;
            _isDm = isDm;
        }

        /// <summary>
        /// アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        public override void Dispose()
        {
            // nothing to do
        }

        public override async Task StartAsync()
        {
            if (_isDm)
                await _provider.Croudia.SecretMails.DestroyAsync(_statusId);
            else
                await _provider.Croudia.Statuses.DestroyAsync(_statusId);
        }
    }
}