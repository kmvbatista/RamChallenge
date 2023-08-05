public interface IStatusRepository : IGenericRepository<Status>
{
    Status GetStatusByLatestOrder();
}