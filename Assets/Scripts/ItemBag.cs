using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���Ŀ� ������ ����Ͽ�
//������ ������ �������̺�� ���������� �����Ѵ�.
public class ItemBag : Singleton<ItemBag>
{
    [SerializeField]
    private Vector2Int InventorySlotSize = new Vector2Int(10, 4);
    public Vector2 InveltorySlotInterval;

    [SerializeField]
    private int QuickInvectoryNum = 8;
    public Vector2 QuickSlotInterval;

    //public BaseSlot EquipSlot;
    public BaseSlot InventorySlot;
    public BaseSlot QuickSlot;

    
    public BaseSlot[] InventorySlotArr;
    public BaseSlot[] QuickSlotArr;

    private void Awake()
    {
        InventorySlotInit();
    }

    //�ʱ�ȭ �۾� �����丮 ���Ե��� ����� �ش�.
    public void InventorySlotInit()
    {
        BaseSlot temp;
        InventorySlotArr = new BaseSlot[InventorySlotSize.x * InventorySlotSize.y];
        InventorySlotArr[0] = InventorySlot;
        for (int y = 0; y < InventorySlotSize.y; y++)
        {
            for (int x = 0; x < InventorySlotSize.x; x++)
            {
                if (x != 0 || y != 0)
                {
                    temp = GameObject.Instantiate<BaseSlot>(InventorySlot);
                    temp.transform.parent = InventorySlot.transform.parent;
                    temp.transform.position = InventorySlot.transform.position + new Vector3(x * InveltorySlotInterval.x, -y * InveltorySlotInterval.y);
                    temp.SlotIndex = new Vector2Int(x, y);
                    InventorySlotArr[x + (y * InventorySlotSize.x)] = temp;

                }
            }
        }
        QuickSlotArr = new BaseSlot[QuickInvectoryNum];
        QuickSlotArr[0] = QuickSlot;
        for (int x = 1; x < QuickInvectoryNum; x++)
        {
            temp = GameObject.Instantiate<BaseSlot>(QuickSlot);
            temp.transform.parent = QuickSlot.transform.parent;
            temp.transform.position = QuickSlot.transform.position + new Vector3(x * QuickSlotInterval.x, 0);
            temp.SlotIndex = new Vector2Int(x, 0);
            
            QuickSlotArr[x] = temp;
        }

    }

    //�ش� ��� �ı�
    public void DeleteItem(BaseSlot slot, BaseNode node)
    {
        slot.RemoveNode(node);
    }

    //�̸� ������� ��� ��ü�� �Ѱܹ޾Ƽ� ����
    public void InsertItem(BaseSlot slot, BaseNode node)
    {
        if(slot.SettingNode==null)
        {
            slot.SetNode(node);
        }
    }

