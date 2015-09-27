using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    public class Search : CroudiaApiImpl
    {
        internal Search(Croudia croudia) : base(croudia)
        {
        }

        /// <summary>
        /// ささやき（投稿）を検索して返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>q : string (* Required)</para>
        /// <para>count : int</para>
        /// <para>max_id : long</para>
        /// <para>since_id : long</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<SearchOwner> Voices(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<SearchOwner>(EndPoints.SearchVoices, parameters);
        }

        /// <summary>
        /// ユーザー(ユーザー名またはユーザーID)を検索して返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>q : string (* Required)</para>
        /// <para>count : int</para>
        /// <para>page : int</para>
        /// <para>trim_user : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> Users(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<IEnumerable<User>>(EndPoints.UsersSearch, parameters);
        }

        /// <summary>
        /// プロフィール検索をして <see cref="Vulpecula.Models.User"/> のリストを返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>q : string (* Required)</para>
        /// <para>count : int</para>
        /// <para>page : int</para>
        /// <para>trim_user : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> Profile(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<IEnumerable<User>>(EndPoints.ProfileSearch, parameters);
        }

        /// <summary>
        /// お気に入りしたささやき（投稿）を検索して返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>q : string (* Required)</para>
        /// <para>count : int</para>
        /// <para>max_id : long</para>
        /// <para>since_id : long</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<SearchOwner> Favorites(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<SearchOwner>(EndPoints.SearchFavorits, parameters);
        }
    }
}