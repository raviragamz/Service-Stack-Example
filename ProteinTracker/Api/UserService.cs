using ServiceStack;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProteinTracker.Api
{
    public class UserService : Service
    {
        /// <summary>
        /// Service Stack is going to inject a repository, bcoz we registered it in global.asax
        /// </summary>
        public IRepository Repository { get; set; }

        //add user using a POST, dont let do with a GET.
        public object Post(AddUser request)
        {
            var id = Repository.AddUser(request.Name, request.Goal);
            return new AddUserResponse { UserId = id };
        }

        public object Get(ProteinTrackerUsers request)
        {
            return new ProteinTrackerUsersResponse { ProteinTrackerUsers = Repository.GetUsers() };
        }

        public object Post(AddProtein request)
        {
            var user = Repository.GetUsers(request.UserId);
            user.Total += request.Amount;
            Repository.UpdateUser(user);
            return new AddProteinResponse { NewTotal = user.Total };
        }
    }
}