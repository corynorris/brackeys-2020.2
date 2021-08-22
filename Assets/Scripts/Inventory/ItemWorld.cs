using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItemInDirection(Vector3 dropPosition, Item item, Vector3 direction)
    {
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + direction, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(direction * 2f, ForceMode2D.Impulse);
        return itemWorld;
    }


    public static ItemWorld DropItemRandom(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = Utils.GetRandomDir();
        return DropItemInDirection(dropPosition, item, randomDir);
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    private TextMeshPro uiTextAmount;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = transform.Find("light").GetComponent<Light2D>();
        uiTextAmount = transform.Find("amount").GetComponent<TextMeshPro>();
        uiTextAmount.sortingOrder = spriteRenderer.sortingOrder +1;
        
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        Color color = item.GetColor();
        light2D.color = item.GetColor();

        
        if (item.amount > 1)
        {
            uiTextAmount.SetText(item.amount.ToString());
        }
        else
        {
            uiTextAmount.SetText("");
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
