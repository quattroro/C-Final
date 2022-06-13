using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이후에 재사용을 고려하여
//아이템 가방은 제작테이블과 독립적으로 동작한다.
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

    //초기화 작업 인텐토리 슬롯들을 만들어 준다.
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

    //해당 노드 파괴
    public void DeleteItem(BaseSlot slot, BaseNode node)
    {
        slot.RemoveNode(node);
    }

    //미리 만들어진 노드 객체를 넘겨받아서 세팅
    public void InsertItem(BaseSlot slot, BaseNode node)
    {
        if(slot.SettingNode==null)
        {
            slot.SetNode(node);
        }
    }

    //아이템을 아이템 가방에 넣어준다.
    public void InsertItem(BaseNode node)
    {
        Vector2Int itemsize;
        bool flag = false;
        //node.SettedSlotList.Clear();
        //만약 아이템이 스택이 가능한 아이템이면 퀵슬롯과 인벤토리 창에서 같은 종류의 아이템이 있는지 찾고 같은 아이템이 있으면 스택을 증가시켜 준다.

        if(node.IsStackAble())
        {
            for (int x = 0; x < QuickInvectoryNum; x++)
            {
                if (QuickSlotArr[x + 0].GetSettingNodeID() == node.GetItemID())
                {
                    QuickSlotArr[x + 0].SettingNode.ChangeStack(node.GetStack());
                    Debug.Log("퀵슬롯 스택 증가");
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
                            Debug.Log("가방 스택 증가");
                            GameObject.Destroy(node.gameObject);
                            return;
                        }
                    }
                }
            }
        }

        //스택이 불가능한 아이템이면 인벤토리 창에서 해당 아이템의 크기만큼 슬롯이 비어있는지 확인하고 비어있으면 인벤토리에 아이템을 넣어주고 아니면 그냥 없애준다.
        for (int y = 0; y < InventorySlotSize.y; y++)
        {
            for (int x = 0; x < InventorySlotSize.x; x++)
            {
                //슬롯을 찾다가 비어있는 슬롯이 있으면 
                if (InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode == null)
                {
                    //그곳에서부터 해당 아이템의 크기만큼 비어있는지 확인한다.

                    itemsize = node.GetSize();
                    if(SlotIsEmpty(EnumTypes.SlotTypes.Item,new Vector2Int(x,y),itemsize))
                    {
                        SetItem(node, new Vector2Int(x, y));
                        Debug.Log("아이템 넣어줌");
                        //InventorySlotArr[x + y * (InventorySlotSize.x)].SetNode(node, node.GetSize());
                        return;
                    }
                    //flag = false;
                    //if (x + itemsize.x <= InventorySlotSize.x && y + itemsize.y <= InventorySlotSize.y)
                    //{
                    //    for (int i = 0; i < itemsize.y; i++)
                    //    {
                    //        for (int j = 0; j < itemsize.x; j++)
                    //        {
                    //            if (InventorySlotArr[(x + j) + (y + i) * (InventorySlotSize.x)].SettingNode != null)
                    //            {
                    //                flag = true;
                    //                break;
                    //            }
                    //        }
                    //        if(flag)
                    //        {
                    //            break;
                    //        }
                    //    }
                    //    //아이템의 크기만큼 슬롯이 비어있으면 해당 슬롯의 왼쪽 위 첫번째 슬롯에 아이템을 넣어준다.
                    //    if(!flag)
                    //    {
                    //        SetItem(node, new Vector2Int(x, y));
                    //        Debug.Log("아이템 넣어줌");
                    //        //InventorySlotArr[x + y * (InventorySlotSize.x)].SetNode(node, node.GetSize());
                    //        return;
                    //    }
                    //}
                }
            }
        }
        //비어있응 곳이 없으면 객체를 없애고 끝낸다.
        Debug.Log("빈공간 없음");
        GameObject.Destroy(node.gameObject);
        return;

    }

    //아이템을 크기에 맞춰서 슬롯에 넣어준다.
    public void SetItem(BaseNode node,Vector2Int index)
    {
        Vector2 imagesize;
        BaseSlot topleft = InventorySlotArr[(index.x) + (index.y) * (InventorySlotSize.x)];
        //BaseSlot bottomright = InventorySlotArr[(index.x + node.GetSize().x) + (index.y + node.GetSize().y) * (InventorySlotSize.x)];
        node.SettedSlotList.Clear();

        imagesize.x = topleft.rectTransform.rect.width * node.GetSize().x;
        imagesize.y = topleft.rectTransform.rect.height * node.GetSize().y;

        node.transform.parent = topleft.transform;
        node.rectTransform.sizeDelta = imagesize;
        node.rectTransform.localPosition = new Vector3(0, 0, 0);




        //node.rectTransform.rect.width = imagesize.x;
        //node.rectTransform.rect.height = imagesize.y;

        for (int y=0;y<node.GetSize().y;y++)
        {
            for(int x=0;x<node.GetSize().x;x++)
            {
                //if (y == 0 && x == 0)
                    //node.rectTransform.localPosition = InventorySlotArr[(index.x + x) + (index.y + y) * (InventorySlotSize.x)].SettingNode = node;
                InventorySlotArr[(index.x + x) + (index.y + y) * (InventorySlotSize.x)].SetNodeData(node);
            }
        }
        
        

    }

    //슬롯의 시작 지점에서 아이템의 크기만큼 슬롯이 비어있는지 확인
    public bool SlotIsEmpty(EnumTypes.SlotTypes type, Vector2Int start, Vector2Int size)
    {
        if (type == EnumTypes.SlotTypes.Item)//아이템창
        {
            if (start.x + size.x > InventorySlotSize.x || start.y + size.y > InventorySlotSize.y)//아이템상의 크기를 넘어가면 false
                return false;

            for (int i = 0; i < size.y; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    if (InventorySlotArr[(start.x + j) + (start.y + i) * (InventorySlotSize.x)].SettingNode != null)
                    {
                        return false;
                    }
                }
            }

        }
        else if (type == EnumTypes.SlotTypes.Quick)//퀵슬롯창
        {
            if (start.x + size.x <= QuickInvectoryNum)//아이템창 크기를 넘어가면 false
                return false;

            if (QuickSlotArr[start.x] != null)
                return false;
        }
        else
        {

        }
        return true;
    }

    //아이템을 크기에 맞춰서 슬롯에 넣어준다.
    public void SetItem(BaseNode node, BaseSlot slot/*왼쪽 위 슬롯*/)
    {
        Vector2 imagesize;
        BaseSlot topleft = slot;
        Vector2Int index = slot.SlotIndex;
        node.SettedSlotList.Clear();

        //BaseSlot bottomright = InventorySlotArr[(index.x + node.GetSize().x) + (index.y + node.GetSize().y) * (InventorySlotSize.x)];

        imagesize.x = topleft.rectTransform.rect.width * node.GetSize().x;
        imagesize.y = topleft.rectTransform.rect.height * node.GetSize().y;

        //
        //node.rectTransform.pivot = new Vector2(0, 1);
        node.transform.parent = topleft.transform;
        node.rectTransform.sizeDelta = imagesize;
        node.rectTransform.localPosition = new Vector3(0, 0, 0);

        


        //node.rectTransform.rect.width = imagesize.x;
        //node.rectTransform.rect.height = imagesize.y;

        //아이템을 넣으려는 슬롯이 퀵슬롯일때
        if(slot.GetSlotTypes()==EnumTypes.SlotTypes.Quick)
        {
            slot.SetNodeData(node);
        }
        else if(slot.GetSlotTypes() == EnumTypes.SlotTypes.Item)
        {
            for (int y = 0; y < node.GetSize().y; y++)
            {
                for (int x = 0; x < node.GetSize().x; x++)
                {
                    //if (y == 0 && x == 0)
                    //node.rectTransform.localPosition = InventorySlotArr[(index.x + x) + (index.y + y) * (InventorySlotSize.x)].SettingNode = node;
                    InventorySlotArr[(index.x + x) + (index.y + y) * (InventorySlotSize.x)].SetNodeData(node);
                }
            }
        }

        



    }

    ////미리 만들어진 노드 객체를 넘겨받아서 세팅
    //public void InsertItem(BaseNode node)
    //{
    //    BaseSlot tempslot = null;
    //    bool flag = false;


    //    //만약 같은 종류의 아이템이 있으면 해당 아이템의 개수를 증가시켜 준다.
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


    //    //비어있는 슬롯 을 찾는다.
    //    for (int y = 0; y < InventorySlotSize.y; y++)
    //    {
    //        for (int x = 0; x < InventorySlotSize.x; x++)
    //        {
    //            //슬롯이 비어있으면 해당 슬롯에 아이템 노드를 넣어준다.
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

    //    //인벤토리가 전부 찼으면 퀵슬롯에서 찾는다.
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