    //�������� ������ ���濡 �־��ش�.
    public void InsertItem(BaseNode node)
    {
        Vector2Int itemsize;
        bool flag = false;
        //���� �������� ������ ������ �������̸� �����԰� �κ��丮 â���� ���� ������ �������� �ִ��� ã�� ���� �������� ������ ������ �������� �ش�.

        if(node.IsStackAble())
        {
            for (int x = 0; x < QuickInvectoryNum; x++)
            {
                if (QuickSlotArr[x + 0].GetSettingNodeID() == node.GetItemID())
                {
                    QuickSlotArr[x + 0].SettingNode.ChangeStack(node.GetStack());
                    GameObject.Destroy(node.gameObject);
                    return;
                }

            }

            for (int y = 0; y < InventorySlotSize.y; y++)
            {
                for (int x = 0; x < InventorySlotSize.x; x++)
                {
                    if (InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode != null)
                    {

                        if (InventorySlotArr[x + y * (InventorySlotSize.x)].GetSettingNodeID() == node.GetItemID())
                        {
                            InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode.ChangeStack(node.GetStack());
                            GameObject.Destroy(node.gameObject);
                            return;
                        }
                    }
                }
            }
        }

        //������ �Ұ����� �������̸� �κ��丮 â���� �ش� �������� ũ�⸸ŭ ������ ����ִ��� Ȯ���ϰ� ��������� �κ��丮�� �������� �־��ְ� �ƴϸ� �׳� �����ش�.
        for (int y = 0; y < InventorySlotSize.y; y++)
        {
            for (int x = 0; x < InventorySlotSize.x; x++)
            {
                //������ ã�ٰ� ����ִ� ������ ������ 
                if (InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode == null)
                {
                    //�װ��������� �ش� �������� ũ�⸸ŭ ����ִ��� Ȯ���Ѵ�.
                    itemsize = node.GetSize();
                    flag = false;
                    if (x + itemsize.x < InventorySlotSize.x && y + itemsize.y < InventorySlotSize.y)
                    {
                        for (int i = 0; i < itemsize.y; i++)
                        {
                            for (int j = 0; j < itemsize.x; j++)
                            {
                                if (InventorySlotArr[(x + j) + (y + i) * (InventorySlotSize.x)].SettingNode != null)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if(flag)
                            {
                                break;
                            }
                        }
                        //�������� ũ�⸸ŭ ������ ��������� �ش� ������ ���� �� ù��° ���Կ� �������� �־��ش�.
                        if(!flag)
                        {
                            SetItem(node, new Vector2Int(x, y));
                            //InventorySlotArr[x + y * (InventorySlotSize.x)].SetNode(node, node.GetSize());
                            return;
                        }
                    }
                }
            }
        }

    }

    //�������� ũ�⿡ ���缭 ���Կ� �־��ش�.
    public void SetItem(BaseNode node,Vector2Int index)
    {

    }



    ////�̸� ������� ��� ��ü�� �Ѱܹ޾Ƽ� ����
    //public void InsertItem(BaseNode node)
    //{
    //    BaseSlot tempslot = null;
    //    bool flag = false;


    //    //���� ���� ������ �������� ������ �ش� �������� ������ �������� �ش�.
    //    for (int y = 0; y < InventorySlotSize.y; y++)
    //    {
    //        for (int x = 0; x < InventorySlotSize.x; x++)
    //        {
    //            if(InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode != null)
    //            {

    //                if (InventorySlotArr[x + y * (InventorySlotSize.x)].GetSettingNodeID() == node.GetItemID())
    //                {
    //                    if (node.IsStackAble())
    //                    {
    //                        InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode.ChangeStack(+1);
    //                        GameObject.Destroy(node.gameObject);
    //                        return;
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    for (int x = 0; x < QuickInvectoryNum; x++)
    //    {
    //        if(QuickSlotArr[x + 0].GetSettingNodeID() == node.GetItemID())
    //        {
    //            if (node.IsStackAble())
    //            {
    //                QuickSlotArr[x + 0].SettingNode.ChangeStack(+1);
    //                GameObject.Destroy(node.gameObject);
    //                return;
    //            }
    //        }

    //    }


    //    //����ִ� ���� �� ã�´�.
    //    for (int y = 0; y < InventorySlotSize.y; y++)
    //    {
    //        for (int x = 0; x < InventorySlotSize.x; x++)
    //        {
    //            //������ ��������� �ش� ���Կ� ������ ��带 �־��ش�.
    //            if (InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode == null)
    //            {
    //                tempslot = InventorySlotArr[x + y * (InventorySlotSize.x)];
    //                flag = true;
    //                break;
    //            }

    //        }
    //        if (flag)
    //        {
    //            break;
    //        }
    //    }

    //    //�κ��丮�� ���� á���� �����Կ��� ã�´�.
    //    if (tempslot == null)
    //    {
    //        for (int x = 0; x < QuickInvectoryNum; x++)
    //        {
    //            if (QuickSlotArr[x + 0].SettingNode == null)
    //            {
    //                tempslot = QuickSlotArr[x + 0];
    //                break;
    //            }
    //        }
    //    }

    //    if (tempslot == null)
    //    {
    //        GameObject.Destroy(node.gameObject);
    //        return;
    //    }

    //    tempslot.SetNode(node);

    //}

}
