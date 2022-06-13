using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//���콺 ���ۿ� ���� ���� ���� ����
public class MouseInputManager : MonoBehaviour
{
    GraphicRaycaster raycaster;
    public BaseNode ClickedObj;

    private void Awake()
    {
        raycaster = GetComponent<GraphicRaycaster>();
    }


    public void RightMouseDown(Vector2 pos)
    {

    }


    

    //���� ���콺�� ��ġ�� ���� Ŭ���� ����� ũ�⸸ŭ�� ����ִ� ������ �ִ��� Ȯ���Ѵ�.
    public void CheckClickNodeSlot()
    {



    }



    



    public void LeftMouseDown(Vector2 pos)
    {
        //canvas�� �ִ� graphicraycast�� �̿��� Ŭ���� ��ġ�� �ִ� ��ü���� �������� �޾ƿ´�.
        PointerEventData ped = new PointerEventData(null);
        ped.position = pos;
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(ped, result);

        BaseNode node = null;
        BaseSlot slot = null;


        if (ClickedObj == null)
        {
            foreach (var a in result)
            {
                if (a.gameObject.tag == "Node")
                {
                    node = a.gameObject.GetComponent<BaseNode>();
                    if (node.NodeIsActive)//Ȱ�����
                    {
                        if (!node.NodeIsClicked)//Ŭ���� ��尡 �ƴҶ�
                        {
                            Debug.Log("��� Ŭ����");
                            ClickedObj = node.NodeClick();
                            //if (IsControl)
                            //{
                            //    ClickedObj = node.DivideNode();
                            //}
                            //else
                            //{
                            //    ClickedObj = node.SettedSlot.GetSettingNode();
                            //}
                        }
                    }
                    //else//��Ȱ�����
                    //{
                    //    //�ش� ��尡 Ŭ���Ǹ� ������â�� �ش� �������� �߰����ش�.
                    //    BaseNode copyNode = GameObject.Instantiate<BaseNode>(node);
                    //    copyNode.NodeIsActive = true;
                    //    ItemBag.Instance.InsertItem(copyNode);
                    //}

                    Debug.Log($"{a.gameObject.name} clicked");
                }
            }
        }
        else//��尡 Ŭ���� ���¿��� �ٽ� Ŭ���� �ϸ�
        {
            BaseSlot[] resultslot = new BaseSlot[4];
            int count = 0;
            bool flag = true;

            //�ƹ��͵� ���� ������ ��带 �θ� �ش� ���� �ı�
            if (result.Count == 1)
            {
                if (ClickedObj != null)
                {
                    GameObject.Destroy(ClickedObj.gameObject);
                    ClickedObj = null;
                    return;
                }
            }

            ////���� ������ �ش� ��ġ�� �ִ� ������Ʈ�� Ȯ��
            //foreach (var a in result)
            //{
                
            //}

            //�׷����ϴ� ��尡 ������
            if (ClickedObj != null)
            {
                //�ش� ����� ��ġ�� �����ϴ� ���Ե��� ã�´�.
                for (int i = 0; i < 4; i++)
                {
                    Vector2 temppos = ClickedObj.GetCastPoint(i);
                    ped.position = pos + temppos;
                    result.Clear();
                    raycaster.Raycast(ped, result);
                    
                    foreach (var aa in result)
                    {
                        if (aa.gameObject.tag == "Slot")
                        {
                            count++;
                            resultslot[i] = aa.gameObject.GetComponent<BaseSlot>();
                            break;
                        }
                    }
                }

                //��� ����Ʈ�� ���Ծȿ� ���� �ְ� �� ������ ���� �����϶� �׸��� �� ������ ��� ����ִ� �����϶�.
                if (count >= 4)
                {
                    EnumTypes.SlotTypes type = resultslot[0].GetSlotTypes();
                    for (int i = 1; i < resultslot.Length; i++)
                    {
                        if (type != resultslot[i].GetSlotTypes())
                        {
                            //ClickedObj = ClickedObj.NodeClick();
                            flag = false;
                            break;
                        }

                    }

                    if (!ItemBag.Instance.SlotIsEmpty(EnumTypes.SlotTypes.Item, resultslot[0].SlotIndex, ClickedObj.GetSize()))
                    {
                        flag = false;
                    }

                    if (flag)
                    {
                        Debug.Log("���Կ� �������");
                        ItemBag.Instance.SetItem(ClickedObj, resultslot[0]);
                        ClickedObj = null;
                    }

                }


                ////�ش� ���콺�� ��ġ���� ������ ã�´�.
                //if (a.gameObject.tag == "Slot")
                //{
                //    slot = a.gameObject.GetComponent<BaseSlot>();
                //    //if (slot.GetSlotTypes() != EnumTypes.SlotTypes.Result)
                //    //{
                //    //    //ã�� ������ �� �����϶� �ش� ������ ��带 �ش� ��忡 �������ش�.
                //    //    if (slot.SettingNode == null)
                //    //    {
                //    //        slot.SetNode(ClickedObj);
                //    //        //ClickedObj = null;
                //    //    }
                //    //    else
                //    //    {
                //    //        //Ŭ���� ���� �������� ����ִ� ������ �ְ� �ش� �������� �巡���ϰ� �ִ� �����۰� ���� �������̸� �������� �����Ѵ�.
                //    //        if (slot.SettingNode.GetItemID() == ClickedObj.GetItemID() && ClickedObj.IsStackAble())
                //    //        {
                //    //            slot.SettingNode.ItemMerge(ClickedObj);
                //    //            ClickedObj = null;
                //    //        }
                //    //        slot = null;
                //    //        //ClickedObj = null;
                //    //    }
                //    //}
                //    //else
                //    //{
                //    //    slot = null;
                //    //}
                //}

            }

            //�巡�� ���� ��尡 �ִµ� ���콺�� ��ġ�� ������ ������ ���� �����־��� �ڸ��� ���ư���.
            if (ClickedObj != null && slot == null)
            {
                Debug.Log("�ٽõ��ư�");
                ItemBag.Instance.SetItem(ClickedObj, ClickedObj.PreSlot);
                //ClickedObj.PreSlot.SetNode(ClickedObj);
                ClickedObj = null;
            }

            if (ClickedObj != null)
                ClickedObj = null;
            Debug.Log("����ı�");
        }

    }

