using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    private float _highScore;

    private void Awake()
    {
        //get Odometer from CarDashBoard
        //_highScore = Odometer;
    }
    private void Start()
    {
        highScoreText.text = $"HighScore: {_highScore}";
    }
}
