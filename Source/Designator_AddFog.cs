using RimWorld;
using UnityEngine;
using Verse;

namespace ClaimDoors
{
    public class Designator_AddFog : Designator_Fog
    {
        public Designator_AddFog() : base(DesignateMode.Add)
        {
            defaultLabel = "Fog Map";
            defaultDesc = "Cover the map with fog of war.";
            icon = ContentFinder<Texture2D>.Get("UI/Designators/Hide");
        }
    }
}