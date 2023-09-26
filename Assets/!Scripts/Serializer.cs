using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public Dictionary<eCarModel, eCarLevel> CarsAssortmentDict;
    public int Odometer;
    public int Balance;

    public PlayerData(
        Dictionary<eCarModel, eCarLevel> carsAssortmentDict,
        int odometer,
        int balance)
    {
        CarsAssortmentDict = carsAssortmentDict;
        Odometer = odometer;
        Balance = balance;
    }
}

public class Serializer : MonoBehaviour
{
    public void Save(PlayerData playerData)
    {
        var dictString = "";
        foreach (var car in playerData.CarsAssortmentDict)
        {
            dictString += $"{car.Key},{car.Value};";
        }
        PlayerPrefs.SetString("carsDict", dictString);

        PlayerPrefs.SetString("odometer", playerData.Odometer.ToString());
        PlayerPrefs.SetString("balance", playerData.Balance.ToString());

        Debug.Log($"SaveData: {dictString} {playerData.Odometer} {playerData.Balance}");
    }    

    public PlayerData Load()
    {
        var dictString = PlayerPrefs.HasKey("carsDict") ?
            PlayerPrefs.GetString("carsDict") :
            "ChevroletCamaroSS1969,Lvl1;ToyotaTundra,Locked;NissanSkylineGT,Locked;DodgeViperGTS,Locked;MercedesBenzGCLass,Locked;LamborghiniHuracanLP700,Locked;";
        

        Debug.Log($"dictString {dictString}");
        var carsDict = new Dictionary<eCarModel, eCarLevel>();
        
        var carsArray = dictString.Split(";");
        foreach (var car in carsArray)
        {
            var carInfo = car.Split(",");
            if (carInfo[0] != "" && carInfo[1] != "")
            {
                carsDict.Add(Enum.Parse<eCarModel>(carInfo[0]),
                         Enum.Parse<eCarLevel>(carInfo[1]));
            }
        }

        var odometer = PlayerPrefs.HasKey("odometer") ?
            Int32.Parse(PlayerPrefs.GetString("odometer")) : 0;

        var balance = PlayerPrefs.HasKey("balance") ?
            Int32.Parse(PlayerPrefs.GetString("balance")) : 0;

        Debug.Log($"LoadData: {dictString} {odometer} {balance}");
        return new PlayerData(carsDict, odometer, balance);
    }
}