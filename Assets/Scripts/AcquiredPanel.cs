using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AcquiredPanel : MonoBehaviour
{
    public OreData ore;
    public int amount;

    public TMP_Text oreNameTxt;
    public TMP_Text oreAmountTxt;
    public Image oreImage;

    public float timeToDisable;

    void Update()
    {
        if (timeToDisable <= 0)
        {
            gameObject.SetActive(false);
        }
        timeToDisable -= Time.deltaTime;
    }

    public void UpdateFields()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = ore.oreName;
        transform.GetChild(1).GetComponent<TMP_Text>().text = amount.ToString();
        if (ore.oreIcon != null)
        {
            transform.GetChild(2).GetComponent<Image>().sprite = ore.oreIcon;
        }
    }
}

