using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace ClaimDoors
{
    public class DesignatorFog : Designator
    {
        public override int DraggableDimensions => 2;

        public override AcceptanceReport CanDesignateCell(IntVec3 loc) => loc.InBounds(Map) && loc.Fogged(Map) == (mode == DesignateMode.Remove);

        private readonly DesignateMode mode;
        public override bool DragDrawMeasurements => true;
        public override bool Visible => ClaimDoorsSettings.EnableFogTool;

        [UsedImplicitly]
        public DesignatorFog(DesignateMode mode)
        {
            this.mode = mode;
            soundDragSustain = SoundDefOf.Designate_DragStandard;
            soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            useMouseIcon = true;
            soundSucceeded = SoundDefOf.Designate_Claim;
        }

        public override void DesignateSingleCell(IntVec3 cell)
        {
            if (!CanDesignateCell(cell).Accepted) return;
            
            switch (mode)
            {
                case DesignateMode.Add:
                    Fog(cell);
                    break;

                default:
                case DesignateMode.Remove:
                    Map?.fogGrid?.Unfog(cell);
                    break;
            }
        }

        public override void SelectedUpdate()
        {
            GenUI.RenderMouseoverBracket();
        }

        public override void RenderHighlight(System.Collections.Generic.List<IntVec3> dragCells) => DesignatorUtility.RenderHighlightOverSelectableCells(this, dragCells);

        private void Fog(IntVec3 c)
        {
            if (Map?.fogGrid?.fogGrid is null || Map.cellIndices is null) return;
            int index = Map.cellIndices.CellToIndex(c);
            Map.fogGrid.fogGrid[index] = true;
            if (Current.ProgramState == ProgramState.Playing)
            {
                Map.mapDrawer?.MapMeshDirty(c, MapMeshFlag.Things | MapMeshFlag.FogOfWar);
                Map.roofGrid?.Drawer?.SetDirty();
            }
        }
    }
}