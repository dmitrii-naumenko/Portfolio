using System;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace NationalCriminalDatabase
{
    [ServiceContract]
    public interface INcSearchService
    {
        [OperationContract]
        bool Search(SearchParameter request, string email, int maxResultCount);

    }
}
