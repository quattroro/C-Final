using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : BaseSlot
{
    public override void Start()
    {
        base.Start();
        slottype = EnumTypes.SlotTypes.Quick;
    }
}
