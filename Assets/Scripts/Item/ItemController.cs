using UnityEngine;

public class ItemController : MonoBehaviour
{
    private ItemRuntime itemRuntime;

    public void Initialize(ItemRuntime itemRuntime)
    {
        this.itemRuntime = itemRuntime;
    }
}
