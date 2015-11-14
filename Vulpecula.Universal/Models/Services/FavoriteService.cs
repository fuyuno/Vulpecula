using System.Threading.Tasks;

using Vulpecula.Universal.Models.Services.Primitive;

namespace Vulpecula.Universal.Models.Services
{
    public class FavoriteService : AsyncService
    {
        private readonly bool _isNot;
        private readonly CroudiaProvider _provider;
        private readonly long _statusId;

        public FavoriteService(CroudiaProvider provider, long statusId, bool isNot)
        {
            this._provider = provider;
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
            {
                await _provider.Croudia.Favorites.DestroyAsync(_statusId);
            }
            else
            {
                await _provider.Croudia.Favorites.CreateAsync(_statusId);
            }
        }
    }
}