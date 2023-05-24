namespace Lesson36.Dao;

public record EntityWithId
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
}