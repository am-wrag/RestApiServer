using System.Collections.Generic;
using System.Threading.Tasks;
using RestApiServer.Models;

namespace RestApiServer.Interfaces
{
    public interface IHocrParser
    {
        Task<IList<HocrParseResult>> ParseLinesAsync(IEnumerable<string> xmlLines);

        Task<HocrParseResult> ParseLineAsync(string xmlLine);
    }
}