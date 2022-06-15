using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPanel : MonoBehaviour
{
    GameObject LinkedObj;
    public delegate void ClickedEvent();
    protected ClickedEvent clickedevent;

    public Vector3 intervalpos;

    public void LinkObjectPanel(GameObject obj,ClickedEvent cevent,Vector2 pos)
    {
        LinkedObj = obj;
        clickedevent += cevent;
        intervalpos = pos;
    }

    public void SetPos()
    {
        Vector3 temp = Camera.main.WorldToScreenPoint(LinkedObj.transform.position);
        temp += intervalpos;
        this.transform.position = temp;
    }

    public void ObjectPanelClick()
    {
        clickedevent();
    }

    //������Ʈ ��ü�� ������ �Ǹ� �ش� ��ü�� ��ġ�� �ش��ϴ� ui������ ��ġ�� ���� �����̵��� ���ش�. 
    void Update()
    {

        //LinkedObj.

    }
}
