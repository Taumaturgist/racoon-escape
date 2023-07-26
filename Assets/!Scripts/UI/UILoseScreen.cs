using UnityEngine;
using TMPro;
using UniRx;

public class UILoseScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI odometerText;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private TextMeshProUGUI salaryText;

    public void Launch()
    {
        MessageBroker
            .Default
            .Receive<OnOdometerUpdateMessage>()
            .Subscribe(message =>
            {
                odometerText.text = $"{message.Odometer} M";
            });

        MessageBroker
            .Default
            .Receive<OnBalanceDiffMessage>()
            .Subscribe(message =>
            {
                salaryText.text = $"+{message.Diff} $";
            });
    }
}
