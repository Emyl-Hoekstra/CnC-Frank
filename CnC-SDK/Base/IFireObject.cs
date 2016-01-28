using FirebaseSharp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnC.Base
{
    public interface IFireObject
    {
        string Id { get; set; }
        /// <summary>
        /// Only to be used locally! use Id for unique object identification
        /// </summary>
        string Path { get; set; }
        string Key { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }

        string Revision { get; set; } // not transactional safe, but at least you can see if record is different then cached

        string ToString();

        enObjectState ObjectState { get; set; }
        IFirebase Save(IFirebaseApp app);
        void Delete(IFirebaseApp app);


    }
}
