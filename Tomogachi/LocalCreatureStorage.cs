using System.Diagnostics;
using Newtonsoft.Json;

namespace Tomogachi;

public class LocalCreatureStorage : IDataStore<Creature>
{
    string creatureString;
    public LocalCreatureStorage()
    {
    }
    private HttpClient client = new HttpClient();

    public async Task<bool> CreateItem(Creature Data, string S)
    {
        creatureString = JsonConvert.SerializeObject(Data);
        Creature TestCreature = JsonConvert.DeserializeObject<Creature>(creatureString);

        Preferences.Set("TeestCreatureHardCoded", creatureString);
        creatureString = Preferences.Get("TeestCreatureHardCoded", "");

        if (creatureString != null)
        {
            return true;
        }
        else
        {
            return false;

        }
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
            var Creature = JsonConvert.DeserializeObject<Creature>(creatureString);

            Preferences.Set("CreatureID", creatureString);

            return Creature;

        }
        return null;
    }

    public bool UpdateItem(Creature Item)
    {
        throw new NotImplementedException();
        //ik wil dat de stat na ongeveer 10 minuten vol is, dus door 1/600 e toe te voegen iedere seconde kom ik daar op uit
        Item.Hunger -= 1f / 600f;
        Item.Thirst -= 1 / 600f;
        Item.boredom -= 1 / 600f;
    }
}
