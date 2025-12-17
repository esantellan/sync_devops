namespace Metrics.Domain.Entities
{
    public abstract class BaseEntity<T>
    {
        public T? Id { get; set; }
        public BaseEntity()
        {
            if (typeof(T) == typeof(Guid))
            {
                Id = (T)(object)Guid.NewGuid();
            }
        }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {

    }
}
