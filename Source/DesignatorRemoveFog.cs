using JetBrains.Annotations;
using RimWorld;
using UnityEngine;
using Verse;

namespace ClaimDoors
{
    public class DesignatorRemoveFog : DesignatorFog
    {
        [UsedImplicitly]
        public DesignatorRemoveFog() : base(DesignateMode.Remove)
        {
            defaultLabel = "Unfog Map";
            defaultDesc = "Remove fog of war from the map.";
            icon = ContentFinder<Texture2D>.Get("UI/Designators/Reveal");
        }
    }
}