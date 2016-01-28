using FirebaseSharp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnC.Base
{
    public interface IFireDelegate
    {
        FirebaseApp FAppPublic { get; set; }
        FirebaseApp FAppPrivate { get; set; }

        void object_added(IFireObject fireObject);
        void object_deleted(IFireObject fireObject);
        void object_updated(IFireObject fireObject);

    }
}
