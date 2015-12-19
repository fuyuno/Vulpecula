using System.Linq;

using Microsoft.Practices.Unity.Utility;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Triggers
{
    public class CallMethodAction : TriggerAction<Element>
    {
        public string MethodName { get; set; }

        // TODO: Impl
        public Binding TargetObject { get; set; }

        protected override void Invoke(Element sender)
        {
            if (MethodName == null)
            {
                return;
            }

            var bindingContext = sender.BindingContext;
            if (TargetObject?.Path == null || TargetObject.Path == ".")
            {
                var method = bindingContext.GetType().GetMethodsHierarchical().FirstOrDefault(w => w.IsPublic && w.Name == MethodName);
                method?.Invoke(bindingContext, null);
            }
            else
            {
                var target = TargetObject.Path;
                var prop = bindingContext.GetType().GetPropertiesHierarchical().FirstOrDefault(w => w.CanRead && w.Name == target);
                var method = prop.GetValue(bindingContext).GetType().GetMethodsHierarchical().FirstOrDefault(w => w.IsPublic && w.Name == MethodName);
                method?.Invoke(prop.GetValue(bindingContext), null);
            }
        }
    }
}