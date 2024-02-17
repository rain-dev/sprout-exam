namespace Sprout.Exam.Domain.Models
{
    /// <summary>
    /// base entity for repository
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public record BaseEntity<TId>
        where TId : struct
    {
        public virtual TId Id { get; set; }
    }
}
