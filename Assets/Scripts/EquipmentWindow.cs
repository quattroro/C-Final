using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EquipmentWindow : Singleton<EquipmentWindow>
{
    public List<BaseSlot> equipslots;



    //장비 슬롯에 장비를 장착한다.
    public bool EquipEquipments(BaseNode node, BaseSlot slot)
    {
        //아이템 유형이 장비가 아니면 등록 실패
        if (node.GetItemTypes() != EnumTypes.ItemTypes.Equips)
            return false;


        EquipSlot eslot = slot as EquipSlot;


        //장비의 파츠가 장착가능한 파츠인지 확인하고장착 가능하면 집어넣는다.
        if(node.GetEquipTypes()==eslot.equiptype)
        {
            slot.SetNode(node);
        }
        else
        {
            //장착 풀가능한 파츠이면 원래 있던 자리로 되돌아간다.
            //if (node.PreSlot.GetSlotTypes() != EnumTypes.SlotTypes.Equip)
            //    ItemBag.Instance.SetItem(node, node.PreSlot);
            //else
            //    EquipEquipments(node, node.PreSlot);
            return false;
        }
        return true;
    }

    //장비를 장착하면 
    public void EquipEvent()
    {

    }

    public void UnEquipEvent()
    {

    }


    private void Awake()
    {
        BaseSlot[] temps = (BaseSlot[])GetComponentsInChildren<EquipSlot>();
        equipslots = temps.ToList();

        for(int i=0;i<equipslots.Count; i++)
        {
            equipslots[i].InsertEvent(EquipEvent);
            equipslots[i].PickUpEvent(UnEquipEvent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
