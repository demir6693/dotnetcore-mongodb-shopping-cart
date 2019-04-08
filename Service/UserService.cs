using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("WebShopDb"));
        var database = client.GetDatabase("WebShopDb");
        _users = database.GetCollection<User>("User");
    }

    public List<User> Get()
    {
        return _users.Find(user => true).ToList();
    }

    public User Get(string id)
    {
        return _users.Find<User>(user => user.Id == id).FirstOrDefault();
    }

    public User Create(User user)
    {
        _users.InsertOne(user);
        return user;
    }

    public void Update(string id, User userIn)
    {
        _users.ReplaceOne(user => user.Id == id, userIn);
    }

    public void Remove(User userIn)
    {
        _users.DeleteOne(user => user.Id == userIn.Id);
    }

    public void Remove(string id)
    {
        _users.DeleteOne(user => user.Id == id);
    }
}