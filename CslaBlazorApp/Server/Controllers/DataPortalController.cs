using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using log4net;
using log4net.Config;
using System.Reflection;

namespace CslaBlazorApp.Server.Controllers {

	[Route("api/[controller]")]
	[ApiController]
	public class DataPortalController : Csla.Server.Hosts.HttpPortalController {
		private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public DataPortalController(Csla.ApplicationContext applicationContext)
		  : base(applicationContext) { }

		[HttpGet]
		public string Get() {
			return "Running";
		}

		public override Task PostAsync([FromQuery] string operation) {
			var result = base.PostAsync(operation);
			_log.Info(string.Format("[API] PostAsync operation : {0}", operation));
			return result;
		}
	}

}
