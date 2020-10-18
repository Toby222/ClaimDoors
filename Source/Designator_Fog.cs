using HarmonyLib;
using RimWorld;
using Verse;

namespace ClaimDoors
{
    public class Designator_Fog : Designator
    {
        public override int DraggableDimensions => 2;

        public override AcceptanceReport CanDesignateCell(IntVec3 loc) => loc.InBounds(Map) && loc.Fogged(Map) == (mode == DesignateMode.Remove);

        private readonly DesignateMode mode;
        public override bool DragDrawMeasurements => true;
        public override bool Visible => ClaimDoorsSettings.enableFogTool;

        public Designator_Fog(DesignateMode mode)
        {
            this.mode = mode;
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            useMouseIcon = true;
            soundSucceeded = SoundDefOf.Designate_Claim;
        }

        public override void DesignateSingleCell(IntVec3 cell)
        {
            if (CanDesignateCell(cell).Accepted)
            {
                switch (mode)
                {
                    case DesignateMode.Add:
                        FogWorker(cell);
                        break;

                    case DesignateMode.Remove:
                        Traverse.Create(Map.fogGrid).Method("UnfogWorker", cell).GetValue();
                        break;
                }
            }
        }

        public override void SelectedUpdate()
        {
            GenUI.RenderMouseoverBracket();
        }

        public override void RenderHighlight(System.Collections.Generic.List<IntVec3> dragCells) => DesignatorUtility.RenderHighlightOverSelectableCells(this, dragCells);

        private void FogWorker(IntVec3 c)
        {
            int index = Map.cellIndices.CellToIndex(c);
            Map.fogGrid.fogGrid[index] = true;
            if (Current.ProgramState == ProgramState.Playing)
            {
                Map.mapDrawer.MapMeshDirty(c, MapMeshFlag.Things | MapMeshFlag.FogOfWar);
                Map.roofGrid.Drawer.SetDirty();
            }
        }
    }
}