    //public void LeftMouseDown(Vector2 pos)
    //{
    //    //canvas�� �ִ� graphicraycast�� �̿��� Ŭ���� ��ġ�� �ִ� ��ü���� �������� �޾ƿ´�.
    //    PointerEventData ped = new PointerEventData(null);
    //    ped.position = pos;
    //    List<RaycastResult> result = new List<RaycastResult>();
    //    raycaster.Raycast(ped, result);
    //    BaseNode node = null;
    //    foreach (var a in result)
    //    {
    //        if (a.gameObject.tag == "Node")
    //        {
    //            node = a.gameObject.GetComponent<BaseNode>();
    //            if (node.NodeIsActive)//Ȱ�����
    //            {
    //                if (!node.NodeIsClicked)//Ŭ���� ��尡 �ƴҶ�
    //                {
    //                    ClickedObj = node.SettedSlot.GetSettingNode();

    //                    //if (IsControl)
    //                    //{
    //                    //    ClickedObj = node.DivideNode();
    //                    //}
    //                    //else
    //                    //{
                            
    //                    //}
    //                }
    //            }
    //            //else//��Ȱ�����
    //            //{
    //            //    //�ش� ��尡 Ŭ���Ǹ� ������â�� �ش� �������� �߰����ش�.
    //            //    BaseNode copyNode = GameObject.Instantiate<BaseNode>(node);
    //            //    copyNode.NodeIsActive = true;
    //            //    ItemBag.Instance.InsertItem(copyNode);
    //            //}

    //            Debug.Log($"{a.gameObject.name} clicked");
    //        }
    //    }
    //}

    //public void LeftMouseUp(Vector2 pos)
    //{
    //    //canvas�� �ִ� graphicraycast�� �̿��� Ŭ���� ��ġ�� �ִ� ��ü���� �������� �޾ƿ´�.
    //    PointerEventData ped = new PointerEventData(null);
    //    ped.position = pos;
    //    List<RaycastResult> result = new List<RaycastResult>();
    //    raycaster.Raycast(ped, result);

    //    BaseNode node = null;
    //    BaseSlot slot = null;

    //    Debug.Log($"����{result.Count}");

    //    //�ƹ��͵� ���� ������ ��带 �θ� �ش� ���� �ı�
    //    if (result.Count == 1)
    //    {
    //        if (ClickedObj != null)
    //        {
    //            GameObject.Destroy(ClickedObj.gameObject);
    //            ClickedObj = null;
    //            return;
    //        }
    //    }

