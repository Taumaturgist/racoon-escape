using UnityEngine;
using TMPro;
using UniRx;

public class UIHUD : UIScreen
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private int _balance;
    
    public void Launch()
    {
        MessageBroker
            .Default
            .Receive<OnDataLoadedMessage>()
            .Subscribe(message =>
            {
                _balance = message.PlayerData.Balance;
                moneyText.text = $"$: {_balance}";
            });

        MessageBroker
            .Default
            .Receive<OnBalanceDiffMessage>()
            .Subscribe(message =>
            {
                _balance += message.Diff;
                moneyText.text = $"$: {_balance}";
            });
    }
}
