using BenchmarkDotNet.Attributes;
using Crowler.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crowler.ViewModel;
using System.Threading;
using System.IO;

namespace Crowler.Service
{
    public interface IPostService
    {
        string GetVideoAddress();
    }

    //[MemoryDiagnoser]
    public class PostService : IPostService

    {


        private readonly IWebHostEnvironment _hostEnvironment;

        public PostService(IWebHostEnvironment hostEnvironment)
        {

            _hostEnvironment = hostEnvironment;
        }



        //[Benchmark]
        public /*async*/ string GetVideoAddress()
        {
            if (string.IsNullOrWhiteSpace(_hostEnvironment.WebRootPath))
            {
                _hostEnvironment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var driverpath = _hostEnvironment.WebRootPath;



            //var driverpath = _hostEnvironment.WebRootPath;
            //var driverpath = @"G:\Projects\AppSaz\Crowler\Crowler\Crowler\wwwroot";
            //var options = new ChromeOptions();
            //options.AddArgument("--headless");
            string url = "https://www.tiktok.com/@respect.lame_khaby/video/7076115475150703877?is_copy_url=1&is_from_webapp=v1";



            string videoAddress = "";

            using (IWebDriver driver = new ChromeDriver(driverpath))
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    driver.Navigate().GoToUrl(url);

                    Thread.Sleep(3000);
                    IWebElement firstResult = wait.Until(e => e.FindElement(By.CssSelector("video")));

                    firstResult.Click();
                    //driver.FindElement(By.CssSelector("video")).Click();
                    IWebElement secondResult = wait.Until(e => e.FindElement(By.CssSelector("video")));
                    videoAddress = secondResult.GetAttribute("src");
                    //driver.Quit();
                }
                catch (Exception ex)
                {
                    //driver.Quit();
                }
                finally
                {
                    driver?.Close();
                    driver?.Quit();
                    driver?.Dispose();
                }
                driver.Quit();
            }

            return videoAddress;
        }
    }
}