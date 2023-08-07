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

        // I have used this method to get response from the cache.
        // Ideally We can use GetAll to get the list of EmployeePayslipResponse from DB.
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

        // I have used this method to append response to cache.
        // Ideally We can use PostData to store EmployeePayslipModel, calculate response in DB and get response data as result.
        public IEnumerable<EmployeePayslipResponse> SetData(string cacheKey, EmployeePayslipResponse response)
        {
            List<EmployeePayslipResponse> existingData = GetData(cacheKey).ToList();
            existingData.Add(response);
            return MemoryCacheWrapper.Set(cacheKey, existingData);
        }
    }
}
