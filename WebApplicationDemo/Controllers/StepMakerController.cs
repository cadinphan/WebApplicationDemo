using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationDemo.Controllers
{
    public class StepMakerController : Controller
    {
       

        public StepMakerController() { }

        /// <summary>
        /// Welcome
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            Log.Information("Step Maker Controller Start !!! ");
            return View();
        }

        /// <summary>
        /// Main View
        /// </summary>
        /// <returns></returns>
        public IActionResult Main()
        {
            return View();
        }

        /// <summary>
        /// _Step1_PV (Step 1)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="buttonType"></param>
        /// <param name="stepName"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult _Step1_PV(string data, string buttonType, string stepName)
        {
            Log.Information("Start Step 1");

            if (data != null && buttonType == "next")
            {
                return PartialView("~/Views/StepMaker/PartialViews/_Step2_PV.cshtml", data); //Step 2
            }

            else if (buttonType == "prev")
            {
                return PartialView("~/Views/StepMaker/PartialViews/_Step1_PV.cshtml"); //Step 1
            }

            if (buttonType == "selected")
            {
                return PartialView($"~/Views/StepMaker/PartialViews/{stepName}.cshtml", data); // step selected
            }

            return Json("Fail");
        }

        /// <summary>
        /// _Step2_PV (Step 2)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="buttonType"></param>
        /// <param name="stepName"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult _Step2_PV(string data, string buttonType, string stepName)
        {
            Log.Information("Start Step 2");

            if (data != null && buttonType == "next")
            {
                return PartialView("~/Views/StepMaker/PartialViews/_Step3_PV.cshtml", data); //Step 3
            }
            else if (buttonType == "prev")
            {
                return PartialView("~/Views/StepMaker/PartialViews/_Step1_PV.cshtml", data); //Step 1 
            }

            if (buttonType == "selected")
            {
                return PartialView($"~/Views/StepMaker/PartialViews/{stepName}.cshtml", data); // step selected
            }

            return Json("Fail");
        }

        /// <summary>
        /// _Step3_PV (Step 3)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="buttonType"></param>
        /// <param name="stepName"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult _Step3_PV(string data, string buttonType, string stepName)
        {
            Log.Information("Start Step 3");
            Log.Logger.Information("Start Step 3");

            if (data != null && buttonType == "next")
            {
                return PartialView("~/Views/StepMaker/PartialViews/_Step4_PV.cshtml", data); //Step 4
            }
            else if (buttonType == "prev")
            {
                return PartialView("~/Views/StepMaker/PartialViews/_Step2_PV.cshtml", data); //Step 2 
            }

            if (buttonType == "selected")
            {
                return PartialView($"~/Views/StepMaker/PartialViews/{stepName}.cshtml", data); // step selected
            }

            Log.Error("Fail");
            Log.Logger.Error("Fail Start Step 3");
            return Json("Fail");
            
        }

        /// <summary>
        /// _Step4_PV (Step 4)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="buttonType"></param>
        /// <param name="stepName"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult _Step4_PV(string data, string buttonType, string stepName)
        {
            Log.Information("Start Step 4");

            if (buttonType == "prev")
            {
                return PartialView("~/Views/StepMaker/PartialViews/_Step3_PV.cshtml", data); //Step 3 
            }

            if (buttonType == "selected")
            {
                return PartialView($"~/Views/StepMaker/PartialViews/{stepName}.cshtml", data); // step selected
            }

            return Json("Fail");
        }
    }
}