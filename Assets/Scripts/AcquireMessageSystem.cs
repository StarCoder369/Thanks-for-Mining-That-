using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class AcquireMessageSystem : MonoBehaviour
{
    public GameObject acquiredPanel;
    public List<AcquiredPanel> instantiatedPanels;

    public float totalTimeToDisableMessage;
    public void ItemMessage(OreData ore, int amount)
    {
        foreach (AcquiredPanel panel in instantiatedPanels)
        {
            if (panel.ore == ore && panel.gameObject.activeSelf)
            {
                panel.timeToDisable = totalTimeToDisableMessage;
                panel.amount += amount;
                panel.transform.SetAsFirstSibling();
                panel.UpdateFields();
                return;
            }
        }

        GameObject instantiatedPanel = Instantiate(acquiredPanel, transform);
        instantiatedPanel.GetComponent<AcquiredPanel>().ore = ore;
        instantiatedPanel.GetComponent<AcquiredPanel>().amount = amount;
        instantiatedPanel.GetComponent<AcquiredPanel>().timeToDisable = totalTimeToDisableMessage;
        instantiatedPanels.Add(instantiatedPanel.GetComponent<AcquiredPanel>());
        instantiatedPanel.transform.SetAsFirstSibling();
        instantiatedPanel.GetComponent<AcquiredPanel>().UpdateFields();
    }

    public void DisablePanels()
    {
        foreach (AcquiredPanel panel in instantiatedPanels)
        {
            panel.timeToDisable = 0f;
        }
    }
}
