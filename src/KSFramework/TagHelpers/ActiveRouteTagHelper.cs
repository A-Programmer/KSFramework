
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace KSFramework.TagHelpers
{
    [HtmlTargetElement(Attributes = ActiveRouteClassValue)]
    public class ActiveRouteTagHelper : TagHelper
    {
        private const string ActiveRouteClassValue = "active-route-class";

        [HtmlAttributeName(ActiveRouteClassValue)]
        public string ActiveRouteCssClass { get; set; } = string.Empty;
        private IDictionary<string, string>? _routeValues;

        /// <summary>The name of the action method.</summary>
        /// <remarks>Must be <c>null</c> if <see cref="P:Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper.Route" /> is non-<c>null</c>.</remarks>
        [HtmlAttributeName("asp-action")]
        public string? Action { get; set; }

        /// <summary>The name of the controller.</summary>
        /// <remarks>Must be <c>null</c> if <see cref="P:Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper.Route" /> is non-<c>null</c>.</remarks>
        [HtmlAttributeName("asp-controller")]
        public string? Controller { get; set; }

        /// <summary>Additional parameters for the route.</summary>
        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                _routeValues ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                return _routeValues;
            }
            set => _routeValues = value;
        }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.ViewContext" /> for the current request.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public required ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (ShouldBeActive())
            {
                MakeActive(output);
            }

            output.Attributes.RemoveAll(ActiveRouteClassValue);
        }

        private bool ShouldBeActive()
        {
            var currentController = ViewContext.RouteData.Values["Controller"]?.ToString() ?? string.Empty;
            var currentAction = ViewContext.RouteData.Values["Action"]?.ToString() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(Controller) && !string.Equals(Controller, currentController, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(Action) && !string.Equals(Action, currentAction, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            foreach (KeyValuePair<string, string> routeValue in RouteValues)
            {
                if (!ViewContext.RouteData.Values.ContainsKey(routeValue.Key) ||
                    ViewContext.RouteData.Values[routeValue.Key].ToString() != routeValue.Value)
                {
                    return false;
                }
            }

            return true;
        }

        private void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttr == null)
            {
                classAttr = new TagHelperAttribute("class", ActiveRouteCssClass);
                output.Attributes.Add(classAttr);
            }
            else
            {
                var currentClasses = classAttr.Value?.ToString() ?? string.Empty;
                if (!currentClasses.Contains(ActiveRouteCssClass))
                {
                    var newClasses = string.IsNullOrEmpty(currentClasses)
                        ? ActiveRouteCssClass
                        : $"{currentClasses} {ActiveRouteCssClass}";
                    output.Attributes.SetAttribute("class", newClasses);
                }
            }
        }
    }
}