namespace IN2.UserPortal.Persistance.Interfaces
{
    public interface IAsyncRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Delete(T model);
        Task<T> Update(T model);
        Task<T> Insert(T model);


    }
}
