using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EquipmentWindow : MonoBehaviour
{
    public List<BaseSlot> equipslots;

    private void Awake()
    {
        BaseSlot[] temps = (BaseSlot[])GetComponentsInChildren<EquipSlot>();
        equipslots = temps.ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
