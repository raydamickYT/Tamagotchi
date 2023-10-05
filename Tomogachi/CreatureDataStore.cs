using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;

namespace Tomogachi
{
    internal class CreatureDataStore : IDataStore<Creature>
    {
        public CreatureDataStore()
        {
        }

        public Task<bool> CreateItem(Creature Data, string StorageKey)
        {
            if (Preferences.ContainsKey(StorageKey))
            {
                return Task.FromResult(false);
            }

            string creatureString = JsonConvert.SerializeObject(Data);
            Preferences.Set(StorageKey, creatureString);

            return Task.FromResult(Preferences.ContainsKey(StorageKey));
        }

        public bool DeleteItem(Creature Item)
        {
            bool keyExists = Preferences.ContainsKey(Item.Name);
            if (keyExists)
            {
                Preferences.Remove(Item.Name);
                return true;
            }
            return false;
        }

        public Task<Creature> ReadItem(string id)
        {
            string itemText = Preferences.Get(id, "");

            Creature creature = JsonConvert.DeserializeObject<Creature>(itemText);
            return Task.FromResult(creature);
        }

        public bool UpdateItem(Creature Item, bool IsSleeping)
        {
            string itemText = Preferences.Get(Item.Name, "");

            if (itemText != null)
            {
                Creature creature = JsonConvert.DeserializeObject<Creature>(itemText);
                creature.Hunger = Item.Hunger;
                creature.Thirst = Item.Thirst;
                creature.boredom = Item.boredom;
                creature.tired = Item.tired;
                string UpdatedCreatureString = JsonConvert.SerializeObject(creature);
                Preferences.Set(Item.Name, UpdatedCreatureString);
                return true;
            }
            return false;
        }
    }
}
