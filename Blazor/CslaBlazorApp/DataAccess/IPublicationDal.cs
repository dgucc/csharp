using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess {
    public interface IPublicationDal {
        bool Exists(int id);
        PublicationDTO Get(int id);
        List<PublicationDTO> Get();
        PublicationDTO Insert(PublicationDTO publication);
        PublicationDTO Update(PublicationDTO publication);
        bool Delete(int id);
        //object Delete(Func<object, int> value);
        bool UpdateCover(int publicationId, byte[] cover);
	}
}
