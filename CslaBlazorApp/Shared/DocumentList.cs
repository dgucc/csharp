using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Core;
using Csla.Rules;
using DataAccess;
using log4net;

namespace CslaBlazorApp.Shared {
	[Serializable]
	public class DocumentList: BusinessListBase<DocumentList, Document> {
		[Fetch]
		private void Fetch(int publicationId, [Inject] DataAccess.IDocumentDal dal, [Inject] IChildDataPortal<Document> documentPortal) {
			using (LoadListMode) {
				var data = dal.GetByPublication(publicationId).Select(d => documentPortal.FetchChild(d));
				AddRange(data);
			}
		}
	}
}
