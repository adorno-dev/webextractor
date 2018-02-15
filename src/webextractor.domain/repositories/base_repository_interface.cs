using System.Collections.Generic;

namespace WebExtractor.Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        (int totalRecords, IList<T> data) All();
        (int totalRecords, int totalPages, IList<T> data) All(int page, int pageSize);
    }
}