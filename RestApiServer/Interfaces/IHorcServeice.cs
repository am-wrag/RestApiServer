using System.Threading.Tasks;
using RestApiServer.Models;
using RestApiServer.Models.Responses;

namespace RestApiServer.Interfaces
{
    public interface IHocrService
    {
        Task AddDocument(HocrLinkRequest hocrLink);
        Task<HocrResponse> GetHocr(string id);

        Task<HocrResponse> GetHocrByClass(string documentName, string @class);

        Task<HocrResponse> GetHocrByTitle(string documentName, string title);

        Task<HocrResponse> GetHocrByClassWithTitle(string documentName, string @class, string title);

        Task<HocrResponse> GetHocrByTitleWithLang(string documentName, string title, string lang);

        Task<HocrResponse> GetHocrByClassWithLang(string documentName, string @class, string lang);

        Task<HocrResponse> GetHocr(string documentName, string @class, string title, string lang);

        Task<HocrDocumentResponse> GetDocument(string name);
    }
}