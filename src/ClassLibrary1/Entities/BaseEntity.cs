namespace Domain.Entities;

public abstract class BaseEntity<T>
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
