using System;
using Application.Interfaces;
using Domain.Visitors;
using MongoDB.Driver;

namespace Application.Visitors.SaveVisitorInfo;

public class SaveVisitorInfoService : ISaveVisitorInfoService
{

    private readonly IMongoDbContext<Visitor> _visitorMongoDbContext;
    private readonly IMongoCollection<Visitor> _visitorMongoCollection;

    public SaveVisitorInfoService(IMongoDbContext<Visitor> mongoDbContext)
    {
        _visitorMongoDbContext = mongoDbContext;
        _visitorMongoCollection = _visitorMongoDbContext.GetCollection();
    }

    public void Execute(RequestSaveVisitorDto request)
    {
        _visitorMongoCollection.InsertOne(new Visitor()
        {
            Browser = new VisitorVersion()
            {
                Family = request.Browser.Family,
                Version = request.Browser.Version,
            },
            CurrentLink = request.CurrentLink,
            Device = new Device()
            {
                Family = request.Device.Family,
                Brand = request.Device.Brand,
                IsSpider = request.Device.IsSpider,
                Model = request.Device.Model,
            },
            Ip = request.Ip,
            Method = request.Method,
            OperationSystem = new VisitorVersion()
            {
                Family = request.OperationSystem.Family,
                Version = request.OperationSystem.Version,
            },
            PhysicalPath = request.PhysicalPath,
            Protocol = request.Protocol,
            ReferrerLink = request.ReferrerLink,
            VisitorId = request.VisitorId,
            Time = DateTime.Now,
        });
    }
}