namespace MyTime.Persistence.Infrastructure;


public interface IEntity<TKey> : IId<TKey>, ITracking
{
}
