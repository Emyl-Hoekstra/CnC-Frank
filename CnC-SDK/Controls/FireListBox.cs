using CnC.Base;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CnC.Controls
{
    public class FireListBox : IFireControl
    {
        public FireListBox(ListBox listBox)
        {
            this.ListBox = listBox;
        }

        public ListBox ListBox { get; set; } 
        public void Add(IFireObject item)
        {
            this.ListBox.Items.Add(item);
        }

        public void Clear()
        {
            this.ListBox.Items.Clear();
        }

        public void Update(IEnumerable<IFireObject> items)
        {
            this.Clear();

            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public bool InvokeRequired { get { return ListBox.InvokeRequired; } }
    }
}
