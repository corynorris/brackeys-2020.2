using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item 
{
   public enum ItemType
    {
        Scrap,
        Nurition,
    }

    public ItemType itemType;
    public int amount;

    public static ItemType GetRandomType()
    {
        Array values = Enum.GetValues(typeof(ItemType));
        System.Random random = new System.Random();
        return (ItemType)values.GetValue(random.Next(values.Length));
    }

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Scrap: return ItemAssets.Instance.scrapSprite;
            case ItemType.Nurition: return ItemAssets.Instance.nutritionSprite;
        }
    }


    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Scrap: return new Color(0,0,1);
            case ItemType.Nurition: return new Color(0.8f, 0.8f, 0.4f);
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Scrap: return false;
            case ItemType.Nurition: return true;
        }
    }
}
