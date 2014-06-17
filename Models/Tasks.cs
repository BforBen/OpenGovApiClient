using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenGovApiClient.Models
{
    public class ServiceTask
    {
        private string _CategorySlug { get; set; }
        private string _Slug { get; set; }

        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public DateTime Updated { get; set; }
        public Uri Details { get; set; }

        public string Slug
        {
            get
            {
                if (_Slug == null)
                {
                    _Slug = Title.ToLower().Replace(" a ", " ").Replace(" the ", " ").Replace(" ", "-");
                }
                return _Slug;
            }
        }

        public string CategorySlug
        {
            get
            {
                if (_CategorySlug == null)
                {
                    _CategorySlug = CategoryName.ToLower().Replace(" a ", " ").Replace(" the ", " ").Replace(" - ", "-").Replace(" ", "-");
                }
                return _CategorySlug;
            }
        }
    }
}