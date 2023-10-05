using Newtonsoft.Json;
using System;
using System.Diagnostics;
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

            if (string.IsNullOrEmpty(itemText))
            {
                return Task.FromResult<Creature>(null); // Return null if the itemText is empty or null
            }

            Creature creature = JsonConvert.DeserializeObject<Creature>(itemText);
            return Task.FromResult(creature);
        }

        public bool UpdateItem(Creature Item)
        {
            string itemText = Preferences.Get(Item.Name, "");

            if (string.IsNullOrEmpty(itemText))
            {
                return false; // Exit early if the itemText is empty or null
            }

            Creature creature = JsonConvert.DeserializeObject<Creature>(itemText);
            if (creature == null)
            {
                return false; // Exit if deserialization returns null
            }

            creature.Hunger = Item.Hunger;
            creature.Thirst = Item.Thirst;
            creature.Boredom = Item.Boredom;
            creature.Tired = Item.Tired;
            creature.Sleeping = Item.Sleeping;
            string UpdatedCreatureString = JsonConvert.SerializeObject(creature);
            Preferences.Set(Item.Name, UpdatedCreatureString);
            return true;
        }
    }
}
