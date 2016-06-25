using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodigious.Data.Interfaces
{
    public interface IModificationHistory
    {

        DateTime ModifiedDate { get; set; }

        bool IsDirty { get; set; }

    }
}
