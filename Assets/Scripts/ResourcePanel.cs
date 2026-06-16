using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePanel : MonoBehaviour
{
    public OreData ore;
    public int amount;

    public TMP_Text amountTxt;
    public Image img;

    void Update()
    {
        amountTxt.text = $"x{amount}";
        if (img.sprite != ore.oreIcon)
        {
            img.sprite = ore.oreIcon;
        }
    }
}
