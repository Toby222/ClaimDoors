using RimWorld;
using UnityEngine;
using Verse;

namespace ClaimDoors
{
    public class Designator_RemoveFog : Designator_Fog
    {
        public Designator_RemoveFog() : base(DesignateMode.Remove)
        {
            defaultLabel = "Unfog Map";
            defaultDesc = "Remove fog of war from the map.";
            icon = ContentFinder<Texture2D>.Get("UI/Designators/Reveal");
        }
    }
}