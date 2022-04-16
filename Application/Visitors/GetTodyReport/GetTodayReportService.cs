using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Application.Interfaces;
using Application.Visitors.SaveVisitorInfo;
using Domain.Visitors;
using MongoDB.Driver;

namespace Application.Visitors.GetTodyReport;

public class GetTodayReportService:IGetTodayReportService
{

    private readonly IMongoDbContext<Visitor> _visitorMongoDbContext;
    private readonly IMongoCollection<Visitor> _visitorMongoCollection;

    public GetTodayReportService(IMongoDbContext<Visitor> visitorMongoDbContext)
    {
        _visitorMongoDbContext = visitorMongoDbContext;
        _visitorMongoCollection = _visitorMongoDbContext.GetCollection();
    }

    public ResultTodayReportDto Execute()
    {
        DateTime startDay=DateTime.Now.Date;
        DateTime endDate=DateTime.Now.AddDays(1);

        var todyPageViewCount = _visitorMongoCollection.AsQueryable()
            .Where(x => x.Time >= startDay && x.Time <= endDate).LongCount();

        var todyVisitorCount = _visitorMongoCollection.AsQueryable()
            .Where(x => x.Time >= startDay && x.Time <= endDate).GroupBy(x=>x.VisitorId).LongCount();

        var allPageViewCount = _visitorMongoCollection.AsQueryable().LongCount();

        var allVisitorCount = _visitorMongoCollection.AsQueryable().GroupBy(x=>x.VisitorId).LongCount();


        var todyPageViewList=_visitorMongoCollection.AsQueryable()
            .Where(x => x.Time >= startDay && x.Time <= endDate)
            .Select(x=>new {x.Time}).ToList();

        var visitors =
            _visitorMongoCollection
                .AsQueryable()
                .OrderByDescending(x => x.Time)
                .Take(20)
                .Select(x => new VisitorsDto()
                {
                    Id = x.Id,
                    Browser = x.Browser.Family,
                    Time = x.Time,
                    Ip = x.Ip,
                    VisitorId = x.VisitorId,
                    CurrentLink = x.CurrentLink,
                    OperationSystem = x.Device.Family,
                    ReferrerLink = x.ReferrerLink,
                    IsSpider = x.Device.IsSpider,
                }).ToList();



        VisitCountDto visitPerHour = new VisitCountDto()
        {
            Display = new string[24],   
            Value = new int[24],
            
        };

        for (int i = 0; i <= 23; i++)
        {
            visitPerHour.Display[i] = $"h{i}";
            visitPerHour.Value[i] = todyPageViewList.Where(x => x.Time.Hour == i).Count(); 
        }



        DateTime currentMonthStart = DateTime.Now.Date.AddDays(-30);
        DateTime endMonthStart = DateTime.Now.Date.AddDays(1);


        var month_PageViewList = _visitorMongoCollection.AsQueryable()
            .Where(x => x.Time >= currentMonthStart && x.Time < endMonthStart).Select(x => new {x.Time}).ToList();

        VisitCountDto visitPerDay = new VisitCountDto()
        {
            Display = new string[31],
            Value = new int[31],
        };

        for (int i = 0; i < 31; i++)
        {
            var currentDay = DateTime.Now.Date.AddDays(i * (-1));

            visitPerDay.Display[i] = i.ToString();


            visitPerDay.Value[i] = month_PageViewList.Where(x => x.Time.Date == currentDay.Date).Count();
        }

        return new ResultTodayReportDto()
        {
            GeneralStats = new GeneralStateDto()
            {
                TotalVisitors = allVisitorCount,
                TotalPageViews = allPageViewCount,
               PageViewPerVisit =(long)GetAvg(allPageViewCount,allVisitorCount),
               VisitPerDay=visitPerDay,
                        
            },
            Tody = new TodyDto()
            {
                Visitors = todyVisitorCount,
                PageView = todyPageViewCount,
                ViewPerVisitor =GetAvg(todyPageViewCount,todyVisitorCount),  
                VisitPerHour = visitPerHour,
            },
            Visitor = visitors,
            
        };
    }

    private float GetAvg(long visitPage, long visitor)
    {
        if (visitor == 0)
        {
            return 0;
        }
        else
        {
            return visitPage / visitor  ;
        }
    }

}