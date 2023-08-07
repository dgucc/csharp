using Csla;

namespace CslaBlazorApp.Shared {
	[Serializable]
	public class DocumentList: BusinessListBase<DocumentList, Document> {

        public DocumentList(){
			MarkAsChild();
        }

        [Fetch]
		private void Fetch(int publicationId, [Inject] DataAccess.IDocumentDal dal, [Inject] IChildDataPortal<Document> documentPortal) {
			using (LoadListMode) {
				var data = dal.GetByPublication(publicationId).Select(d => documentPortal.FetchChild(d));
				//var data = dal.Get().Select(d => documentPortal.FetchChild(d));
				MarkAsChild();
				AddRange(data);
			}
		}
	}
}
