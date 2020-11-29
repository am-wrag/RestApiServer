using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestApiServer.Interfaces;
using RestApiServer.Models;
using RestApiServer.Models.Responses;

namespace RestApiServer.Services
{
    public class HocrMapper : IHocrMapper
    {

        public Task<HocrDocumentDto> MapAsync(string documentName, IEnumerable<HocrParseResult> hocrList)
        {
            return Task.Run(() => Map(documentName, hocrList));
        }

        public Task<HocrResponse> MapAsync(IEnumerable<HocrDto> hocrList)
        {
            return Task.Run(() =>
            {
                return new HocrResponse
                {
                    Results = hocrList.Select(h => h.FullValue).ToArray()
                };
            });
        }

        public Task<HocrDocumentResponse> MapAsync(IEnumerable<HocrDocumentDto> hocrDocuments)
        {
            return Task.Run(() =>
            {
                return new HocrDocumentResponse
                {
                    Documents = hocrDocuments.Select(h => h.Value).ToArray()
                };
            });
        }

        private HocrDocumentDto Map(string documentName, IEnumerable<HocrParseResult> hocrList)
        {
            var hocrDtoList = new List<HocrDto>();
            var sb = new StringBuilder();
            sb.AppendLine("[");

            foreach (var hocrParseResult in hocrList)
            {
                var hocrDto = new HocrDto
                {
                    Class = hocrParseResult.Class,
                    Id = hocrParseResult.Id,
                    Title = hocrParseResult.Title,
                    Lang = hocrParseResult.Lang,
                    FullValue = JsonConvert.SerializeObject(hocrParseResult)
                };
                hocrDtoList.Add(hocrDto);
                sb.Append(hocrDto.FullValue);
                sb.AppendLine(",");
            }

            var fullJson = $"{sb.ToString().TrimEnd(',')}]";

            return new HocrDocumentDto()
            {
                DocumentName = documentName,
                Hocrs = hocrDtoList,
                Value = fullJson
            };
        }
    }
}