namespace F1Zone.API.INTERFACE
{
    public interface IGenericF1ZoneService<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }
}
