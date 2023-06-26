using UnityEngine;

public class PlayerData
{
    public int Odometer;
    public int Balance;

    public PlayerData(int odometer, int balance)
    {
        Odometer = odometer;
        Balance = balance;
    }
}

public class Serializer : MonoBehaviour
{
    public void Save(PlayerData playerData)
    {
        var saveString = JsonUtility.ToJson(playerData);
        Debug.Log(saveString);
        PlayerPrefs.SetString("save", saveString);
    }    

    public PlayerData Load()
    {
        return JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("save"));
    }
}
