using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enumµé
public class EnumTypes
{
    public enum ItemCollums
    {
        ItemCode,
        Name,
        Damage,
        Durability,
        StrRequire,
        DexRequire,
        Price,
        QualityLevel,
        Category,
        Parts,
        Grade,
        SpriteNum,
        Size,
        CollumMax
    }

    public enum SlotTypes
    {
        Item,
        Equip,
        Quick,
        TypeMax
    }

    public enum ItemTypes
    {
        Equips = 1000 ,
        StackAble = 2000,
        TypeMax
    }
    

    public enum EquipmentTypes
    {
        Head,
        Body,
        RightArm,
        LeftArm,
        TwoHand,
        Amulet,
        Ring,
        EquipMax
    }


    public enum RecipeCollums
    {
        ResultItem,
        ResultName,
        Count,
        Slot1,
        Slot2,
        Slot3,
        Slot4,
        Slot5,
        Slot6,
        Slot7,
        Slot8,
        Slot9,
        CollumMax
    }

    
}
