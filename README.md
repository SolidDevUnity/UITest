This is a simple code exam I did with the following requirements:

1. Loads a screen that has a UI with some buttons on it and a display that shows a player's gold coins. One button opens up an inventory screen. The other one opens up a market screen.

2. Inventory screen is a screen that has a list of some items on the left hand side that you can select from a player's inventory. On the right hand side of this screen is a preview area that shows the item that is selected with some random info on it and another button called "Preview". This button will open a more detailed window for the item, let's call this screen the itemPreviewScreen.

3. The item preview screen has even more info on this item and has two buttons on it.
- One button is "Put up for sale" that opens up a new window that has a field where you can place a price you wish to sell it on the market for and a button that says "Sell" which actually removes it from your inventory and places it on the market.
- The other button is "Delete" which upon pressing you get a new confirmation window asking "Are you sure you wish to delete this item?" and  "Yes" and "No" buttons which actually removes the item from your inventory or cancels the deletion respectively.

4. The market screen is a screen that has items for sale which you can buy for gold coins.( Place random items here with various prices). Each item in this list has a "Buy" and "About" button.
-"Buy" will remove it from the market and add it to your inventory (if you have enough money) and subtract the necessary amount of gold coins from your coffers.
-"About" will open an item preview screen similar to the one at 3. but with no buttons.
- The market screen also has a button that says "Open Inventory" that opens up the inventory mentioned at 2. over it.

5. Only use Unity native features. No premade assets from the assetstore.
