using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// if you're reading this i probably forgot to use a reusable scrollrect instead that is alot more performant
public class ScreenListView : ScreenView
{
    [SerializeField]
    protected RectTransform itemSpawnPoint;
    [SerializeField]
    protected ItemController itemPrefab;

    protected List<int> spawnedItemsID = new List<int>();
    protected GridLayoutGroup contentGrid;

    protected override void Awake()
    {
        base.Awake();
        contentGrid = itemSpawnPoint.GetComponent<GridLayoutGroup>();
    }

    protected virtual void UpdateItemList(List<ItemRuntime> items)
    {
        var gridSize = itemSpawnPoint.sizeDelta;
        var cellSize = contentGrid.cellSize;
        int maxHorizontal = Mathf.FloorToInt(gridSize.x / cellSize.x);
        int maxVertical = Mathf.FloorToInt(gridSize.y / cellSize.y);
        var maxCellsToDisplay = new Vector2(maxHorizontal, maxVertical);
        int itemCount = items.Count;

        float contentHeight = Mathf.CeilToInt(itemCount / maxHorizontal) * cellSize.y;
        itemSpawnPoint.sizeDelta = new Vector2(gridSize.x, contentHeight);
    }

    void GetColumnAndRow(GridLayoutGroup glg, out int column, out int row)
    {
        column = 0;
        row = 0;

        if (glg.transform.childCount == 0)
            return;

        //Column and row are now 1
        column = 1;
        row = 1;

        //Get the first child GameObject of the GridLayoutGroup
        RectTransform firstChildObj = glg.transform.
            GetChild(0).GetComponent<RectTransform>();

        Vector2 firstChildPos = firstChildObj.anchoredPosition;
        bool stopCountingRow = false;

        //Loop through the rest of the child object
        for (int i = 1; i < glg.transform.childCount; i++)
        {
            //Get the next child
            RectTransform currentChildObj = glg.transform.
           GetChild(i).GetComponent<RectTransform>();

            Vector2 currentChildPos = currentChildObj.anchoredPosition;

            //if first child.x == otherchild.x, it is a column, ele it's a row
            if (firstChildPos.x == currentChildPos.x)
            {
                column++;
                //Stop couting row once we find column
                stopCountingRow = true;
            }
            else
            {
                if (!stopCountingRow)
                    row++;
            }
        }
    }

    protected virtual void RefreshItemList(List<ItemRuntime> items, Action<ItemRuntime, RectTransform, ItemController> OnSpawnItemAction)
    {
        // spawn missing items
        var itemsToSpawn = items.Where(i => !spawnedItemsID.Any(siid => siid == i.itemDataStruct.itemID)).ToList();
        foreach (var item in itemsToSpawn)
        {
            OnSpawnItemAction?.Invoke(item, itemSpawnPoint, itemPrefab);
            spawnedItemsID.Add(item.itemDataStruct.itemID);
        }

        // remove excess items
        var itemsToDestroyID = spawnedItemsID.Where(siid => !items.Any(i => i.itemDataStruct.itemID == siid)).ToList();
        foreach (Transform item in itemSpawnPoint)
        {
            int alreadySpawnedItemID = item.GetComponent<ItemController>().itemRuntime.itemDataStruct.itemID;
            bool itemIsToBeDestroyed = itemsToDestroyID.Any(its => its == alreadySpawnedItemID);
            if (itemIsToBeDestroyed)
            {
                Destroy(item.gameObject);
                spawnedItemsID.Remove(alreadySpawnedItemID);
            }
        }
    }
}
