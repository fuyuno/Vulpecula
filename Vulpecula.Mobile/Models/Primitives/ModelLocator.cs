namespace Vulpecula.Mobile.Models.Primitives
{
    internal class ModelLocator
    {
        public static T GetModel<T>()
        {
            return default(T);
        }
    }
}