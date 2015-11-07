using System;

namespace Vulpecula.Mobile.Models
{
    /// <summary>
    /// Interface を実装した Model を、自動的にバインドします。
    /// </summary>
    public class ModelLocator
    {
        private readonly string _formatter;

        /// <summary>
        /// </summary>
        /// <param name="namespace">
        /// ベース名前空間
        /// e.g. Vulpecula.iOS.Models
        /// </param>
        /// <param name="assembly">
        /// アセンブリ名
        /// e.g. Vulpecula.iOS
        /// </param>
        /// <param name="version">
        /// アセンブリバージョン
        /// e.g. 1.0.0.0
        /// </param>
        /// <param name="culture">
        /// アセンブリカルチャー
        /// e.g. neutral
        /// </param>
        public ModelLocator(string @namespace, string assembly, string version, string culture)
        {
            _formatter = $"{@namespace}.{{0}}, {assembly}, Version={version}, Culture={culture}";
        }

        public ModelLocator(string @namespace, string assemblyQualifiedName)
        {
            _formatter = $"{@namespace}.{{0}}, {assemblyQualifiedName}";
        }

        public T GetModel<T>()
        {
            try
            {
                var clazz = string.Format(_formatter, typeof (T).Name.Substring(1));
                var type = Type.GetType(clazz);
                var instance = Activator.CreateInstance(type);
                if (instance is T)
                    return (T)instance;
            }
            catch
            {
                // ignored
            }
            return default(T);
        }
    }
}