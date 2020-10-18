using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;

namespace ClaimDoors.Patches
{
    [HarmonyPatch(typeof(MapGenerator), nameof(MapGenerator.GenerateMap))]
    internal static class UnclaimAllDoorsOnGeneratedMap
    {
        private static void Postfix(Map __result)
        {
            var doors = __result.spawnedThings.Where(thing => thing is Building_Door && thing.Faction != Faction.OfPlayer).Cast<Building_Door>();
            foreach (var door in doors)
                door.SetFactionDirect(null);
        }
    }
}