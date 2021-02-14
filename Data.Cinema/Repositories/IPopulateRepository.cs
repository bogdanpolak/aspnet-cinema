using System;
using System.Threading.Tasks;

namespace Data.Cinema.Repositories
{
    public interface IPopulateRepository
    {
        Task ClearDatabase();
    }
}
