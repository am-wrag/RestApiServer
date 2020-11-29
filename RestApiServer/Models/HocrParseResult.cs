namespace RestApiServer.Models
{
    public class HocrParseResult
    {
        public string Id { get; }

        public string Class { get; }

        public string Title { get; }

        public string Lang { get; }

        public HocrParseResult(string id, string @class, string title, string lang)
        {
            Id = id;
            Class = @class;
            Title = title;
            Lang = lang;
        }
    }
}