using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RestApiServer.Interfaces;
using RestApiServer.Models;

namespace RestApiServer.Services
{
    public class HocrConverter : IHocrParser
    {

        public Task<IList<HocrParseResult>> ParseLinesAsync(IEnumerable<string> xmlLines)
        {
            return Task.Run(async () =>
            {
                var result = new List<HocrParseResult>();

                foreach (var row in xmlLines)
                {
                    var parseResult = await ParseLineAsync(row);
                    if(parseResult == null) continue;
                    result.Add(parseResult);
                }
                return (IList<HocrParseResult>)result;
            });
        }

        public Task<HocrParseResult> ParseLineAsync(string xmlLine)
        {
            return Task.Run(() => ParseLine(xmlLine));
        }

        private HocrParseResult ParseLine(string xmlRow)
        {
            var regex = GetHocrRegexVariant1();
            var result = regex.Match(xmlRow);

            if (result.Success)
            {
                var @class = result.Groups["Class"].Value;
                var id = result.Groups["Id"].Value;
                var title = result.Groups["Title"].Value;
                var lang = result.Groups["Lang"].Success ? result.Groups["Lang"].Value : null;
                return new HocrParseResult(id, @class, title, lang);
            }

            regex = GetHocrRegexVariant2();
            result = regex.Match(xmlRow);

            if (result.Success)
            {
                var @class = result.Groups["Class"].Value;
                var id = result.Groups["Id"].Value;
                var title = result.Groups["Title"].Value;
                var lang = result.Groups["Lang"].Value;
                return new HocrParseResult(id, @class, title, lang);
            }

            return null;
        }

        private string GetCsvValue(Match record)
        {
            var capture = record.Groups["Value"].Captures[0];
            var v1 = record.Groups["Value"].Value;
            return (capture.Length == 0 
                    || capture.Index == record.Index 
                    || record.Value[capture.Index - record.Index - 1] != '\"') 
                ? capture.Value 
                : capture.Value.Replace("\"\"", "\"");
        }

        private Regex GetHocrRegexVariant1()
        {
            return new Regex(
                @"class='(?<Class>[^']*)'\s
                        id='(?<Id>[^']*)'\s
                        title=['|""](?<Title>[^']*)['|""]\s? 
                        (lang='(?<Lang>[^']*)')?",
                RegexOptions.IgnorePatternWhitespace);
        }
        private Regex GetHocrRegexVariant2()
        {
            return new Regex(
                @"class='(?<Class>[^']*)'\s
                        id='(?<Id>[^']*)'\s
                        lang='(?<Lang>[^']*)'\s
                        title=['|""](?<Title>[^']*)['|""]",
                RegexOptions.IgnorePatternWhitespace);
        }
    }
}