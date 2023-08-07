using System;
using EmployeePayslip.Models;
using EmployeePayslip.Utilities;

namespace EmployeePayslip.Data
{
    public interface IEmployeePayslipManager
    {
        IEnumerable<EmployeePayslipResponse> GetData(string cacheKey);
        IEnumerable<EmployeePayslipResponse> SetData(string cacheKey, EmployeePayslipResponse data);
    }

    public class EmployeePayslipManager : IEmployeePayslipManager
    {
        public IMemoryCacheWrapper MemoryCacheWrapper { get; }
        public EmployeePayslipManager(IMemoryCacheWrapper memoryCacheWrapper)
        {
            MemoryCacheWrapper = memoryCacheWrapper;
        }

        public IEnumerable<EmployeePayslipResponse> GetData(string cacheKey)
        {
            if (MemoryCacheWrapper.TryGetValue(cacheKey, out List<EmployeePayslipResponse>? data))
            {
                return data;
            }
            else
            {
                return new List<EmployeePayslipResponse>();
            }
        }

        public IEnumerable<EmployeePayslipResponse> SetData(string cacheKey, EmployeePayslipResponse response)
        {
            var existingData = GetData(cacheKey).ToList();
            existingData.Add(response);
            return MemoryCacheWrapper.Set(cacheKey, existingData);
        }
    }
}
