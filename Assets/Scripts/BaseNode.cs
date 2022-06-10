using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    Vector2Int Index;

    private bool _isactive;//Ȱ����� / ��Ȱ�� ���
    private bool _isclicked;//Ŭ���Ȼ��� / Ŭ������ ���� ����

    public RectTransform rectTransform;//�ڱ� �ڽ��� recttransform

    public BaseSlot SettedSlot;//��尡 ���õǾ� �ִ� ����
    public List<BaseSlot> SettedSlotList;//��尡 ���õǾ� �ִ� ���Ե�


    public BaseSlot PreSlot;//��尡 Ŭ���Ǿ ���콺�� ����ٴҶ� ���� �ִ� ������ ��ġ�� ������ ���´�.



    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void AddSettedSlotList(BaseSlot slot)
    {
        SettedSlotList.Add(slot);
    }

    //������ �߰��뿡 �ִ� ��Ȱ�� ������� ���� ���� â���� ��ȣ�ۿ��ϴ� Ȱ��������� �����ϱ� ���� ������Ƽ
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

    //Ŭ���� �������� �ƴ��� 
    public bool NodeIsClicked
    {
        get
        {
            return _isclicked;
        }
        set
        {
            _isclicked = value;

        }
    }

    //�ش� ��带 ���ݾ� �ΰ��� ���� �����ش�.
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
    //������ ũ�⸦ ����
    public virtual Vector2Int GetSize()
    {
        return Vector2Int.zero;
    }

    //�ش� �������� �ϳ� ����� �������ش�.
    public virtual BaseNode GetDuplicateNode()
    {
        return null;
    }
    //������ ����
    public virtual void ItemMerge(BaseNode node)
    {

    }
    //�������� ������ ����
    public virtual void ChangeStack(int val)
    {
        
    }
    //�������� ������ ����
    public virtual int GetStack()
    {
        return -1;
    }
    //�����۾��̵� ����
    public virtual int GetItemID()
    {
        return -1;
    }
    //������ ������ ���������� �ƴ��� ����
    public virtual bool IsStackAble()
    {
        return false;
    }


    public virtual void Update()
    {
        
    }
}
