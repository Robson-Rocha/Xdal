// ReSharper disable once CheckNamespace
namespace Xdal
{
    /// <summary>
    /// Internal Only. Used for instance seeking. 
    /// </summary>
    public interface IEntityBase
    {

    }

    /// <summary>
    /// Represents the minimum set of members that composes an entity.
    /// </summary>
    public interface IEntity<TKey> : IEntityBase
    {
        /// <summary>
        /// Gets or sets the Id of the entity.
        /// </summary>
        TKey Id { get; set; }
    }

    /// <summary>
    /// Represents the minimum set of members that composes an entity.
    /// </summary>
    public interface IEntity : IEntity<long>
    {
    }

}
