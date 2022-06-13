using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    Vector2Int Index;
    [SerializeField]
    private bool _isactive;//활성노드 / 비활성 노드
    [SerializeField]
    private bool _isclicked;//클릭된상태 / 클릭되지 않은 상태

    //[SerializeField]
    //private bool _isSnapped;//스냅에 의해 붙어있는 상태 / 스냅에 의해 붙어있지 않은 상태

    public RectTransform rectTransform;//자기 자신의 recttransform

    public BaseSlot SettedSlot;//노드가 셋팅되어 있는 슬롯

    public List<BaseSlot> SettedSlotList;//노드가 셋팅되어 있는 슬롯들


    public BaseSlot PreSlot;//노드가 클릭되어서 마우스를 따라다닐때 원래 있던 슬롯의 위치를 저장해 놓는다.


    public Transform[] castpoint = new Transform[4];
    public Vector2 castpointpos;


    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        

    }

    //노드 스냅에 사용
    public void Snap(BaseSlot slot)
    {
        //노드가 달라붙을 슬롯의 번호를 주면 해당 노드를 해당 슬롯에 붙인다.
    }

    public Vector2 GetCastPoint(int index)
    {
        return castpoint[index].position - this.transform.position;
    }

    public BaseNode NodeClick()
    {
        //BaseNode temp = this;
        NodeIsClicked = true;
        //this.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        PreSlot = SettedSlotList[0];
        foreach(var a in SettedSlotList)
        {
            a.ClearSettingNode();
        }
        SettedSlotList.Clear();
        return this;
    }    

    public void AddSettedSlotList(BaseSlot slot)
    {
        SettedSlotList.Add(slot);
    }

    //해당 노드가 상호작용 할 수 있는 활성 노드인지를 확인
    public bool NodeIsActive
    {
        get
        {
            return _isactive;
        }
        set
        {
            _isactive = value;
        }
    }

    //클릭된 상태인지 아닌지 
    public bool NodeIsClicked
    {
        get
        {
            return _isclicked;
        }
        set
        {
            _isclicked = value;
            if(_isclicked)
            {
                this.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            }
            else
            {
                this.rectTransform.pivot = new Vector2(0.0f, 1.0f);
            }

        }
    }




    public virtual void InitSetting(Dictionary<int, string> itemdata, Sprite sprite)
    {

    }

    public virtual void InitSetting(int itemcode, string Name, Sprite sprite, Vector3 pos, string _type)
    {

    }

    //해당 노드를 절반씩 두개의 노드로 나눠준다.
    public virtual BaseNode DivideNode()
    {
        BaseNode temp = null;
        if(IsStackAble()&&GetStack()>=2)
        {
            int cur = GetStack() / 2;
            
            temp = GameObject.Instantiate<BaseNode>(this);
            temp.transform.parent = this.transform.parent;
            temp.NodeIsClicked = true;
            temp.PreSlot = SettedSlot;
            temp.ChangeStack(0);
            temp.ChangeStack(cur);
            this.ChangeStack(-cur);
        }
        else
        {
            temp = SettedSlot.GetSettingNode();
        }

        return temp;
    }
    //아이템 크기를 리턴
    public virtual Vector2Int GetSize()
    {
        return Vector2Int.zero;
    }

    //해당 아이템을 하나 떼어내서 리턴해준다.
    public virtual BaseNode GetDuplicateNode()
    {
        return null;
    }
    //아이템 병합
    public virtual void ItemMerge(BaseNode node)
    {

    }
    //아이템의 개수를 변경
    public virtual void ChangeStack(int val)
    {
        
    }
    //아이템의 개수를 리턴
    public virtual int GetStack()
    {
        return -1;
    }
    //아이템아이디를 리턴
    public virtual int GetItemID()
    {
        return -1;
    }
    //스택이 가능한 아이템인지 아닌지 구분
    public virtual bool IsStackAble()
    {
        return false;
    }


    public virtual void Update()
    {
        
    }
}
