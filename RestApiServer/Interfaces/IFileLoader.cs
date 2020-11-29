using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiServer.Interfaces
{
    public interface IFileLoader
    {
        Task<IEnumerable<string>> GetFileLinesAsync(string url);
        Task<string> GetFileText(string url);
    }
}