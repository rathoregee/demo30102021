using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Bll.Enums;
using Demo.BLL.Models;

namespace Demo.Bll.Interfaces
{
    public interface IFileManager
    {
        Task<ServiceResult<string>> ReadFileAsync();
        Task<ServiceResult<bool>> WriteFileAsync(string data);
    }
}