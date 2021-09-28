namespace LoginStat.DAL.Entities.Abstract
{
    public abstract class BaseEntity<T> where T : struct
    {
        public T Id { get; set; }
    }
}