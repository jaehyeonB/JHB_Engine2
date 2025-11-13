using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Sprite Grass;
    public Sprite Stone;

    public List<Transform> Slot;
    public GameObject SlotItem;
    List<GameObject> items = new List<GameObject>();

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
                case BlockType.Grass:
                    sitem.ItemSetting(Grass, $"{item.Value}");
                    break;
                    //sitem.itemImage = 
                case BlockType.Stone:
                    sitem.ItemSetting(Grass, $"{item.Value}");
                    break;
            }
            idx++;
        }    
    }
}
