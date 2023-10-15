using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetsServer.Authorization.Model
{
    public enum Restrictions
    {
        All, 
        Organizations,
        Locality,
        None
    }

    public enum Possibilities
    {
        Insert,
        Update,
        Delete
    }
}
