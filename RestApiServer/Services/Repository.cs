using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RestApiServer.Interfaces;
using RestApiServer.Models;

namespace RestApiServer.Services
{
    public class Repository : DbContext, IRepository
    {
        private readonly ServerSettings _serverSettings;
        public DbSet<HocrDocumentDto> HocrDocuments { get; set; }
        public DbSet<HocrDto> Hocr { get; set; }

        public Repository(ServerSettings serverSettings)
        {
            _serverSettings = serverSettings;
        }

        public Task SaveDocument(HocrDocumentDto hocrDocument)
        {
            return Task.Run(() =>
            {
                HocrDocuments.Add(hocrDocument);
                SaveChanges();
            });
        }

        public Task<IEnumerable<HocrDocumentDto>> GetDocumentByName(string name)
        {
            return Task.Run(() => (IEnumerable<HocrDocumentDto>)HocrDocuments.Where(d => d.DocumentName == name));
        }

        public Task<IEnumerable<HocrDto>> GetHocrById(string Id)
        {
            return Task.Run(() => (IEnumerable<HocrDto>)Hocr.Where(d => d.Id == Id));
        }

        public Task<IEnumerable<HocrDto>> GetHocrByTitle(string documentName, string title)
        {
            return Task.Run(() =>
            {
                return (IEnumerable<HocrDto>) Hocr.Where(d => d.Title == title && d.HocrDocument.DocumentName == documentName);
            });
        }

        public Task<IEnumerable<HocrDto>> GetHocrByClass(string documentName, string @class)
        {
            return Task.Run(() =>
            {
                return (IEnumerable<HocrDto>)Hocr.Where(d => d.Class == @class && d.HocrDocument.DocumentName == documentName);
            });
        }

        public Task<IEnumerable<HocrDto>> GetHocrByClassWithLang(string documentName, string @class, string lang)
        {
            return Task.Run(() =>
            {
                return (IEnumerable<HocrDto>)Hocr.Where(
                    d => d.Class == @class
                         && d.Lang == lang
                         && d.HocrDocument.DocumentName == documentName);
            });
        }

        public Task<IEnumerable<HocrDto>> GetHocrByTitleWithClass(string documentName, string title, string @class)
        {
            return Task.Run(() =>
            {
                return (IEnumerable<HocrDto>)Hocr.Where(
                    d => d.Title == title 
                         && d.Class == @class
                         && d.HocrDocument.DocumentName == documentName);
            });
        }

        public Task<IEnumerable<HocrDto>> GetHocrByTitleWithLang(string documentName, string title, string lang)
        {
            return Task.Run(() =>
            {
                return (IEnumerable<HocrDto>)Hocr.Where(
                    d => d.Title == title
                         && d.Lang == lang
                         && d.HocrDocument.DocumentName == documentName);
            });
        }

        public Task<IEnumerable<HocrDto>> GetHocr(string documentName, string title, string @class, string lang)
        {
            return Task.Run(() =>
            {
                return (IEnumerable<HocrDto>)Hocr.Where(
                    d => d.Title == title
                         && d.Class == @class
                         && d.Lang == lang
                         && d.HocrDocument.DocumentName == documentName);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = _serverSettings.SqliteDbName };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HocrDto>()
                .HasOne(c => c.HocrDocument)
                .WithMany(d => d.Hocrs);

            base.OnModelCreating(modelBuilder);
        }

        public Task CreateDbIfNotExist()
        {
            return Database.EnsureCreatedAsync();
        }
    }
}