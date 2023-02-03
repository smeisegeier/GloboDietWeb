namespace GloboDiet.Services
{
    /// <summary>
    /// Must be inherited by entity classes
    /// </summary>
    public interface IEntity
    {
        public int Id { get; set; }
    }
}
