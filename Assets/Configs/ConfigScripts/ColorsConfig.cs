using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorsConfig", menuName = "Configs/ColorsConfig", order = 51)]
public class ColorsConfig : ScriptableObject
{
    public SpecificCarColors[] CarColors;

    [Serializable]
    public struct SpecificCarColors
    {
        public eCarModel CarModel;        
        public ColorAndPrice[] ColorChoice;
    }

    [Serializable]
    public struct ColorAndPrice
    {
        public Color CarColor;
        public int ColorPrice;
    }
}
