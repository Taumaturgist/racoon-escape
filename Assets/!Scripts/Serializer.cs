using System.Collections.Generic;
using UnityEngine;

public class Serializer : MonoBehaviour
{
    private PlayerDataStorage playerDataStorage;
    public void Save(int odometer)
    {
        PlayerPrefs.SetInt("odometer", odometer);
    }

    public Dictionary<string, int> Load()
    {
        var storage = new Dictionary<string, int>();

        int odometer = PlayerPrefs.GetInt("odometer");
        storage.Add("odometer", odometer);

        return storage;
    }
}
