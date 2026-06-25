using System.Collections.Generic;
using UnityEngine;

public class ShopPanelManager : MonoBehaviour
{
    public List<ShopPanel> shopPanels;

    public int selectedIndex;

    void Start()
    {
        shopPanels[selectedIndex].UpdateInfoPanel();
    }

    public void ButtonClick()
    {
        shopPanels[selectedIndex].UpdateInfoPanel();
        shopPanels[selectedIndex].ButtonClicked();
    }

    public void Select(int index)
    {
        selectedIndex = index;
        shopPanels[selectedIndex].UpdateInfoPanel();
    }
}
