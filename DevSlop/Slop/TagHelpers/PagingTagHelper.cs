using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevSlop.Slop.TagHelpers
{
    using System;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    public class PagingTagHelper : TagHelper
    {
        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public string Route { get; set; }

        public string SortString { get; set; }

        public bool SortAscending { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder result = new StringBuilder();
            var totalPages = this.GetTotalPages();
            for (int i = 1; i <= totalPages; i++)
            {
                // begin an <a> tag
                result.Append("<a ");

                // create href
                result.Append("href ='");
                result.Append(this.Route);
                result.Append("?page=");
                result.Append(i);
                result.Append("&sortString=");
                result.Append(this.SortString);
                result.Append("&sortAscending=");
                result.Append(this.SortAscending);
                result.Append("' ");

                // create css if selected
                if (i == this.CurrentPage)
                {
                    result.Append("class=\"selected\" ");
                }

                result.Append(">");
                result.Append(i);
                result.Append("</a> ");
            }

            output.Content.SetHtmlContent(result.ToString());
        }

        private int GetTotalPages()
        {
            return (int)Math.Ceiling((decimal)this.TotalItems / this.ItemsPerPage);
        }
    }
}
