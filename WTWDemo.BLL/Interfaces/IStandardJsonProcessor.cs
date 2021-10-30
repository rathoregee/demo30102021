using FluentValidation;
using System.Threading.Tasks;
using Demo.Bll.Enums;
using Demo.BLL.Models;

namespace Demo.Bll.Interfaces
{
    public interface IStandardJsonProcessor
    {
        Task<ServiceResult<StandardReportDto>> ProcessAsync(string rows);
    }
}