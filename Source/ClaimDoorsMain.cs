using HarmonyLib;
using Verse;

namespace ClaimDoors
{
    internal class ClaimDoorsMod : Mod
    {
        public ClaimDoorsMod(ModContentPack content) : base(content)
        {
            GetSettings<ClaimDoorsSettings>();
            new Harmony("tobs.claimdoors.mod").PatchAll();
        }

        public override string SettingsCategory() => "Claim Doors";

        public override void DoSettingsWindowContents(UnityEngine.Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("Enable Fog Tools", ref ClaimDoorsSettings.enableFogTool);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
    }

    internal class ClaimDoorsSettings : ModSettings
    {
        public static bool enableFogTool = true;

        /// <summary>
        /// The part that writes our settings to file. Note that saving is by ref.
        /// </summary>
        public override void ExposeData()
        {
            Scribe_Values.Look(ref enableFogTool, nameof(enableFogTool));
            base.ExposeData();
        }
    }
}