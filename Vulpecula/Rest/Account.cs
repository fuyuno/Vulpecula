using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    internal class Account : CroudiaApiImpl
    {
        public Account(Croudia croudia) : base(croudia)
        {
        }

        /// <summary>
        /// 認証に成功するとリクエストしたユーザーの <see cref="Vulpecula.Models.User"/> を返します。
        /// </summary>
        /// <returns></returns>
        public async Task<User> VerifyCredentials()
        {
            return await this.Croudia.GetAsync<User>(EndPoints.AccountVeriryCredentials);
        }

        /// <summary>
        /// <para>認証ユーザーのプロフィール画像を更新します。</para>
        /// <para>フォーマットはJPG, GIF, PNGに対応しており、幅が120ピクセルを超える場合は自動的に縮小されます。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>image : <see cref="System.IO.Stream"/> (* Required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> UpdateProfileImage(Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<User>(EndPoints.AccountUpdateProfileImage, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーのカバー画像を更新します。フォーマットはJPG, GIF, PNGに対応しています。</para>
        /// <para>カバー画像は550×200のサイズに拡大縮小される為、550×200に近いサイズの画像をアップロードすることを利用者にオススメしてください。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>image : <see cref="System.IO.Stream"/> (* Required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> UpdateCoverImage(Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<User>(EndPoints.AccountUpdateCoverImage, parameters);
        }

        /// <summary>
        /// 認証ユーザーのプロフィール情報を更新します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>name : string</para>
        /// <para>url : string</para>
        /// <para>location : string</para>
        /// <para>description : string</para>
        /// <para>timersec : int</para>
        /// <para>protected : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> UpdateProfile(Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<User>(EndPoints.AccountUpdateProfile, parameters);
        }
    }
}