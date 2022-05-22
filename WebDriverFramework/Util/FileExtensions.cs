using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OpenQA.Selenium;
using WebDriverFramework.WebDriver;

namespace WebDriverFramework.Util
{
    
    /// <summary>
    /// All actions for File Downloading
    /// </summary>
    public static class FileExtensions
    {
        private static readonly string pathToDoc = @"downloadedDocs/";

        public static void DownloadFile(IWebElement linkElement, string fileName)
        {
            var path = Directory.GetCurrentDirectory() + "/" + fileName;
            var href = linkElement.GetAttribute("href");
            WebClient webClient = new WebClient();
            webClient.DownloadFile(href, path);
        }

    }
}

