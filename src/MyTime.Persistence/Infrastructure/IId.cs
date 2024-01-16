namespace MyTime.Persistence.Infrastructure;

public interface IId<TKey>
{
  TKey Id { get; set; }
}
