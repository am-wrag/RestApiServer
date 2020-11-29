using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using RestApiServer.Interfaces;

namespace RestApiServer.Services
{
    public class FileLoader : IFileLoader
    {
        public async Task<IEnumerable<string>> GetFileLinesAsync(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            using var client = new HttpClient();
            await using var stream = await client.GetStreamAsync(url);
            using var streamReader = new StreamReader(stream);

            var result = new List<string>();
            var fileLine = await streamReader.ReadLineAsync();
            while (fileLine != null)
            {
                result.Add(fileLine);
                fileLine = await streamReader.ReadLineAsync();
            };

            return result;
        }

        public Task<string> GetFileText(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));

            using var client = new HttpClient();
            return client.GetStringAsync(url);
        }
    }
}