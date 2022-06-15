using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//마우스 조작에 따른 노드와 슬롯 조작
public class MouseInputManager : MonoBehaviour
{
    GraphicRaycaster raycaster;
    public BaseNode ClickedObj;
    public ItemNode LastOverlayNode;


    private void Awake()
    {
        raycaster = GetComponent<GraphicRaycaster>();
    }


    public void RightMouseDown(Vector2 pos)
    {

    }

    
    

    //현재 마우스의 위치에 현재 클릭된 노드의 크기만큼의 비어있는 슬롯이 있는지 확인한다.
    public void CheckClickNodeSlot()
    {



    }



    



    public void LeftMouseDown(Vector2 pos)
    {
        //canvas에 있는 graphicraycast를 이용해 클릭된 위치에 있는 객체들의 정보들을 받아온다.
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
                    if (node.NodeIsActive)//활성노드
                    {
                        if (!node.NodeIsClicked)//클릭된 노드가 아닐때
                        {
                            Debug.Log("노드 클릭됨");
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
                    //else//비활성노드
                    //{
                    //    //해당 노드가 클릭되면 아이템창에 해당 아이템을 추가해준다.
                    //    BaseNode copyNode = GameObject.Instantiate<BaseNode>(node);
                    //    copyNode.NodeIsActive = true;
                    //    ItemBag.Instance.InsertItem(copyNode);
                    //}

                    Debug.Log($"{a.gameObject.name} clicked");
                }
            }
        }
        else//노드가 클릭된 상태에서 다시 클릭을 하면
        {
            BaseSlot[] resultslot = new BaseSlot[4];
            int count = 0;
            bool flag = true;

            //아무것도 없는 공간에 노드를 두면 해당 노드는 파괴
            if (result.Count == 1)
            {
                if (ClickedObj != null)
                {
                    GameObject.Destroy(ClickedObj.gameObject);
                    ClickedObj = null;
                    return;
                }
            }

            ////무언가 있으면 해당 위치에 있는 오브젝트들 확인
            //foreach (var a in result)
            //{
                
            //}

            //그래그하는 노드가 있을때
            if (ClickedObj != null)
            {
                //해당 노드의 위치에 존재하는 슬롯들을 찾는다.
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

                //모든 포인트가 슬롯안에 들어와 있고 들어간 슬롯이 같은 종류일때 그리고 들어갈 슬롯이 모두 비어있는 슬롯일때.
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

                    if(resultslot[0].GetSlotTypes() != EnumTypes.SlotTypes.Equip)//장비 슬롯이 아니라 다른 슬롯에 아이템을 넣을때
                    {
                        if (!ItemBag.Instance.SlotIsEmpty(EnumTypes.SlotTypes.Item, resultslot[0].SlotIndex, ClickedObj.GetSize()))//아이템 가방에 해당 크기만큼 비어있는지 확인하고
                        {
                            flag = false;
                        }

                        if (flag)//이버있으면 아이템을 넣어준다.
                        {
                            Debug.Log("슬롯에 집어넣음");
                            
                            
                            if(ItemBag.Instance.SetItem(ClickedObj, resultslot[0]))
                                ClickedObj = null;

                        }
                    }
                    else//아이템을 넣으려는 슬롯이 장비 슬롯일때
                    {
                        if(EquipmentWindow.Instance.EquipEquipments(ClickedObj, resultslot[0]))
                            ClickedObj = null;
                    }
                    

                }


                ////해당 마우스의 위치에서 슬롯을 찾는다.
                //if (a.gameObject.tag == "Slot")
                //{
                //    slot = a.gameObject.GetComponent<BaseSlot>();
                //    //if (slot.GetSlotTypes() != EnumTypes.SlotTypes.Result)
                //    //{
                //    //    //찾은 슬롯이 빈 슬롯일때 해당 아이템 노드를 해당 노드에 세팅해준다.
                //    //    if (slot.SettingNode == null)
                //    //    {
                //    //        slot.SetNode(ClickedObj);
                //    //        //ClickedObj = null;
                //    //    }
                //    //    else
                //    //    {
                //    //        //클릭된 곳에 아이템이 들어있는 슬롯이 있고 해당 아이템이 드래그하고 있는 아이템과 같은 아이템이면 아이템을 병합한다.
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

            //드래그 중인 노드가 있는데 마우스의 위치에 슬롯이 없으면 노드는 원래있었던 자리로 돌아간다.
            if (ClickedObj != null && slot == null)
            {
                Debug.Log("다시돌아감");

                if (ClickedObj.PreSlot.GetSlotTypes() != EnumTypes.SlotTypes.Equip)
                    ItemBag.Instance.SetItem(ClickedObj, ClickedObj.PreSlot);
                else
                    EquipmentWindow.Instance.EquipEquipments(ClickedObj, ClickedObj.PreSlot);

                //ItemBag.Instance.SetItem(ClickedObj, ClickedObj.PreSlot);
                //ClickedObj.PreSlot.SetNode(ClickedObj);
                ClickedObj = null;
            }

            if (ClickedObj != null)
                ClickedObj = null;
            Debug.Log("노드파괴");
        }

    }

    //public void LeftMouseDown(Vector2 pos)
    //{
    //    //canvas에 있는 graphicraycast를 이용해 클릭된 위치에 있는 객체들의 정보들을 받아온다.
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
    //            if (node.NodeIsActive)//활성노드
    //            {
    //                if (!node.NodeIsClicked)//클릭된 노드가 아닐때
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
    //            //else//비활성노드
    //            //{
    //            //    //해당 노드가 클릭되면 아이템창에 해당 아이템을 추가해준다.
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
    //    //canvas에 있는 graphicraycast를 이용해 클릭된 위치에 있는 객체들의 정보들을 받아온다.
    //    PointerEventData ped = new PointerEventData(null);
    //    ped.position = pos;
    //    List<RaycastResult> result = new List<RaycastResult>();
    //    raycaster.Raycast(ped, result);

    //    BaseNode node = null;
    //    BaseSlot slot = null;

    //    Debug.Log($"개수{result.Count}");

    //    //아무것도 없는 공간에 노드를 두면 해당 노드는 파괴
    //    if (result.Count == 1)
    //    {
    //        if (ClickedObj != null)
    //        {
    //            GameObject.Destroy(ClickedObj.gameObject);
    //            ClickedObj = null;
    //            return;
    //        }
    //    }

    //    //무언가 있으면 해당 위치에 있는 오브젝트들 확인
    //    foreach (var a in result)
    //    {
    //        //그래그하는 노드가 있을때
    //        if (ClickedObj != null)
    //        {
    //            //해당 마우스의 위치에서 슬롯을 찾는다.
    //            if (a.gameObject.tag == "Slot")
    //            {
    //                slot = a.gameObject.GetComponent<BaseSlot>();
    //                //if (slot.GetSlotTypes() != EnumTypes.SlotTypes.Result)
    //                //{
    //                //    //찾은 슬롯이 빈 슬롯일때 해당 아이템 노드를 해당 노드에 세팅해준다.
    //                //    if (slot.SettingNode == null)
    //                //    {
    //                //        slot.SetNode(ClickedObj);
    //                //        //ClickedObj = null;
    //                //    }
    //                //    else
    //                //    {
    //                //        //클릭된 곳에 아이템이 들어있는 슬롯이 있고 해당 아이템이 드래그하고 있는 아이템과 같은 아이템이면 아이템을 병합한다.
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

    //    //드래그 중인 노드가 있는데 마우스의 위치에 슬롯이 없으면 노드는 원래있었던 자리로 돌아간다.
    //    if (ClickedObj != null && slot == null)
    //    {
    //        ClickedObj.PreSlot.SetNode(ClickedObj);
    //        ClickedObj = null;
    //    }

    //    if (ClickedObj != null)
    //        ClickedObj = null;
    //}

    //아이템 노드가 클릭되어 마무스에 끌려다니고 있는 상태에서의 스냅을 구현
    //public void DraggingItem(Vector2 pos)
    //{
    //    //해당 아이템 노드의 크기를 받아와서 네 귀퉁이에 raycast를 이용해서 슬롯 위에 들어왔는지릴 확인한다.
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
        
        
    //    //모든 포인트가 슬롯안에 들어와 있고 들어간 슬롯이 같은 종류일때 해당 슬롯에 세팅해준다.
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


    public float lasttime = 0.0f;
    //마우스가 UI 요소들 위에 올라와 있는지 확인한다.(0.1초에 한번씩 실행)
    public void CheckMouseOveray()
    {
        if (Time.time - lasttime >= 0.1f)
        {
            lasttime = Time.time;
            PointerEventData ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> result = new List<RaycastResult>();
            raycaster.Raycast(ped, result);
            ItemNode node;


            foreach (var a in result)
            {
                
                if (a.gameObject.tag == "Node")//해당 위치에 노드가 있고 해당 노드가 슬롯에 셋팅되어 있는 노드면
                {
                    node = a.gameObject.GetComponent<ItemNode>();
                    if(node.SettedSlotList.Count > 0)
                    {
                        if (node.IsMouseOn == false)//이미 정보를 보여주고 있었는지 여부를 확인하고
                        {
                            node.IsMouseOn = true;//아니라면 정보창을 보여준다.

                            if (LastOverlayNode == null)
                                LastOverlayNode = node;

                            if (LastOverlayNode != node)//만약 이전에 정보를 보여주고 있던 노드가 있고 현재 보여주고 있는 노드와 다른 노드면 
                            {
                                LastOverlayNode.IsMouseOn = false;//이전 노드는 그만 보여주도록 해준다.
                                LastOverlayNode = node;
                            }


                        }
                        return;
                    }
                    
                }

            }

            if(LastOverlayNode!=null)//현재 마우스 위치에 아무 노드도 없는데 아이템 정보를 보여주고 있는 노드가 있으면 그만 보여주도록 한다.
            {
                if (LastOverlayNode.IsMouseOn == true)
                    LastOverlayNode.IsMouseOn = false;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseDown(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //LeftMouseUp(Input.mousePosition);
        }

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

        CheckMouseOveray();
    }
}
