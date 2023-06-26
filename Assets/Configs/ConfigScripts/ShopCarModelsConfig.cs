using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopCarModelsConfig", menuName = "Configs/ShopCarModelsConfig", order = 51)]
public class ShopCarModelsConfig : ScriptableObject
{
    public List<PlayerCarShopView> carsL0;
    public List<PlayerCarShopView> carsL1;
    public List<PlayerCarShopView> carsL2;
    public List<PlayerCarShopView> carsL3;
}
