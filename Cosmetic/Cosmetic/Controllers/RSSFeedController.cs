using Cosmetic.Models;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Net;  
using System.Web;  
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;  

namespace Cosmetic.Controllers  
{  
    public class RSSFeedController : Controller  
    {  
        [Route("[controller]/[action]")]
        public ActionResult Index()  
        {  
            return View();  
        }  
        [Route("[controller]/[action]")]
        [HttpPost]  
        public ActionResult Index(string RSSURL)  
        {  
            WebClient wclient = new WebClient();  
            string RSSData=wclient.DownloadString(RSSURL);  
  
            XDocument xml = XDocument.Parse(RSSData);  
            var RSSFeedData = (from x in xml.Descendants("item")  
                             select new ReadRSSFeed  
                             {  
                                 Title = ((string)x.Element("title")),  
                                 Link = ((string)x.Element("link")),  
                                 Description = ((string)x.Element("description")),  
                                 PubDate = ((string)x.Element("pubDate"))  
                             });  
            ViewBag.RSSFeed = RSSFeedData;  
            ViewBag.URL = RSSURL;  
            return View();  
        }  
    }  
}  