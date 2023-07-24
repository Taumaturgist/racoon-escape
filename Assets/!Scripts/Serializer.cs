using UnityEngine;

public class PlayerData
{
    public CarsAssortment CarsAssortment;
    public int Odometer;
    public int Balance;

    public PlayerData(
        CarsAssortment carsAssortment,
        int odometer,
        int balance)
    {
        CarsAssortment = carsAssortment;
        Odometer = odometer;
        Balance = balance;
    }
}

public class Serializer : MonoBehaviour
{
    public void Save(PlayerData playerData)
    {
        var saveString = JsonUtility.ToJson(playerData);
        Debug.Log($"Save data: {saveString}");
        PlayerPrefs.SetString("save", saveString);
    }    

    public PlayerData Load()
    {
        var loadString = PlayerPrefs.GetString("save");
        Debug.Log($"Load data: {loadString}");
        return JsonUtility.FromJson<PlayerData>(loadString);
    }
}
