using System.Runtime.CompilerServices;
using Application.Visitors.SaveVisitorInfo;
using Microsoft.AspNetCore.Mvc.Filters;
using UAParser;

namespace WebSite.EndPoint.Utilities.Filters
{
    public class SaveVisitorFilter:IActionFilter
    {

        private readonly ISaveVisitorInfoService _saveVisitorInfoService;

        public SaveVisitorFilter(ISaveVisitorInfoService saveVisitorInfoService)
        {
            _saveVisitorInfoService = saveVisitorInfoService;
        }



        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { 
            string ip = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            string actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor) context.ActionDescriptor).ActionName;
            string controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor) context.ActionDescriptor).ControllerName;
            var referer = context.HttpContext.Request.Headers["Referer"].ToString();
            var currentUrl = context.HttpContext.Request.Path;
            var userAgent = context.HttpContext.Request.Headers["User-Agent"];
            var request = context.HttpContext.Request;

            var uaParser = Parser.GetDefault();

           ClientInfo clientInfo=uaParser.Parse(userAgent);


           _saveVisitorInfoService.Execute(new RequestSaveVisitorDto()
           {
               Browser = new VisitorVersionDto()
               {
                   Family = clientInfo.UA.Family,
                   Version = $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}",
               },
               CurrentLink = currentUrl,
               Device = new DeviceDto()
               {
                   Brand = clientInfo.Device.Brand,
                   Family = clientInfo.Device.Family,
                   IsSpider = clientInfo.Device.IsSpider,
                   Model=clientInfo.Device.Model,
               },
               Ip=ip,
               Method = request.Method,
               OperationSystem = new VisitorVersionDto()
               {
                   Family = clientInfo.OS.Family,
                    Version = $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}",
               },
               PhysicalPath = $"{controllerName}/{actionName}",
               Protocol =request.Protocol,
               ReferrerLink = referer,
           });
        }
    }
}
