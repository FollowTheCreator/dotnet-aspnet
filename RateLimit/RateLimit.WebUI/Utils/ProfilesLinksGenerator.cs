using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RateLimit.WebUI.Models.Profile;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;

namespace RateLimit.WebUI.Utils
{
    public static class ProfilesLinksGenerator
    {
        public static HtmlString GenerateSortLink(this IHtmlHelper helper, ProfilesSortState sortState, ViewDataDictionary viewData)
        {
            return new HtmlString($"<a class=\"text-dark\" href=\"/?SortState={sortState}&Filter={viewData["Filter"]}\">{sortState.ToString()}</a>");
        }

        public static HtmlString GeneratePageLink(this IHtmlHelper helper, int pageNumber, ViewDataDictionary viewData, string styleClass)
        {
            return new HtmlString($"<a class=\"{styleClass}\" href=\"/?PageNumber={pageNumber}&SortState={viewData["SortState"]}&Filter={viewData["Filter"]}\">{pageNumber.ToString()}</a>");
        }
    }
}
