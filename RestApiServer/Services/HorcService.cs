using System;
using System.Threading.Tasks;
using RestApiServer.Interfaces;
using RestApiServer.Models;
using RestApiServer.Models.Responses;

namespace RestApiServer.Services
{
    public class HocrService : IHocrService
    {
        private readonly IRepository _repository;
        private readonly IFileLoader _fileLoader;
        private readonly IHocrParser _hocrParser;
        private readonly IHocrMapper _hocrMapper;

        public HocrService(
            IRepository repository, IFileLoader fileLoader, IHocrParser hocrParser, IHocrMapper hocrMapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _fileLoader = fileLoader ?? throw new ArgumentNullException(nameof(fileLoader));
            _hocrParser = hocrParser ?? throw new ArgumentNullException(nameof(hocrParser));
            _hocrMapper = hocrMapper ?? throw new ArgumentNullException(nameof(hocrMapper));
        }

        public async Task AddDocument(HocrLinkRequest hocrLink)
        {
            if(hocrLink?.Url == null) return;

            var fileLines = await _fileLoader.GetFileLinesAsync(hocrLink.Url);
            var hocrList = await _hocrParser.ParseLinesAsync(fileLines);
            var newDocument = await _hocrMapper.MapAsync(hocrLink.Url, hocrList);

            await _repository.CreateDbIfNotExist();
            await _repository.SaveDocument(newDocument);
        }

        public async Task<HocrResponse> GetHocr(string id)
        {
            var result = await _repository.GetHocrById(id);
            return await _hocrMapper.MapAsync(result);
        }

        public async Task<HocrResponse> GetHocrByClass(string documentName, string @class)
        {
            var result = await _repository.GetHocrByClass(documentName, @class);
            return await _hocrMapper.MapAsync(result);
        }

        public async Task<HocrResponse> GetHocrByTitle(string documentName, string title)
        {
            var result = await _repository.GetHocrByTitle(documentName, title);
            return await _hocrMapper.MapAsync(result);
        }

        public async Task<HocrResponse> GetHocrByClassWithTitle(string documentName, string @class, string title)
        {
            var result = await _repository.GetHocrByTitleWithClass(documentName, title, @class);
            return await _hocrMapper.MapAsync(result);
        }

        public async Task<HocrResponse> GetHocrByTitleWithLang(string documentName, string title, string lang)
        {
            var result = await _repository.GetHocrByTitleWithLang(documentName, title, lang);
            return await _hocrMapper.MapAsync(result);
        }

        public async Task<HocrResponse> GetHocrByClassWithLang(string documentName, string @class, string lang)
        {
            var result = await _repository.GetHocrByClassWithLang(documentName, @class, lang);
            return await _hocrMapper.MapAsync(result);
        }

        public async Task<HocrResponse> GetHocr(string documentName, string @class, string title, string lang)
        {
            var result = await _repository.GetHocr(documentName, title, @class, lang);
            return await _hocrMapper.MapAsync(result);
        }

        public async Task<HocrDocumentResponse> GetDocument(string name)
        {
            var result = await _repository.GetDocumentByName(name);
            return await _hocrMapper.MapAsync(result);
        }
    }
}