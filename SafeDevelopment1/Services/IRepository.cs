namespace CardService.Services
{
    public interface IRepository<T, TId>
    {
        IList<T> GetAll();

        T GetById(TId id);

        TId Create(T data);

        int Update(T data);

        int Delete(TId id);    
    }
}
