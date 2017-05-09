using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assign10.Controllers
{
    // Attention - Sharer entity resource models

    public class SharerAdd
    {
        [Required, StringLength(100)]
        public string Username { get; set; }

        [Required, StringLength(30)]
        public string AccessLevel { get; set; }

        [Range(1, UInt32.MaxValue)]
        public int ProjectId { get; set; }
    }

    public class SharerBase : SharerAdd
    {
        public int Id { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    // ############################################################

    // Classes that have link relations

    // Delivery only, no data annotations needed
    public class SharerAddTemplate
    {
        public string Username { get { return "Email username"; } }
        public string AccessLevel { get { return "Access level, shared, public, private"; } }
        public string ProjectId { get { return "The project id"; } }
    }

    public class SharerWithLink : SharerBase
    {
        public Link Link { get; set; }
    }

    public class SharerLinked : LinkedItem<SharerWithLink>
    {
        // Constructor - call the base class constructor

        // All use cases except "add new"
        public SharerLinked(SharerWithLink item) : base(item) { }

        // "Add new" use case
        public SharerLinked(SharerWithLink item, int id) : base(item, id) { }
    }

    public class SharersLinked : LinkedCollection<SharerWithLink>
    {
        // Constructor - call the base class constructor
        public SharersLinked(IEnumerable<SharerWithLink> collection) : base(collection)
        {
            Template = new SharerAddTemplate();
        }

        public SharerAddTemplate Template { get; set; }
    }
}