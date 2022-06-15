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

    //오브젝트 객체와 연결이 되면 해당 객체의 위치에 해당하는 ui에서의 위치에 따라 움직이도록 해준다. 
    void Update()
    {

        //LinkedObj.

    }
}
