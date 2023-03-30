using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess {
	public interface IDocumentDal {
		bool Exists(int id);
		DocumentDTO Get(int id);
		List<DocumentDTO> Get();
		List<DocumentDTO> GetByPublication(int publicationId);
		DocumentDTO Insert(DocumentDTO document);
		DocumentDTO Update(DocumentDTO document);
		bool Delete(int id);
	}
}
