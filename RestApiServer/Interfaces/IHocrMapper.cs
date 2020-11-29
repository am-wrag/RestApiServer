using System.Collections.Generic;
using System.Threading.Tasks;
using RestApiServer.Models;
using RestApiServer.Models.Responses;

namespace RestApiServer.Interfaces
{
    public interface IHocrMapper
    {
        Task<HocrDocumentDto> MapAsync(string documentName, IEnumerable<HocrParseResult> hocrList);
 
        Task<HocrResponse> MapAsync(IEnumerable<HocrDto> hocrList);

        Task<HocrDocumentResponse> MapAsync(IEnumerable<HocrDocumentDto> hocrDocuments);
    }
}