using System.Threading.Tasks;
using Infraestructure.Context;
using System.Linq;

public class StatusRepository : GenericRepository<Status>, IStatusRepository
{
    public StatusRepository(MainContext dbContext) : base(dbContext)
    {

    }
    public Status GetStatusByLatestOrder()
    {
        var result = _dbContext.Set<Status>().OrderByDescending(s => s.Order).First();
        return result;
    }
}