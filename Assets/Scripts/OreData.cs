using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "OreData", menuName = "Scriptable Objects/Ore")]
public class OreData : ScriptableObject
{
    public string oreName;
    public Sprite oreIcon;
    public float oreDurability;
}
