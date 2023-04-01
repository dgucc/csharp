using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Csla;
using log4net;
using log4net.Config;

namespace CslaBlazorApp.Shared {

	[Serializable]
	public class Publications : ReadOnlyListBase<Publications, Publication> {

		private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		[Fetch]
		private void Fetch([Inject] DataAccess.IPublicationDal dal, [Inject] IChildDataPortal<Publication> publicationInfoPortal) {
			using (LoadListMode) {
				_log.Info("IChildDataPortal<Publication> => FetchChild()");
				var data = dal.Get().Select(d => publicationInfoPortal.FetchChild(d));
				AddRange(data);
			}
		}
	}
}
