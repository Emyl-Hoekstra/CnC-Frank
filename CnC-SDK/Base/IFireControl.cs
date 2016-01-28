
using System.Collections.Generic;
namespace CnC.Base
{
    public interface IFireControl
    {
        bool InvokeRequired { get; }
        void Add(IFireObject item);
        void Clear();
        void Update(IEnumerable<IFireObject> items);

    }
}
