using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodigious.Data
{
    public interface IModificationHistory
    {

        DateTime ModifiedDate { get; set; }

        bool IsDirty { get; set; }

    }
}
