using HarmonyLib;
using JetBrains.Annotations;
using Verse;

namespace ClaimDoors
{
    internal class ClaimDoorsMod : Mod
    {
        [UsedImplicitly]
        public ClaimDoorsMod(ModContentPack content) : base(content)
        {
            GetSettings<ClaimDoorsSettings>();
            new Harmony("dev.tobot.claimdoors").PatchAll();
        }

        [UsedImplicitly]
        public override string SettingsCategory() => "Claim Doors";

        [UsedImplicitly]
        public override void DoSettingsWindowContents(UnityEngine.Rect inRect)
        {
            Listing_Standard listingStandard = new();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("Enable Fog Tools", ref ClaimDoorsSettings.EnableFogTool);
            listingStandard.CheckboxLabeled("Un-claim all doors on map generation (allowing anyone to pass through freely)", ref ClaimDoorsSettings.EnableDoorUnclaiming);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
    }

    internal class ClaimDoorsSettings : ModSettings
    {
        public static bool EnableFogTool = true;
        public static bool EnableDoorUnclaiming = true;

        /// <summary>
        /// The part that writes our settings to file. Note that saving is by ref.
        /// </summary>
        public override void ExposeData()
        {
            Scribe_Values.Look(ref EnableFogTool, nameof(EnableFogTool));
            Scribe_Values.Look(ref EnableDoorUnclaiming, nameof(EnableDoorUnclaiming));
            base.ExposeData();
        }
    }
}