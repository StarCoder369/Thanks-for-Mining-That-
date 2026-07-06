using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerGameInventory : MonoBehaviour
{
    //Probably should have used lists for all of these, but its too late now
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;

    public ToolData tool1;
    public ToolData tool2;
    public ToolData tool3;
    public ToolData tool4;

    public float tool1Amount;
    public float tool2Amount;
    public float tool3Amount;
    public float tool4Amount;

    public float[] cooldowns;

    public TMP_Text[] toolNames;

    public float maxCooldown;
    public float scrollThreshold = 120f;

    public Transform shootPoint;

    [HideInInspector] public int selectedSlot;

    public float cooldown;

    private float scrollAccumulator;

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            selectedSlot = 0;
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            selectedSlot = 1;
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            selectedSlot = 2;
        }

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            selectedSlot = 3;
        }

        scrollAccumulator += Mouse.current.scroll.y.ReadValue();

        if (scrollAccumulator >= scrollThreshold)
        {
            selectedSlot = (selectedSlot + 1) % 4;
            scrollAccumulator = 0f;
        }
        else if (scrollAccumulator <= -scrollThreshold)
        {
            selectedSlot = (selectedSlot - 1 + 4) % 4;
            scrollAccumulator = 0f;
        }

        if ((Keyboard.current.spaceKey.wasReleasedThisFrame || Mouse.current.leftButton.wasReleasedThisFrame) && GameManager.Instance.craftingPanel.activeSelf == false && Time.timeScale != 0)
        {
            StartShoot();
        }

        for (int i = 0; i < cooldowns.Length; i++)
        {
            if (cooldowns[i] > 0)
            {
                cooldowns[i] -= Time.deltaTime;
            }
        }

        UpdateAlpha();
    }

    void Start()
    {
        UpdateIcons();
        cooldown = maxCooldown;
    }

    public void UpdateAlpha()
    {
        if (selectedSlot == 0)
        {
            SetAlpha(slot1, 1);
            SetAlpha(slot2, 0.4f);
            SetAlpha(slot3, 0.4f);
            SetAlpha(slot4, 0.4f);
        }
        else if (selectedSlot == 1)
        {
            SetAlpha(slot1, 0.4f);
            SetAlpha(slot2, 1f);
            SetAlpha(slot3, 0.4f);
            SetAlpha(slot4, 0.4f);
        }
        else if (selectedSlot == 2)
        {
            SetAlpha(slot1, 0.4f);
            SetAlpha(slot2, 0.4f);
            SetAlpha(slot3, 1f);
            SetAlpha(slot4, 0.4f);
        }
        else if (selectedSlot == 3)
        {
            SetAlpha(slot1, 0.4f);
            SetAlpha(slot2, 0.4f);
            SetAlpha(slot3, 0.4f);
            SetAlpha(slot4, 1f);
        }
    }

    public void SetAlpha(Image image, float alpha)
    {
        Color tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
    }

    public void AddTool(int index)
    {
        if (index == 0)
        {
            tool1Amount += tool1.amountCrafted;
        }
        else if (index == 1)
        {
            tool2Amount += tool2.amountCrafted;
        }
        else if (index == 2)
        {
            tool3Amount += tool3.amountCrafted;
        }
        else if (index == 3)
        {
            tool4Amount += tool4.amountCrafted;
        }

        UpdateIcons();
    }

    public void UpdateIcons()
    {
        if (tool1 != null)
        {
            slot1.sprite = tool1.icon;
            slot1.transform.GetChild(0).GetComponent<TMP_Text>().text = $"x{tool1Amount}";
            toolNames[0].text = tool1.toolName;
        }
        else
        {
            slot2.sprite = null;
            slot2.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            toolNames[0].text = "";
        }

        if (tool2 != null)
        {
            slot2.sprite = tool2.icon;
            slot2.transform.GetChild(0).GetComponent<TMP_Text>().text = $"x{tool2Amount}";
            toolNames[1].text = tool1.toolName;
        }
        else
        {
            slot2.sprite = null;
            slot2.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            toolNames[1].text = "";
        }

        if (tool3 != null)
        {
            slot3.sprite = tool3.icon;
            slot3.transform.GetChild(0).GetComponent<TMP_Text>().text = $"x{tool3Amount}";
            toolNames[2].text = tool1.toolName;
        }
        else
        {
            slot3.sprite = null;
            slot3.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            toolNames[2].text = "";
        }

        if (tool4 != null)
        {
            slot4.sprite = tool4.icon;
            slot4.transform.GetChild(0).GetComponent<TMP_Text>().text = $"x{tool4Amount}";
            toolNames[3].text = tool1.toolName;
        }
        else
        {
            slot4.sprite = null;
            slot4.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
            toolNames[3].text = "";
        }
    }

    public void StartShoot()
    {
        if (cooldowns[selectedSlot] > 0)
        {
            return;
        }
        Debug.Log("Started shoot");
        GetComponent<Animator>().Play("Shoot");
    }

    public void Shoot()
    {
        GameObject instantiatedModule = null;
        switch (selectedSlot)
        {
            case 0:
                if (tool1 == null)
                {
                    break;
                }
                if (tool1Amount > 0)
                {
                    instantiatedModule = Instantiate(tool1.toolPrefab, shootPoint.position, shootPoint.rotation);
                    tool1Amount -= 1;
                    cooldowns[0] = tool1.cooldown;
                }
                break;
            case 1:
                if (tool2 == null)
                {
                    break;
                }
                if (tool2Amount > 0)
                {
                    instantiatedModule = Instantiate(tool2.toolPrefab, shootPoint.position, shootPoint.rotation);
                    tool2Amount -= 1;
                    cooldowns[1] = tool2.cooldown;
                }
                break;
            case 2:
                if (tool3 == null)
                {
                    break;
                }
                if (tool3Amount > 0)
                {
                    instantiatedModule = Instantiate(tool3.toolPrefab, shootPoint.position, shootPoint.rotation);
                    tool3Amount -= 1;
                    cooldowns[2] = tool3.cooldown;
                }
                break;
            case 3:
                if (tool4 == null)
                {
                    break;
                }
                if (tool4Amount > 0)
                {
                    instantiatedModule = Instantiate(tool4.toolPrefab, shootPoint.position, shootPoint.rotation);
                    tool4Amount -= 1;
                    cooldowns[3] = tool4.cooldown;
                }
                break;
        }

        UpdateIcons();

        if (instantiatedModule != null)
        {
            instantiatedModule.GetComponent<Rigidbody2D>().AddForce(shootPoint.transform.right * 200, ForceMode2D.Impulse);
            StatsManager.Instance.totalToolUsage++;
        }
    }
}
