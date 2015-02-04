using ServiceStack;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteinTracker.Api
{
    [Route("/users","POST")] //force this request to be a POST.
   public class AddUser
    {
        public string Name { get; set; }
        public int Goal { get; set; }
    }
}
