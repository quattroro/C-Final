using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EquipmentWindow : Singleton<EquipmentWindow>
{
    public List<BaseSlot> equipslots;




    public void EquipEquipments(BaseNode node, BaseSlot slot)
    {

    }



    private void Awake()
    {
        BaseSlot[] temps = (BaseSlot[])GetComponentsInChildren<EquipSlot>();
        equipslots = temps.ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
