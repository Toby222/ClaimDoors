using System.Collections.Generic;
using JetBrains.Annotations;
using RimWorld;
using UnityEngine;
using Verse;

namespace ClaimDoors
{
    public class DesignatorFog : Designator_Zone
    {
        public override int DraggableDimensions => 2;

        public override AcceptanceReport CanDesignateCell(IntVec3 loc) =>
            loc.InBounds(Map) && loc.Fogged(Map) == (mode == DesignateMode.Remove);

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
            if (!CanDesignateCell(cell).Accepted)
                return;

            switch (mode)
            {
                case DesignateMode.Add:
                    Map?.fogGrid?.Refog(CellRect.SingleCell(cell));
                    break;

                case DesignateMode.Remove:
                    Map?.fogGrid?.Unfog(cell);
                    break;
            }
        }

        public override void SelectedUpdate()
        {
            GenUI.RenderMouseoverBracket();
        }
    }
}
