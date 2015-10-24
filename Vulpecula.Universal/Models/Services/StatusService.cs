using System.Threading.Tasks;

using Vulpecula.Universal.Models.Services.Primitive;

namespace Vulpecula.Universal.Models.Services
{
    public class StatusService : AsyncService
    {
        private readonly CroudiaProvider _provider;
        private readonly string _text;

        public StatusService(CroudiaProvider provider, string text)
        {
            this._provider = provider;
            this._text = text;
        }

        public override async Task StartAsync()
        {
            await this._provider.Croudia.Statuses.UpdateAsync(status => this._text);
        }

        /// <summary>
        /// アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        public override void Dispose()
        {
            // nothing to do
        }
    }
}