using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Image[] toolImages = new Image[4];
    public TMP_Text[] toolNames = new TMP_Text[4];
    public TMP_Text[] toolBtnTexts = new TMP_Text[4];
    public ToolData[] tools = new ToolData[4];
    public ShopPanel[] equippedPanels = new ShopPanel[4];
    public bool[] toolAvailable = new bool[4];

    public PlayerGameInventory inventory;

    void Start()
    {
        UpdateFields();
    }

    public void UpdateFields()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            toolAvailable[i] = tools[i] == null;

            if (toolAvailable[i])
            {
                toolImages[i].sprite = null;
                toolNames[i].text = "None";
                toolBtnTexts[i].text = "None";
                toolBtnTexts[i].transform.parent.GetComponent<Button>().interactable = false;
                continue;
            }

            toolImages[i].sprite = tools[i].icon;
            toolNames[i].text = tools[i].toolName;
            toolBtnTexts[i].text = "Unequip";
            toolBtnTexts[i].transform.parent.GetComponent<Button>().interactable = true;
        }
        inventory.tool1 = tools[0];
        inventory.tool2 = tools[1];
        inventory.tool3 = tools[2];
        inventory.tool4 = tools[3];
    }

    public void RemoveTool(int index)
    {
        tools[index] = null;
        equippedPanels[index].equipped = false;
        equippedPanels[index].UpdateInfoPanel();
        equippedPanels[index] = null;
        UpdateFields();
    }
}