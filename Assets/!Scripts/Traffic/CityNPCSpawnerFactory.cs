using UnityEngine;

namespace Assets._Scripts.Traffic
{
    public class CityNPCSpawnerFactory : ISpawnerFactory
    {
        public INPC SpawnNPC()
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var npc = go.AddComponent<NPC>();

            return npc;
        }

    }
}
