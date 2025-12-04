using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Sprite Grass;
    public Sprite Stone;
    public Sprite Furnace;

    public List<Transform> Slot;
    public GameObject SlotItem;
    List<GameObject> items = new List<GameObject>();

    public int selectedIndex = -1;

    void Update()
    {
        for(int i = 0; i < Mathf.Min(7, Slot.Count); i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetSelectedIndex(i);
            }
        }
    }

    public void SetSelectedIndex(int idx)
    {
        ResetSelection();
        if(selectedIndex == idx)
        {
            selectedIndex = -1;
        }
        else
        {
            SetSelection(idx);
            selectedIndex = idx;
        }
    }

    public void ResetSelection()
    {
        foreach(var slot in Slot)
        {
            slot.GetComponent<Image>().color = Color.white;
        }
    }

    void SetSelection(int _idx)
    {
        Slot[_idx].GetComponent<Image>().color = Color.yellow;
    }

    public ItemType GetInventorySlot()
    {
        return items[selectedIndex].GetComponent<SlotItemPrefab>().blockType;
    }

    public void UpdateInventory(Inventory myInven)
    {
        foreach(var slotItems in items)
        {
            Destroy(slotItems);
        }
        items.Clear();

        int idx = 0;
        foreach(var item in myInven.items)
        {
            var go = Instantiate(SlotItem, Slot[idx].transform);
            go.transform.localPosition = Vector3.zero;
            SlotItemPrefab sitem = go.GetComponent<SlotItemPrefab>();
            items.Add(go);

            switch(item.Key)
            {
                case ItemType.Grass:
                    sitem.ItemSetting(Grass, $"{item.Value}", ItemType.Grass);
                    break;
                    //sitem.itemImage = 
                case ItemType.Stone:
                    sitem.ItemSetting(Stone, $"{item.Value}", ItemType.Stone);
                    break;
                case ItemType.Furnace:
                    sitem.ItemSetting(Furnace, $"{item.Value}", ItemType.Furnace);
                    break;

            }
            idx++;
        }    
    }
}
