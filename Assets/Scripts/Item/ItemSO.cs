using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    public Sprite icon;
    public string description;

    // gets itemName from ScriptableObject name
    // adds space to CamelCaseNames -> Camel Case Names
    public string displayName => Regex.Replace(name, "(\\B[A-Z0-9])", " $1");
}
