using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteinTracker.Api
{
    [Route("/users/{userid}","POST")]
    public class AddProtein
    {
        public long UserId { get; set; }
        public int Amount { get; set; }
    }
}
