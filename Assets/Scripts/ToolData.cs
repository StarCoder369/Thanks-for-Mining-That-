using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Scriptable Objects/ToolData")]
public class ToolData : ScriptableObject
{
    public string toolName;

    public string description;

    [System.Serializable]
    public class OreCost
    {
        public OreData ore;
        public int amount;
    }

    public int amountCrafted;

    public float cooldown;

    public List<OreCost> cost;
    public int coinCost;

    public int toolsUnlockedRequired;

    public GameObject toolPrefab;

    public Sprite icon;
}
