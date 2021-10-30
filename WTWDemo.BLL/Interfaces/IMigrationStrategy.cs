using System.Threading.Tasks;
using Demo.Bll.Enums;

namespace Demo.Bll.Interfaces
{
    public interface IMigrationStrategy
    {
        string Name { get; }
        int DuplicateCount { get; }
        int ErrorCount { get; }
        int ValidCount { get; }
        void SetPayload(string rows);
        Task<ServiceResult<string>> MigrateAsync();
        string GetDataLog();
    }
}