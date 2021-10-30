using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Bll.Classes;

namespace Demo.Bll.Interfaces
{
    public interface IDataMigration
    {
        string Name { get; set; }
        Task ProcessAsync(IMigrationStrategy calculationStrategy);
    }
}