    //    //���� ������ �ش� ��ġ�� �ִ� ������Ʈ�� Ȯ��
    //    foreach (var a in result)
    //    {
    //        //�׷����ϴ� ��尡 ������
    //        if (ClickedObj != null)
    //        {
    //            //�ش� ���콺�� ��ġ���� ������ ã�´�.
    //            if (a.gameObject.tag == "Slot")
    //            {
    //                slot = a.gameObject.GetComponent<BaseSlot>();
    //                //if (slot.GetSlotTypes() != EnumTypes.SlotTypes.Result)
    //                //{
    //                //    //ã�� ������ �� �����϶� �ش� ������ ��带 �ش� ��忡 �������ش�.
    //                //    if (slot.SettingNode == null)
    //                //    {
    //                //        slot.SetNode(ClickedObj);
    //                //        //ClickedObj = null;
    //                //    }
    //                //    else
    //                //    {
    //                //        //Ŭ���� ���� �������� ����ִ� ������ �ְ� �ش� �������� �巡���ϰ� �ִ� �����۰� ���� �������̸� �������� �����Ѵ�.
    //                //        if (slot.SettingNode.GetItemID() == ClickedObj.GetItemID() && ClickedObj.IsStackAble())
    //                //        {
    //                //            slot.SettingNode.ItemMerge(ClickedObj);
    //                //            ClickedObj = null;
    //                //        }
    //                //        slot = null;
    //                //        //ClickedObj = null;
    //                //    }
    //                //}
    //                //else
    //                //{
    //                //    slot = null;
    //                //}
    //            }

    //        }
    //    }

    //    //�巡�� ���� ��尡 �ִµ� ���콺�� ��ġ�� ������ ������ ���� �����־��� �ڸ��� ���ư���.
    //    if (ClickedObj != null && slot == null)
    //    {
    //        ClickedObj.PreSlot.SetNode(ClickedObj);
    //        ClickedObj = null;
    //    }

    //    if (ClickedObj != null)
    //        ClickedObj = null;
    //}

    //������ ��尡 Ŭ���Ǿ� �������� �����ٴϰ� �ִ� ���¿����� ������ ����
    //public void DraggingItem(Vector2 pos)
    //{
    //    //�ش� ������ ����� ũ�⸦ �޾ƿͼ� �� �����̿� raycast�� �̿��ؼ� ���� ���� ���Դ����� Ȯ���Ѵ�.
    //    PointerEventData ped = new PointerEventData(null);
       
    //    List<RaycastResult> result = new List<RaycastResult>();

    //    BaseSlot[] slot = new BaseSlot[ClickedObj.castpoint.Length];
    //    int count = 0;

    //    for (int i=0;i<ClickedObj.castpoint.Length;i++)
    //    {
    //        result.Clear();
    //        ped.position = pos + ClickedObj.GetCastPoint(i);
    //        raycaster.Raycast(ped, result);
            
    //        foreach (var a in result)
    //        {
    //            if (a.gameObject.tag == "Slot")
    //            {
    //                count++;
    //                slot[i] = a.gameObject.GetComponent<BaseSlot>();
    //                break;
    //            }
    //        }
    //    }
        
        
    //    //��� ����Ʈ�� ���Ծȿ� ���� �ְ� �� ������ ���� �����϶� �ش� ���Կ� �������ش�.
    //    if(count>=4)
    //    {
    //        EnumTypes.SlotTypes type = slot[0].GetSlotTypes();
    //        for (int i = 1; i < slot.Length; i++)
    //        {
    //            if (type != slot[i].GetSlotTypes())
    //            {
    //                //ClickedObj = ClickedObj.NodeClick();
    //                return;
    //            }
                    
    //        }
    //        ItemBag.Instance.SetItem(ClickedObj, slot[0]);
    //    }
    //    else
    //    {
    //        if(ClickedObj.SettedSlotList.Count>0)

    //            ClickedObj = ClickedObj.NodeClick();
    //    }

    //}
    public void RightMouseUp(Vector2 pos)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseDown(Input.mousePosition);
        }

        //if (Input.GetMouseButtonUp(0))
        //{
        //    //LeftMouseUp(Input.mousePosition);
        //}

        if (Input.GetMouseButtonDown(1))
        {
            RightMouseDown(Input.mousePosition);
        }

        //if (Input.GetMouseButtonUp(1))
        //{
        //    //RightMouseUp(Input.mousePosition);
        //}

        if(ClickedObj!=null)
        {
            //DraggingItem(Input.mousePosition);
        }
    }
}
