using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProteinTracker.Api
{
    public interface IRepository
    {
        long AddUser(string name, int goal);
        IEnumerable<User> GetUsers();
        User GetUsers(long userId);
        void UpdateUser(User user);
    }


    public class Repository : IRepository
    {
        //build a constructor to accept redisclientmanager, since we used Funq to register and resolve.

        IRedisClientsManager RedisManager { get; set; }

        public Repository(IRedisClientsManager redisManager)
        {
            RedisManager = redisManager;
        }

        public long AddUser(string name, int goal)
        {
            using(var redisClient = RedisManager.GetClient())
            {
                var redisUsers = redisClient.As<User>();//typed RedisClient
                var user = new User() { Name = name, Goal = goal, Id = redisUsers.GetNextSequence() };
                redisUsers.Store(user);
                return user.Id;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var redisUsers = redisClient.As<User>();
                return redisUsers.GetAll();
            }
        }

        public User GetUsers(long userId)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var redisUsers = redisClient.As<User>();
                return redisUsers.GetById(userId);
            }
        }


        public void UpdateUser(User user)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var redisUsers = redisClient.As<User>();
                redisUsers.Store(user);//we are stroing in sets, if user already exists, it just updates user. We need not reinvent the wheel.
            }
        }
    }
}