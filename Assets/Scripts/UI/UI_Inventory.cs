using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{

    private Vector2 gridSize = new Vector2(4,4);
    private Vector2 padding;

    private Inventory inventory;
    private Transform itemContainer;
    private Transform itemSlotTemplate;
    private RectTransform itemSlotRect;

    private Player player;

    private void Awake()
    {
        itemContainer = transform.Find("itemContainer");
        itemSlotTemplate = itemContainer.Find("itemSlotTemplate");
        itemSlotRect = itemSlotTemplate.GetComponent<RectTransform>();
        itemSlotTemplate.gameObject.SetActive(false);


        RectTransform containerRect = itemContainer.GetComponent<RectTransform>();
        Vector2 containerSpace = new Vector2(
            containerRect.rect.width - 2 * (itemSlotRect.anchoredPosition.x),
            containerRect.rect.height - 2 * (-itemSlotRect.anchoredPosition.y)
        );

        padding.x = (containerSpace.x - (gridSize.x * itemSlotRect.rect.width)) / (gridSize.x - 1);
        padding.y = (containerSpace.y - (gridSize.y * itemSlotRect.rect.height)) / (gridSize.y - 1);
    }


    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }


    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    private void RefreshInventoryItems()
    {
        ClearInventoryContainer();
        DrawInventorySlots();
    }


    private void ClearInventoryContainer()
    {
        foreach (Transform child in itemContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
    }

    // TODO: Factor out slot logic
    private void DrawInventorySlots()
    {
        int x = 0;
        int y = 0;
        Vector2 cumulativePadding = Vector2.zero;

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform rectTransform = Instantiate(itemSlotTemplate, itemContainer).GetComponent<RectTransform>();
            rectTransform.gameObject.SetActive(true);

            rectTransform.GetComponent<Button>().onClick.AddListener(() => {
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                ItemWorld.DropItemRandom(player.GetCenter(), duplicateItem);
                inventory.RemoveItem(item);
            });

            rectTransform.anchoredPosition = new Vector2(x * itemSlotRect.rect.width + cumulativePadding.x, y * itemSlotRect.rect.height + cumulativePadding.y) + itemSlotRect.anchoredPosition;
            cumulativePadding.x += padding.x;

            Image uiIcon = rectTransform.Find("icon").GetComponent<Image>();
            uiIcon.sprite = item.GetSprite();


            TextMeshProUGUI uiTextAmount = rectTransform.Find("amount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiTextAmount.SetText(item.amount.ToString());
            } else
            {
                uiTextAmount.SetText("");
            }

            x++;
            if (x >= gridSize.x)
            {
                x = 0;
                y--;
                cumulativePadding.x = 0;
                cumulativePadding.y -= padding.y;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            bool isActive = itemContainer.gameObject.activeSelf;
            itemContainer.gameObject.SetActive(!isActive);
        }   
    }
}

