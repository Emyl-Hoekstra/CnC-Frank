using FirebaseSharp.Portable;
using FirebaseSharp.Portable.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CnC.Base
{


    public class FireDictionary
    {
        private string lastKey = "";
        private IFirebaseReadonlyQuery myQuery;
        private string Path = "";
        private List<IFireControl> UISubscriptionList = null;
        private Type ObjectType;
        private string UserId = null;
        private const string USERIDKEY = "UserId";
        private const string FIREEVENTADD = "value";
        private const string FIREEVENTDELETE = "child_removed";

        public FireDictionary(Type type, IFireDelegate fireDelegate, IFireControl uiSubscriber, string userId = null, bool initWithCache = false)
        {
            List<IFireControl> controlList = new List<IFireControl>();
            controlList.Add(uiSubscriber);
            InitDictionary(type, fireDelegate, controlList, userId, initWithCache);
        }

        public FireDictionary(Type type, IFireDelegate fireDelegate, List<IFireControl> uiSubscribers = null, string userId = null, bool initWithCache = false)
        {
            InitDictionary(type, fireDelegate, uiSubscribers, userId, initWithCache);
        }

        private void InitDictionary(Type type, IFireDelegate fireDelegate, List<IFireControl> uiSubscribers, string userId, bool initWithCache = false)
        {
            this.LastKey = "";
            this.FireDictDelegate = fireDelegate;
            this.Items = new Dictionary<string, IFireObject>();
            this.Path = type.Name;
            this.UISubscriptionList = uiSubscribers;
            this.ObjectType = type;
            this.UserId = userId;

            if(initWithCache)
            {
                this.LoadFromCache<CnC.Model.Game>();
            }

            if (this.UserId != null) // UserId = "" = public
            {
                //filter on user //.StartAtKey(lastKey)
                myQuery = fireDelegate.firebaseApp.Child(this.Path).OrderByChild(USERIDKEY).EqualTo(userId).On(FIREEVENTADD, (snap, previous_child, context) => AddOrUpdate(snap));
                fireDelegate.firebaseApp.Child(this.Path).OrderByChild(USERIDKEY).EqualTo(userId).On(FIREEVENTDELETE, (snap, previous_child, context) => Removed(snap, previous_child, context));
            }
            else
            {
                //unfiltered
                myQuery = fireDelegate.firebaseApp.Child(this.Path).StartAtKey(lastKey).On(FIREEVENTADD, (snap, previous_child, context) => AddOrUpdate(snap));
                fireDelegate.firebaseApp.Child(this.Path).On(FIREEVENTDELETE, (snap, previous_child, context) => Removed(snap, previous_child, context));
            }
        }

        private void AddOrUpdate(IDataSnapshot snap)
        {
            Debug.Print(snap.Key);
            string previousKey = this.LastKey;
            foreach (IDataSnapshot child in snap.Children)
            {
                IFireObject fireObjectInstance = (IFireObject)JsonConvert.DeserializeObject(child.Value(), this.ObjectType);
                fireObjectInstance.Key = child.Key;
                this.addOrUpdate(fireObjectInstance);
            }
          
            if (previousKey != this.LastKey)
            {
                myQuery.Off();

                if (this.UserId != null)
                {
                    //filter on user //.StartAtKey(lastKey)
                    myQuery = FireDictDelegate.firebaseApp.Child(this.Path).OrderByChild(USERIDKEY).EqualTo(this.UserId).StartAtKey(lastKey).On(FIREEVENTADD, (snap2, previous_child, context) => AddOrUpdate(snap2));
                }
                else
                {
                    myQuery = FireDictDelegate.firebaseApp.Child(this.Path).StartAtKey(lastKey).On(FIREEVENTADD, (snap2, previous_child, context) => AddOrUpdate(snap2));
                }
            }
        }

        private void Removed(IDataSnapshot snap, string previous_child, object context)
        {
            Debug.Print(snap.Key);
            IFireObject fireObjectInstance = (IFireObject)JsonConvert.DeserializeObject(snap.Value(), this.ObjectType);

            fireObjectInstance.Key = snap.Key;
            this.remove(fireObjectInstance);
        }

        public Dictionary<string, IFireObject> Items { get; set; }

        public IFireDelegate FireDictDelegate { get; set; }

        public string LastKey { get; set; }

        public void addOrUpdate(IFireObject fireObject, bool deleted = false)
        {
            if (this.Items.Keys.Count > 0 && this.Items.Keys.Contains(fireObject.Id))
            {
                //exists in cache
                if (deleted)
                {
                    //deleted
                    this.Items.Remove(fireObject.Id);
                    fireObject.ObjectState = enObjectState.deleted;
                }
                else
                {
                    if (this.Items[fireObject.Id].Revision == fireObject.Revision)
                    {
                        //unchanged
                        fireObject.ObjectState = enObjectState.unchanged;
                    }
                    else
                    {
                        //changed
                        fireObject.ObjectState = enObjectState.updated;
                        this.Items[fireObject.Id] = fireObject;
                    }
                }
            }
            else
            {
                //new
                this.Items.Add(fireObject.Id, fireObject);
                this.LastKey = fireObject.Key; // for listener optimization 
                fireObject.ObjectState = enObjectState.added;
            }

            //send notifications
            switch (fireObject.ObjectState)
            {
                case enObjectState.unchanged:
                    //nothing required
                    break;
                case enObjectState.updated:
                    this.FireDictDelegate.object_updated(fireObject);
                    if (this.UISubscriptionList != null) this.updateDelegates(fireObject);
                    break;
                case enObjectState.added:
                    this.FireDictDelegate.object_added(fireObject);
                    if (this.UISubscriptionList != null) this.updateDelegates(fireObject);
                    break;
                case enObjectState.deleted:
                    this.FireDictDelegate.object_deleted(fireObject);
                    if (this.UISubscriptionList != null) this.updateDelegates(fireObject);
                    break;
                default:
                    break;
            }
        }

        public void remove(IFireObject fireObject)
        {
            this.addOrUpdate(fireObject, true);
        }

        delegate void SetListCrudCallback(IFireObject listItem);

        public void updateDelegates(IFireObject fireObject)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            foreach (IFireControl control in this.UISubscriptionList)
            {
                if (control == null) return;

                if (control.InvokeRequired)
                {
                    SetListCrudCallback d = new SetListCrudCallback(updateDelegates);
                    ((Form)this.FireDictDelegate).Invoke(d, new object[] { fireObject });
                }
                else
                {
                    control.Update((this.Items.Select(x => x.Value)));
                }
            }
        }

        public void SaveToCache()
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + this.Path + ".json", JsonConvert.SerializeObject(this.Items));
        }
        public void LoadFromCache<T>()
        {
            throw new Exception("not implemented yet");
            //string sFile = AppDomain.CurrentDomain.BaseDirectory + this.Path + ".json";
            //if (File.Exists(sFile))
            //{
            //    string json = File.ReadAllText(sFile);
            //    Dictionary<string, T> mydic = JsonConvert.DeserializeObject<Dictionary<string, T>>(File.ReadAllText(sFile));
            //    //this.Items = (Dictionary<string, IFireObject>) mydic;
            //    foreach (var item in mydic)
            //    {
            //       // IFireObject myObject = (IFireObject)item;
            //        this.Items.Add(myObject.Id, myObject);
            //    }
            //    this.updateDelegates(null);
            //}
            //else
            //{
            //    Debug.Print("Cache not found");
            //}          
        }

    }
}
