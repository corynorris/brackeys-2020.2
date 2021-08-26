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

    private static bool IsDirectionFree(Vector3 position, Vector3 direction)
    {
        LayerMask mask = LayerMask.GetMask("Obstacles"); // doesn't seem to work? maybe layers are whack

        RaycastHit2D hit = Physics2D.Raycast(position, direction, 1f, mask);
        Debug.DrawRay(position, direction, Color.red, 2f);

        if (hit.collider != null)
        {
            Debug.Log("Collided with: " + hit.collider.name);
            return false;
        }
        return true;
    }

    private static Vector3 FindFreeDirection(Vector3 position, ref Vector3 direction)
    {

        for (int i = 0; i < 10; i++)
        {
            bool dropPositionIsFree = IsDirectionFree(position, direction);
            
            if (dropPositionIsFree)
            {
                return position + direction;
            }
            
            direction = Quaternion.AngleAxis(-36, new Vector3(0,0,1)) * direction;
        }

        return position;
    }

    public static ItemWorld DropItemInDirection(Vector3 position, Item item, Vector3 direction)
    {
        if (!IsDirectionFree(position, direction)) direction = Utils.GetRandomDir();

        Vector3 dropPosition = FindFreeDirection(position, ref direction);
        ItemWorld itemWorld = SpawnItemWorld(dropPosition, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(direction * 2f, ForceMode2D.Impulse);

        return itemWorld;

    }


    public static ItemWorld DropItemRandom(Vector3 position, Item item)
    {
        Vector3 randomDir = Utils.GetRandomDir();
        return DropItemInDirection(position, item, randomDir);
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

        light2D.pointLightOuterRadius = item.GetLightRadius();
        light2D.intensity = item.GetLightIntensity();

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
