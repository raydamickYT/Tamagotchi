using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace Tomogachi
{
    internal class CreatureDataStore : IDataStore<Creature>
    {
        public string StorageKey = string.Empty;
        public CreatureDataStore(string _storageKey)
        {
            StorageKey = _storageKey;
        }

        public Task<bool> CreateItem(Creature Data)
        {
            if (Preferences.ContainsKey("MyCreature"))
            {
                return Task.FromResult(false);
            }

            string creatureString = JsonConvert.SerializeObject(Data);
            Preferences.Set("MyCreature", creatureString);

            return Task.FromResult(Preferences.ContainsKey("MyCreature"));
        }

        public bool DeleteItem(Creature Item)
        {
            return false;
        }

        public Task<Creature> ReadItem(string id)
        {
            string itemText = Preferences.Get("MyCreature", "");

            Creature creature = JsonConvert.DeserializeObject<Creature>(itemText);
            return Task.FromResult(creature);
        }

        public bool UpdateItem(Creature Item)
        {
            return false;
        }
    }
}
