namespace Infrastructure.Pool
{
    public interface IObjectPool<T> where T : class
    {
        T Get();
        void Release(T entity);
    }
}
