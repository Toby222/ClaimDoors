using JetBrains.Annotations;
using RimWorld;
using UnityEngine;
using Verse;

namespace ClaimDoors
{
    public class DesignatorAddFog : DesignatorFog
    {
        [UsedImplicitly]
        public DesignatorAddFog()
            : base(DesignateMode.Add)
        {
            defaultLabel = "Fog Map";
            defaultDesc = "Cover the map with fog of war.";
            icon = ContentFinder<Texture2D>.Get("UI/Designators/Hide");
        }
    }
}
