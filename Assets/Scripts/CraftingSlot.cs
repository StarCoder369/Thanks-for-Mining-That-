using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CraftingSlot : MonoBehaviour
{
    public OreData data;
    public int amountNeeded;

    public void UpdateFields()
    {
        GetComponent<Image>().sprite = data.oreIcon;
        transform.GetChild(0).GetComponent<TMP_Text>().text = $"x{amountNeeded}";
    }
}
