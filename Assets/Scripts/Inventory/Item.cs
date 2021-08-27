using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item 
{
    public static ItemType[] Resources = { ItemType.Scrap, ItemType.Nurition };

    public enum ItemType
    {
        Scrap,
        Nurition,
        Light,
        Oxygen,
        HoverBoard
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
            case ItemType.Light: return ItemAssets.Instance.lightSprite;
            case ItemType.Oxygen: return ItemAssets.Instance.oxygenSprite;
            case ItemType.HoverBoard: return ItemAssets.Instance.hoverBoard;
        }
    }


    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Scrap: return new Color(0,0,1);
            case ItemType.Nurition: return new Color(0.8f, 0.8f, 0.4f);
            case ItemType.Light: return new Color(1f,0f,0f);
            case ItemType.Oxygen: return new Color(0.0f, 0.5f, 0.2f);
            case ItemType.HoverBoard: return new Color(0.1f, 0.1f, 0.6f);
        }
    }
    public bool IsConsumable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Scrap: return false;
            case ItemType.Nurition: return false;
            case ItemType.Light: return true;
            case ItemType.Oxygen: return true;
            case ItemType.HoverBoard: return true;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Scrap: return true;
            case ItemType.Nurition: return true;
            case ItemType.Light: return false;
            case ItemType.Oxygen: return false;
            case ItemType.HoverBoard: return false;
        }
    }

    public float GetLightRadius()
    {
        switch (itemType)
        {
            case ItemType.Light: return 10f;
            default: return 0.7f;
        }
    }
    public float GetLightIntensity()
    {
        switch (itemType)
        {
            case ItemType.Light: return 0.4f;
            default: return 0.4f;
        }
    }

}
