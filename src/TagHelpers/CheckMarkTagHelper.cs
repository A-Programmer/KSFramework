

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace KSFramework.TagHelpers
{
    [HtmlTargetElement("i", Attributes = CheckMarkValueAttributeName)]
    public class CheckMarkTagHelper : TagHelper
    {
        private const string CheckMarkValueAttributeName = "ks-checkmark-value";
        private const string CheckMarkTrueTextValueAttributeName = "ks-checkmark-true-value";
        private const string CheckMarkFalseValueAttributeName = "ks-checkmark-false-value";
        private const string CheckMarkTrueCssAttributeName = "ks-checkmark-true-css-value";
        private const string CheckMarkFalseCssAttributeName = "ks-checkmark-false-css-value";

        [HtmlAttributeName(CheckMarkValueAttributeName)]
        public bool CheckMarkValue { get; set; }

        [HtmlAttributeName(CheckMarkTrueTextValueAttributeName)]
        public string CheckMarkTrueTextValue { get; set; }

        [HtmlAttributeName(CheckMarkFalseValueAttributeName)]
        public string CheckMarkFalseTextValue { get; set; }

        [HtmlAttributeName(CheckMarkTrueCssAttributeName)]
        public string CheckMarkTrueCssValue { get; set; }

        [HtmlAttributeName(CheckMarkFalseCssAttributeName)]
        public string CheckMarkFalseCssValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // string result = "";
            string classValue = "";
            string textValue = " ";
            if (CheckMarkValue)
            {
                classValue = CheckMarkTrueCssValue;
                textValue = CheckMarkTrueTextValue ?? "";
            }
            else
            {
                classValue = CheckMarkFalseCssValue;
                textValue = CheckMarkFalseTextValue ?? "";
            }

            output.Attributes.SetAttribute("class", classValue);
            output.Content.AppendHtml(textValue);

            base.Process(context, output);
        }

    }
}