using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public ShopManager shop;
    public ToolData tool;
    public GameObject infoPanel;
    public Image iconImg;

    public Image infoImg;
    public TMP_Text infoName;
    public TMP_Text infoDescription;
    public TMP_Text infoCost;
    public Button infoBtn;
    public TMP_Text infoBtnTxt;
    public TMP_Text requiresAllToolsTxt;

    public bool unlocked = false;
    public bool equipped = false;

    public int index;

    public void LoadValues()
    {
        unlocked = StatsManager.Instance.IsToolUnlocked(index);
        equipped = StatsManager.Instance.IsToolEquipped(index);

        if (equipped)
        {
            for (int i = 0; i < shop.toolAvailable.Length; i++)
            {
                if (shop.toolAvailable[i])
                {
                    equipped = true;
                    shop.tools[i] = tool;
                    shop.equippedPanels[i] = GetComponent<ShopPanel>();
                    shop.UpdateFields();
                    infoBtn.interactable = false;
                    UpdateInfoPanel();
                    break;
                }
            }
        }

        UpdateInfoPanel();
    }

    void Update()
    {
        if (tool == null)
        {
            return;
        }
        iconImg.sprite = tool.icon;
    }

    public void UpdateInfoPanel()
    {
        infoImg.sprite = tool.icon;
        infoName.text = tool.toolName;
        infoDescription.text = tool.description;
        infoCost.text = unlocked ? "Unlocked" : $"Cost: {tool.coinCost}";
        if (unlocked)
        {
            if (equipped)
            {
                infoBtnTxt.text = "Equipped";
                infoBtn.interactable = false;
            }
            else
            {
                infoBtnTxt.text = "Equip";
                infoBtn.interactable = true;
            }
        }
        else
        {
            infoBtnTxt.text = "Unlock";
            infoBtn.interactable = true;
            if (requiresAllToolsTxt != null)
            {
                if (tool.toolsUnlockedRequired > 0)
                {
                    requiresAllToolsTxt.gameObject.SetActive(true);
                }
                else
                {
                    requiresAllToolsTxt.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ButtonClicked()
    {
        if (tool == null)
        {
            UpdateInfoPanel();
            return;
        }

        if (unlocked)
        {
            if (equipped)
            {
                //Nothing needs to happens
                infoBtn.interactable = false;
            }
            else
            {
                infoBtn.interactable = true;

                for (int i = 0; i < shop.toolAvailable.Length; i++)
                {
                    if (shop.toolAvailable[i])
                    {
                        equipped = true;
                        shop.tools[i] = tool;
                        shop.equippedPanels[i] = GetComponent<ShopPanel>();
                        shop.UpdateFields();
                        infoBtn.interactable = false;
                        UpdateInfoPanel();
                        StatsManager.Instance.toolEquipped[index] = true;
                        break;
                    }
                }
            }
        }
        else
        {
            if (tool.toolsUnlockedRequired > 0)
            {
                int toolUnlockedCount = 0;
                for (int i = 0; i < StatsManager.Instance.toolUnlocked.Length; i++)
                {
                    if (StatsManager.Instance.toolUnlocked[i] == true)
                    {
                        toolUnlockedCount++;
                    }
                }

                if (toolUnlockedCount < tool.toolsUnlockedRequired)
                {
                    return;
                }
            }
            if (GameManager.Instance.coins >= tool.coinCost)
            {
                GameManager.Instance.coins -= tool.coinCost;
                unlocked = true;
                equipped = false;
            }
            StatsManager.Instance.toolUnlocked[index] = true;
        }
        UpdateInfoPanel();
    }
}
