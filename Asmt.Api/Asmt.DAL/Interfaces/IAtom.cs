using System;

namespace Asmt.DAL.Interfaces
{
    /// <summary>
    /// Represents a base interface for atomic entities in the data access layer.
    /// </summary>
    public interface IAtom
    {
        /// <summary>
        /// Gets the unique identifier of the entity.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        DateTime CreateDT { get; set; }

        /// <summary>
        /// Gets the date and time when the entity was last updated.
        /// If null, the entity has never been updated.
        /// </summary>
        DateTime? UpdateDT { get; set; }
    }
}
