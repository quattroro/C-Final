using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EquipmentWindow : Singleton<EquipmentWindow>
{
    public List<BaseSlot> equipslots;



    //��� ���Կ� ��� �����Ѵ�.
    public bool EquipEquipments(BaseNode node, BaseSlot slot)
    {
        //������ ������ ��� �ƴϸ� ��� ����
        if (node.GetItemTypes() != EnumTypes.ItemTypes.Equips)
            return false;


        EquipSlot eslot = slot as EquipSlot;


        //����� ������ ���������� �������� Ȯ���ϰ����� �����ϸ� ����ִ´�.
        if(node.GetEquipTypes()==eslot.equiptype)
        {
            slot.SetNode(node);
        }
        else
        {
            //���� Ǯ������ �����̸� ���� �ִ� �ڸ��� �ǵ��ư���.
            //if (node.PreSlot.GetSlotTypes() != EnumTypes.SlotTypes.Equip)
            //    ItemBag.Instance.SetItem(node, node.PreSlot);
            //else
            //    EquipEquipments(node, node.PreSlot);
            return false;
        }
        return true;
    }

    //��� �����ϸ� 
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
