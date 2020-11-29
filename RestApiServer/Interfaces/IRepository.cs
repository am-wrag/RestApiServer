using System.Collections.Generic;
using System.Threading.Tasks;
using RestApiServer.Models;

namespace RestApiServer.Interfaces
{
    public interface IRepository
    {

        Task SaveDocument(HocrDocumentDto hocrDocument);

        Task<IEnumerable<HocrDocumentDto>> GetDocumentByName(string name);

        Task<IEnumerable<HocrDto>> GetHocrById(string id);

        Task<IEnumerable<HocrDto>> GetHocrByTitle(string documentName, string title);

        Task<IEnumerable<HocrDto>> GetHocrByClass(string documentName, string @class);

        Task<IEnumerable<HocrDto>> GetHocrByClassWithLang(string documentName, string @class, string lang);

        Task<IEnumerable<HocrDto>> GetHocrByTitleWithClass(string documentName, string title, string @class);

        Task<IEnumerable<HocrDto>> GetHocrByTitleWithLang(string documentName, string title, string lang);

        Task<IEnumerable<HocrDto>> GetHocr(string documentName, string title, string @class, string lang);

        Task CreateDbIfNotExist();
    }
}