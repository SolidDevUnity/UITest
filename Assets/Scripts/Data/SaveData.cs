using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<ItemDataStruct> inventoryItems;
    public List<ItemDataStruct> marketItems;
    public int playerGold;
}
