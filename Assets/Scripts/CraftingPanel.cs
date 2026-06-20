using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CraftingPanel : MonoBehaviour
{
    public PlayerGameInventory inventory;

    public int slotNum;

    public Image[] toolCraftResources;
    public Image output;
    public TMP_Text outputAmount;

    public List<ToolData.OreCost> cost;

    public ToolData tool;

    void Start()
    {
        UpdateImages();
    }

    public void UpdateCost()
    {
        switch (slotNum)
        {
            case 1:
                cost = inventory.tool1.cost;
                tool = inventory.tool1;
                break;
            case 2:
                cost = inventory.tool2.cost;
                tool = inventory.tool2;
                break;
            case 3:
                cost = inventory.tool3.cost;
                tool = inventory.tool3;
                break;
            case 4:
                cost = inventory.tool4.cost;
                tool = inventory.tool4;
                break;
        }
    }
    public void UpdateImages()
    {
        UpdateCost();
        foreach (Image img in toolCraftResources)
        {
            img.gameObject.SetActive(false);
        }

        int index = 0;

        foreach (ToolData.OreCost item in cost)
        {
            toolCraftResources[index].gameObject.SetActive(true);
            toolCraftResources[index].GetComponent<CraftingSlot>().data = item.ore;
            toolCraftResources[index].GetComponent<CraftingSlot>().amountNeeded = item.amount;
            toolCraftResources[index].GetComponent<CraftingSlot>().UpdateFields();
            index += 1;
        }

        output.sprite = tool.icon;
        outputAmount.text = $"x{tool.amountCrafted}";
    }

    public void TryCraft()
    {
        foreach (Image item in toolCraftResources)
        {
            if (GameManager.Instance.ContainsResource(item.GetComponent<CraftingSlot>().data) >= item.GetComponent<CraftingSlot>().amountNeeded)
            {
                //Nothing really
            }
            else
            {
                return;
            }
        }

        Craft();
    }

    public void Craft()
    {
        int index = slotNum - 1;

        foreach (ToolData.OreCost item in cost)
        {
            Debug.Log("Removed Item");
            GameManager.Instance.RemoveItem(item.ore, item.amount);
        }

        inventory.AddTool(index);
    }
}
