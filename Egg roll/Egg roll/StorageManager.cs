using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;

namespace Egg_roll
{
    class StorageManager
    {

        IsolatedStorageSettings Storage;

        public StorageManager()
        {
            Storage = IsolatedStorageSettings.ApplicationSettings;
        }
        public void SaveObj(object o, string key)
        {
            Storage[key] = o;
            Storage.Save();
        }
 
        public object LoadObj(string key)
        {
            object o;
            Storage.TryGetValue<object>(key, out o);
            return o;
                     
        }
 
    }
    }

