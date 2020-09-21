using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Entities
{
    public interface IEntity<TId>
    {
        TId ID { get; }
    }
}
