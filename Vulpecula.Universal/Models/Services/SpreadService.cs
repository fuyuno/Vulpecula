using System.Threading.Tasks;

using Vulpecula.Universal.Models.Services.Primitive;

namespace Vulpecula.Universal.Models.Services
{
    public class SpreadService : AsyncService
    {
        private readonly bool _isNot;
        private readonly CroudiaProvider _provider;
        private readonly long _statusId;

        public SpreadService(CroudiaProvider provider, long statusId, bool isNot)
        {
            _provider = provider;
            _statusId = statusId;
            _isNot = isNot;
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
            if (_isNot)
                await _provider.Croudia.Statuses.DestroyAsync(_statusId);
            else
                await _provider.Croudia.Statuses.SpreadAsync(_statusId);
        }
    }
}