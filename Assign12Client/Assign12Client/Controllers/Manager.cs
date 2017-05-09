using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Assign12Client.SSOS;

namespace Assign12Client.Controllers
{
    public class Manager
    {
        // Attention 2 - Reference to the web service proxy class
        SubjectOutlineServicePortTypeClient so = new SubjectOutlineServicePortTypeClient();

        public SubjectOutline CallSoapXML(string courseCode)
        {
            try
            {
                // attempt to call fall 2016
                var fetchedObject = so.getOutline(courseCode, "20163", "SICT", true);

                return fetchedObject;
            }
            catch // if null
            {
                try
                {
                    // attempt to call winter 2017
                    var fetchedObject = so.getOutline(courseCode, "20171", "SICT", true);

                    return fetchedObject;
                }
                catch // if null again
                {
                    try
                    {
                        // attempt to call summer 2016
                        var fetchedObject = so.getOutline(courseCode, "20162", "SICT", true);

                        return fetchedObject;
                    }
                    catch // if null again
                    {
                        // attempt to call winter 2016
                        var fetchedObject = so.getOutline(courseCode, "20161", "SICT", true);

                        if (fetchedObject == null) // if null again finally...
                        {
                            // just return null
                            return null;
                        }
                        else
                        {
                            return fetchedObject;
                        }
                    }
                }                
            }
        }


        public async Task<IEnumerable<CourseBase>> CoursesGetAll(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var requestObj = CreateRequest()) // Create the HttpClient request object
            {
                var responseObj = await requestObj.GetAsync("https://ict.senecacollege.ca/api/courses/bsd");

                if (responseObj.IsSuccessStatusCode)
                {
                    return (await responseObj.Content.ReadAsAsync<IEnumerable<CourseBase>>());
                }
                else
                {
                    // return an empty collection if it is not status 200
                    IEnumerable<CourseBase> allCourses = new List<CourseBase>();
                    return allCourses;
                }
            }
        }


        // Attention 1 - This is a factory, which creates an HttpClient object for a web service request...
        // Configured with the base URI, and headers (accept and authorization)

        private HttpClient CreateRequest(string acceptValue = "application/json")
        {
            var request = new HttpClient();

            // Could also fetch the base address string from the app's global configuration
            // Base URI of the web service we are interacting with
            request.BaseAddress = new Uri("https://ict.senecacollege.ca/"); // localhost:36431/api/ 

            // Accept header configuration
            request.DefaultRequestHeaders.Accept.Clear();
            request.DefaultRequestHeaders.Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(acceptValue));

            // Attempt to get the token from session state memory
            // Info: https://msdn.microsoft.com/en-us/library/system.web.httpcontext.session(v=vs.110).aspx

            var token = HttpContext.Current.Session["token"] as string;

            if (string.IsNullOrEmpty(token)) { token = "empty"; }

            // Authorization header configuration
            request.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue
                ("Bearer", token);

            return request;
        }
    }
}