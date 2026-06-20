using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Scriptable Objects/ToolData")]
public class ToolData : ScriptableObject
{
    public string toolName;

    [System.Serializable]
    public class OreCost
    {
        public OreData ore;
        public int amount;
    }

    public int amountCrafted;

    public List<OreCost> cost;

    public GameObject toolPrefab;

    public Sprite icon;
}
