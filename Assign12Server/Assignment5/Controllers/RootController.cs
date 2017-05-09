using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment5.Controllers
{
    public class RootController : ApiController
    {
        // GET: /api/Root, /api, /api/
        public IHttpActionResult Get()
        {
            // Attention 17 - Collection of link objects
            List<Link> links = new List<Link>();
            links.Add(new Link() { Rel = "collection", Href = "/api/customers", Method = "GET,POST,PUT" });
            links.Add(new Link() { Rel = "collection", Href = "/api/invoices", Method = "GET" });
            links.Add(new Link() { Rel = "set", Href = "/api/customers/{id}/setsalesrep", Method = "PUT" });

            // Create and configure a dictionary to hold the collection
            // We need to return a simple object, so a Dictionary<TKey, TValue> is ideal

            Dictionary<string, List<Link>> linkList = new Dictionary<string, List<Link>>();
            linkList.Add("Links", links);

            return Ok(linkList);
        }
    }
}
