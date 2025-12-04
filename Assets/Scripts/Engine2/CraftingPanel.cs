using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingPanel : MonoBehaviour
{
    public Inventory inventory;
    public List<CraftingRecipe> recipeList;
    public GameObject root;
    public TMP_Text plannedText;
    public Button craftButton;
    public Button clearButton;
    public TMP_Text hintText;

    readonly Dictionary<ItemType, int> planned = new();

    bool isOpen;

    void Start()
    {
        SetOpen(false);
        craftButton.onClick.AddListener(DoCraft);
        clearButton.onClick.AddListener(ClearPlanned);
        RefreshPlannedUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SetOpen(!isOpen);
    }

    public void SetOpen(bool open)
    {
        isOpen = open;
        if(root)
            root.SetActive(open);

        if (!open)
            ClearPlanned();
    }

    public void AddPlanned(ItemType type, int count = 1)
    {
        if (!planned.ContainsKey(type))
            planned[type] = 0;
        planned[type] += count;

        RefreshPlannedUI();
        SetHint($"{type} x{count} Added");
    }

    public void ClearPlanned()
    {
        planned.Clear();
        RefreshPlannedUI();
        SetHint("Cleared");
    }

    void RefreshPlannedUI()
    {
        if (!plannedText)
            return;

        if(planned.Count == 0)
        {
            plannedText.text = "Right Click to add Ingrediant.";
            return;
        }

        var sb = new StringBuilder();

        foreach (var item in planned)
            sb.AppendLine($"{item.Key} x{item.Value}");

        plannedText.text = sb.ToString();
    }

    void SetHint(string msg)
    {
        if(hintText)
            hintText.text = msg;
    }

    void DoCraft()
    {
        if(planned.Count == 0)
        {
            SetHint("Need more Ingrediants");
            return;
        }

        foreach(var plannedItem in planned)
        {
            if(inventory.GetCount(plannedItem.Key) < plannedItem.Value)
            {
                SetHint($"Need more {plannedItem.Key}.");
                return;
            }
        }

        var matchedProduct = FindMatch(planned);
        if(matchedProduct == null)
        {
            SetHint("This Recipe doesn't exsist.");
            return;
        }

        foreach (var itemforConsume in planned)
            inventory.Consume(itemforConsume.Key, itemforConsume.Value);

        foreach (var p in matchedProduct.outputs)
            inventory.Add(p.itemType, p.count);

        ClearPlanned();

        SetHint($"Craft Complete : {matchedProduct.displayName}");
    }

    CraftingRecipe FindMatch(Dictionary<ItemType, int> planned)
    {
        foreach(var recipe in recipeList)
        {
            bool ok = true;
            foreach(var ing in recipe.inputs)
            {
                if (!planned.TryGetValue(ing.itemType, out int have) || have != ing.count)
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
                return recipe;
        }
        return null;
    }
}
