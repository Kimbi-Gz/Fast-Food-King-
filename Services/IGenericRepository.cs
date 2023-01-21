namespace FastFoodKing.Services
{
    public interface IGenericRepository<T> where T: class 
    {
        Task<IEnumerable<T>> GetAllSync();
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id); 
    }
}
