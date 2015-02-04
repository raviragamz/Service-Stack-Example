using ServiceStack;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProteinTracker.Api
{
    public class HelloService: Service
    {
        /// <summary>
        /// Responds to any verb(HTTP- GET,POST,PUT), as along its type request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello," + request.Name };
        }
    }

    /// <summary>
    /// Convention for creating request. It has routes, defines how to access the request.
    /// /hello - "POST" JSON data that contains name, gets processed.
    /// "GET" /hello with Name, gets processed.
    /// </summary>
    [Route("/hello")]
    [Route("/hello/{Name}")]
    public class Hello
    {
        public string Name { get; set; }
    }
    /// <summary>
    /// Convention for creating response. RequestResponse, eg: HelloResponse.
    /// </summary>
    public class HelloResponse
    {
        public string Result { get; set; }
    }
}