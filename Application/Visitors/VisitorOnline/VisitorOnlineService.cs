using System;
using System.Linq;
using Application.Interfaces;
using Domain.Visitors;
using MongoDB.Driver;

namespace Application.Visitors.VisitorOnline;

public class VisitorOnlineService : IVisitorOnlineService
{

    private readonly IMongoDbContext<OnlineVisitor> _visitorDbContext;
    private readonly IMongoCollection<OnlineVisitor> _mongoCollection;

    public VisitorOnlineService(IMongoDbContext<OnlineVisitor> visitorDbContext)
    {
        _visitorDbContext = visitorDbContext;
        _mongoCollection = _visitorDbContext.GetCollection();
    }

    public void ConnectUser(string visitorId)
    {
        var exist = _mongoCollection.AsQueryable().FirstOrDefault(x => x.visitorId == visitorId);

        if (exist == null)
        {
            _mongoCollection.InsertOne(new OnlineVisitor()
            {
                visitorId = visitorId,
                Time = DateTime.Now,
            });
        }

    }

    public void DisConnectUser(string visitorId)
    {
        _mongoCollection.FindOneAndDelete(x => x.visitorId == visitorId);
    }

    public int GetCount()
    {
        return _mongoCollection.AsQueryable().Count();
    }
}