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

    public class FireList
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
        private string UserIDKey = USERIDKEY;
        private FirebaseApp FireBaseApp = null;

        public FireList(FirebaseApp firebaseApp, Type type, IFireDelegate fireDelegate, IFireControl uiSubscriber, string userId = null, bool initWithCache = false, string userIDKey = USERIDKEY)
        {
            List<IFireControl> controlList = new List<IFireControl>();
            controlList.Add(uiSubscriber);
            InitDictionary(firebaseApp, type, fireDelegate, controlList, userId, initWithCache, userIDKey);
        }

        public FireList(FirebaseApp firebaseApp, Type type, IFireDelegate fireDelegate, List<IFireControl> uiSubscribers = null, string userId = null, bool initWithCache = false, string userIDKey = USERIDKEY)
        {
            InitDictionary(firebaseApp, type, fireDelegate, uiSubscribers, userId, initWithCache, userIDKey);
        }

        private void InitDictionary(FirebaseApp firebaseApp, Type type, IFireDelegate fireDelegate, List<IFireControl> uiSubscribers, string userId, bool initWithCache = false, string userIDKey = USERIDKEY)
        {
            this.LastKey = "";
            this.FireDictDelegate = fireDelegate;
            this.Items = new List<IFireObject>();
            this.Path = type.Name;
            this.UISubscriptionList = uiSubscribers;
            this.ObjectType = type;
            this.UserId = userId;
            this.UserIDKey = userIDKey;
            this.FireBaseApp = firebaseApp;
            //   this.FBApp = new FirebaseApp(); //continue on

            if (initWithCache)
            {
                this.LoadFromCache<CnC.Model.Game>();
            }

            if (this.UserId != null) // UserId = "" = public
            {
                //filter on user //.StartAtKey(lastKey)

                myQuery = this.FireBaseApp.Child(this.Path).OrderByChild(this.UserIDKey).EqualTo(userId).On(FIREEVENTADD, (snap, previous_child, context) => AddOrUpdate(snap));
                this.FireBaseApp.Child(this.Path).OrderByChild(this.UserIDKey).EqualTo(userId).On(FIREEVENTDELETE, (snap, previous_child, context) => Removed(snap, previous_child, context));
            }
            else
            {
                //unfiltered
                myQuery = this.FireBaseApp.Child(this.Path).StartAtKey(lastKey).On(FIREEVENTADD, (snap, previous_child, context) => AddOrUpdate(snap));
                this.FireBaseApp.Child(this.Path).On(FIREEVENTDELETE, (snap, previous_child, context) => Removed(snap, previous_child, context));
            }
        }

        public void DisposeQuery()
        {
            this.myQuery.Off();
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
                    myQuery = this.FireBaseApp.Child(this.Path).OrderByChild(this.UserIDKey).EqualTo(this.UserId).StartAtKey(lastKey).On(FIREEVENTADD, (snap2, previous_child, context) => AddOrUpdate(snap2));
                }
                else
                {
                    myQuery = this.FireBaseApp.Child(this.Path).StartAtKey(lastKey).On(FIREEVENTADD, (snap2, previous_child, context) => AddOrUpdate(snap2));
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

        public List<IFireObject> Items { get; set; }

        public List<T> TypedItems<T>()
        {
            return this.Items.Cast<T>().ToList();
        }

        public IFireDelegate FireDictDelegate { get; set; }
        public FirebaseApp FBApp { get; set; }

        public string LastKey { get; set; }

        public void addOrUpdate(IFireObject fireObject, bool deleted = false)
        {

            if (this.Items.Count > 0 && this.Items.Where(i => i.Id == fireObject.Id).Count() > 0)
            {
                //exists in cache
                if (deleted)
                {
                    //deleted
                    this.Items.Remove(this.Items.Where(i => i.Id == fireObject.Id).First());

                    fireObject.ObjectState = enObjectState.deleted;
                }
                else
                {
                    if (this.Items.Where(i => i.Id == fireObject.Id).First().Revision == fireObject.Revision)
                    {
                        //unchanged
                        fireObject.ObjectState = enObjectState.unchanged;
                    }
                    else
                    {
                        //changed
                        fireObject.ObjectState = enObjectState.updated;
                        int index = this.Items.FindLastIndex(i => i.Id == fireObject.Id);
                        if (index != -1)
                        {
                            this.Items[index] = fireObject;
                        }
                    }
                }
            }
            else
            {
                //new
                this.Items.Add(fireObject);
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
                    control.Update(this.Items);
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
