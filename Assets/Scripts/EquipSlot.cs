using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : BaseSlot
{
    public EnumTypes.EquipmentTypes equiptype;
    public override void Start()
    {
        base.Start();
        slottype = EnumTypes.SlotTypes.Equip;
    }


    void Update()
    {
        
    }
}
