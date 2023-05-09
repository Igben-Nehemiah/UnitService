using System;
using System.Threading.Tasks;

namespace UnitService.Core.Extensions
{
    internal static class TaskExtension
    {
        public async static void Await(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}