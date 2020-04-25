using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;

namespace ClaimDoors
{
    [StaticConstructorOnStartup]
    internal static class ClaimDoorsMain
    {
        static ClaimDoorsMain()
        {
            new Harmony("tobs.claimdoors.mod").PatchAll();
        }

        [HarmonyPatch(typeof(MapGenerator), nameof(MapGenerator.GenerateMap))]
        internal static class MapParent_PostMapGenerate
        {
            private static void Postfix(Map __result)
            {
                var doors = __result.spawnedThings.Where(thing => thing is Building_Door && thing.Faction != Faction.OfPlayer);
                foreach (var door in doors)
                    door.SetFactionDirect(null);
            }
        }
    }
}
