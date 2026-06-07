using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class AcquireMessageSystem : MonoBehaviour
{
    public GameObject acquiredPanel;
    public void ItemMessage(OreData ore, int amount)
    {
        GameObject instantiatedPanel = Instantiate(acquiredPanel, transform);
        instantiatedPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = ore.oreName;
        instantiatedPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = amount.ToString();
        if (ore.oreIcon != null)
        {
            instantiatedPanel.transform.GetChild(2).GetComponent<Image>().sprite = ore.oreIcon;
        }
    }
}
