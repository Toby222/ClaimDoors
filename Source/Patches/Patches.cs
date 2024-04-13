using System.Linq;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace ClaimDoors.Patches
{
    [HarmonyPatch(typeof(MapGenerator), nameof(MapGenerator.GenerateMap))]
    internal static class UnclaimAllDoorsOnGeneratedMap
    {
        [UsedImplicitly]
        private static void Postfix([CanBeNull] Map __result)
        {
            if (!ClaimDoorsSettings.EnableDoorUnclaiming || __result?.spawnedThings is null)
                return;
            var doors = __result
                .spawnedThings.Where(thing =>
                    thing is Building_Door && thing.Faction != Faction.OfPlayer
                )
                .Cast<Building_Door>();
            foreach (Building_Door door in doors)
                door!.SetFactionDirect(null);
        }
    }
}
