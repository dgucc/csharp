using System.Reflection;
using Csla;
using log4net;

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
