using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Tomogachi
{
    //deze class gaat over het managen van een connection met een online server
    public class RemoteCreatureDataStore() : IDataStore<Creature>
    {
        private HttpClient client = new HttpClient();

        public async Task<bool> CreateItem(Creature Data)
        {
            string creatureString = JsonConvert.SerializeObject(Data);

            //"encoding" is onderdeel van system.text(volledige cmd is system.text.encoding.utf8)
            var response = await client.PostAsync($"https://tamagotchi.hku.nl/api/Creatures/", new StringContent(creatureString, Encoding.UTF8, "application/json"));
            var response2 = await client.GetAsync("https://tamagotchi.hku.nl/api/Creatures");
            if(!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Error: {errorMessage}");
                Debug.WriteLine(response.IsSuccessStatusCode);
            }
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                var responseCreature = JsonConvert.DeserializeObject<Creature>(responseString);

                Preferences.Set("CreatureID", responseCreature.Id);


                return true;

            }
            return false;
        }

        public bool DeleteItem(Creature Item)
        {
            throw new NotImplementedException();
        }

        public async Task<Creature> ReadItem(string id)
        {
            var response = await client.GetAsync($"https://tamagotchi.hku.nl/api/Creatures/{id}");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                var Creature = JsonConvert.DeserializeObject<Creature>(responseString);

                Preferences.Set("CreatureID", Creature.Id);

                return Creature;

            }
            return null;
        }

        public bool UpdateItem(Creature Item)
        {
            throw new NotImplementedException();
        }
    }
}
