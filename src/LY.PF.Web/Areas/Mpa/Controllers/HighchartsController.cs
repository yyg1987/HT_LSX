using LY.PF.SaleFunnels;
using LY.PF.Web.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LY.PF.Web.Areas.Mpa.Controllers
{
    public class HighchartsController : PFControllerBase
    {
        // GET: Mpa/Highcharts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AreaIndex()
        {
            return View();
        }
        public ActionResult HospitalIndex()
        {
            return View();
        }
        public ActionResult Pie()
        {
            return View();
        }
        public ActionResult AreaPie()
        {
            return View();
        }

        public ActionResult ProductTypePie()
        {
            return View();
        }

        //取数据
        private readonly ISaleFunnelAppService _saleFunnelAppService;

        public HighchartsController(ISaleFunnelAppService saleFunnelAppService)
        {
            this._saleFunnelAppService = saleFunnelAppService;
        }

        public ActionResult GetData(string keyValue)
        {
            var data = _saleFunnelAppService.GetData(keyValue);

            return Content(JsonConvert.SerializeObject(data));
        }
        public ActionResult GetAreaData(string keyValue)
        {
            var data = _saleFunnelAppService.GetAreaData(keyValue);

            return Content(JsonConvert.SerializeObject(data));
        }
        public ActionResult GetHospitalData(string keyValue)
        {
            var data = _saleFunnelAppService.GetHospitalData(keyValue);

            return Content(JsonConvert.SerializeObject(data));
        }

        public ActionResult GetPieData(string keyValue)
        {
            var data = _saleFunnelAppService.GetPieData(keyValue);

            return Content(JsonConvert.SerializeObject(data));
        }

        public ActionResult GetAreaPieData(string keyValue)
        {
            var data = _saleFunnelAppService.GetAreaPieData(keyValue);

            return Content(JsonConvert.SerializeObject(data));
        }

        public ActionResult GetProductTypePieData(string keyValue)
        {
            var data = _saleFunnelAppService.GetProductTypePieData(keyValue);

            return Content(JsonConvert.SerializeObject(data));
        }
    }
}