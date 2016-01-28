using FirebaseSharp.Portable;
using System;

namespace CnC.Base
{
    public class FireObject : IFireObject
    {
        public FireObject()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Revision = Guid.NewGuid().ToString();
            this.Key = "";
        }

        public IFireObject SaveDelegate { get; set; }
        
        public FirebaseApp FireBaseApp { get; set; }
        public string UserId { get; set; }
        public string uid { get; set; }

        public string Id { get; set; }
        public string Path { get; set; }
        /// <summary>
        /// Only to be used locally! use Id for unique object identification
        /// </summary>
        public string Key { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public string Revision { get; set; }

        public override string ToString()
        {
            return this.Id;
        }

        public enObjectState ObjectState { get; set; }

        public IFirebase Save(IFirebaseApp app)
        {
            if (this.Key != null && this.Key != "")
            {
                //remove old version first (not very pretty but Firebase change events are pain in tha butt)
                var entity = app.Child(this.Path + "/" + this.Key);
                entity.Remove();
            }
            else
            {
                this.CreatedAt = DateTime.Now;
            }

            var newEntity = app.Child(this.Path);
            this.UpdatedAt = DateTime.Now;
            this.Revision = Guid.NewGuid().ToString();
            IFirebase result = newEntity.Push(this);
            this.Key = result.Key;
            return result;

        }
        public void Delete(IFirebaseApp app)
        {
            var entity = app.Child(this.Path + "/" + this.Key);
            entity.Remove();
        }

        //public T Get<T>(IFirebaseApp app, string path)
        //{
        //    app.Child(path).Once()
        //    return default(T);

        //}
    }



}
