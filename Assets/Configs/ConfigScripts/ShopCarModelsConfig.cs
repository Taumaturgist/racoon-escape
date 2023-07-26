using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopCarModelsConfig", menuName = "Configs/ShopCarModelsConfig", order = 51)]
public class ShopCarModelsConfig : ScriptableObject
{
    public List<PlayerCarShopView> CarsL0;
    public List<PlayerCarShopView> CarsL1;
    public List<PlayerCarShopView> CarsL2;
    public List<PlayerCarShopView> CarsL3;
}
