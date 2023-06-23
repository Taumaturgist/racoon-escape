using System.Collections.Generic;
using UnityEngine;

public readonly struct PlayerData
{
    public readonly int Odometer;
    public readonly int Balance;

    public PlayerData(int odometer, int balance)
    {
        Odometer = odometer;
        Balance = balance;
    }
}

public class Serializer : MonoBehaviour
{
    
    private PlayerDataStorage playerDataStorage;
    public void Save(PlayerData playerData)
    {
        PlayerPrefs.SetInt("odometer", playerData.Odometer);
        PlayerPrefs.SetInt("balance", playerData.Balance);
    }    

    public PlayerData Load()
    {
        var playerData = new PlayerData(
            PlayerPrefs.GetInt("odometer"),
            PlayerPrefs.GetInt("balance"));

        return playerData;
    }
}
