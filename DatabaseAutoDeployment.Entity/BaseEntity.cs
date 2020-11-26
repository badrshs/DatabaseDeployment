using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAutoDeployment.Entity
{
    /// <summary>
    /// Base Entity
    /// </summary>
    /// <seealso cref="DatabaseAutoDeployment.Entity.IBaseEntity" />
    public class BaseEntity: IBaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public bool IsDeleted { get; set; }
    }
}
