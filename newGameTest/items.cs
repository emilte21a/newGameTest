using System;
using Raylib_cs;


//Tack till Rickard som hjälpt med detta
/*
En sorts blueprint för varje item
*/
public class InventoryItem
{
    public bool stackable;
    public string name;
    public bool craftable;
    public string Texture;
    public int stacks;

    public int slot;
    public Dictionary<string, int> Recipe;

}

//Varje item inheritar från Inventoryitem
public class wood : InventoryItem
{
    //Struct
    public wood()
    {
        name = "wood";
        stackable = true;
        craftable = false;
        Texture = "IMG/woodTexture.png";
    }
}

public class stone : InventoryItem
{
    public stone()
    {
        name = "stone";
        stackable = true;
        craftable = false;
        Texture = "IMG/rockTexture.png";
    }
}

public class stick : InventoryItem
{
    public stick()
    {
        name = "stick";
        stackable = true;
        craftable = true;
        Texture = "IMG/stickTexture.png";
        Recipe = new Dictionary<string, int> { { "wood", 1 } };
    }
}
public class woodPickaxe : InventoryItem
{

    public woodPickaxe()
    {
        name = "woodPickaxe";
        stackable = false;
        craftable = true;
        Texture = "IMG/woodenPickaxeTexture.png";
        Recipe = new Dictionary<string, int> { { "stick", 2 }, { "wood", 3 } };
    }
}

public class stoneAxe : InventoryItem
{
    public stoneAxe()
    {
        name = "stoneAxe";
        stackable = false;
        craftable = true;
        Texture = "IMG/stoneAxeTexture.png";
        Recipe = new Dictionary<string, int> { { "stick", 2 }, { "stone", 3 } };
    }

}

public class craftingTable : InventoryItem
{
    public craftingTable()
    {
        name = "craftingTable";
        stackable = false;
        craftable = true;
        Texture = "IMG/craftingtableicon.png";
        Recipe = new Dictionary<string, int> { { "wood", 4 } };
    }
}

public class inventory
{

   
    public Dictionary<string, InventoryItem> ItemsInInventory = new Dictionary<string, InventoryItem>();

    
    public Dictionary<int, string> InventorySlots = new Dictionary<int, string>();
    int InventoryLength = 5;

    //En struct av inventory    
    public inventory()
    {
        for (int invSlot = 0; invSlot < InventoryLength; invSlot++)
        {
            InventorySlots.Add(invSlot, "Empty");
        }
    }

    
    public void addToInventory(string item, InventoryItem Itemdata, int Amount)
    {
        if (ItemsInInventory.ContainsKey(item))
        {
            if (Itemdata.stackable == true)
            {
                Itemdata.stacks += Amount;
            }
        }

        else
        {
            int UsableSlot = findFirstEmptySlot();
            InventorySlots.Remove(UsableSlot);
            InventorySlots.Add(UsableSlot, Itemdata.name);
            ItemsInInventory.Add(Itemdata.name, Itemdata);
            Itemdata.slot = UsableSlot;
            if (Amount != 0)
            {
                Itemdata.stacks += Amount;
            }
        }
    }
    public int findFirstEmptySlot()
    {
        for (int i = 0; i < InventoryLength; i++)
        {
            if (InventorySlots[i] == "Empty")
            {
                return i;
            }
            continue;
        }
        return 10;
    }
    int itemIndex = 0;
    public int activeHotbarItem()
    {

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ONE))
        {
            itemIndex = 0;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_TWO))
        {
            itemIndex = 1;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_THREE))
        {
            itemIndex = 2;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_FOUR))
        {
            itemIndex = 3;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_FIVE))
        {
            itemIndex = 4;
        }
        return itemIndex;
    }

    public string activeItem(string currentActiveItem, string item)
    {
        int itemIndex = activeHotbarItem();

        if (InventorySlots[itemIndex] == currentActiveItem)
        {
            if (ItemsInInventory.ContainsKey(item))
            {
                currentActiveItem = InventorySlots[itemIndex];
            }
        }
        return currentActiveItem;
    }

    int active = 1;
    string CurrentActiveItem;
    public void weaponDamageComponent()
    {
        active = activeHotbarItem();
        CurrentActiveItem = activeItem(InventorySlots[active], "Empty");

        switch (CurrentActiveItem)
        {
            case "woodPickaxe":
                Variable.Damage = 20;
                Variable.canBreakWood = false;
                Variable.canBreakStone = true;
                break;
            case "stoneAxe":
                Variable.Damage = 25;
                Variable.canBreakWood = true;
                Variable.canBreakStone = false;
                break;
            default:
                Variable.Damage = 10;
                Variable.canBreakWood = true;
                Variable.canBreakStone = false;
                break;
        }
    }
    //Parametern är av InventoryItem, vilket är en klass som varje item inheritar
    public bool CanCraft(InventoryItem itemdata)
    {
        foreach (KeyValuePair<string, int> ingredient in itemdata.Recipe)
        {
            if (!ItemsInInventory.ContainsKey(ingredient.Key) || ItemsInInventory[ingredient.Key].stacks < ingredient.Value)
            {
                return false;
            }
        }
        return true;
    }

    public void CraftItem(InventoryItem itemdata)
    {
        if (CanCraft(itemdata))
        {
            foreach (KeyValuePair<string, int> ingredient in itemdata.Recipe)
            {
                ItemsInInventory[ingredient.Key].stacks -= ingredient.Value;
                if (ItemsInInventory[ingredient.Key].stacks <= 0 && InventorySlots.ContainsValue(ItemsInInventory[ingredient.Key].name))
                {
                    InventorySlots[ItemsInInventory[ingredient.Key].slot] = "Empty";
                    ItemsInInventory.Remove(ingredient.Key);                    
                    
                }
              
            }
            addToInventory(itemdata.name, itemdata, 1);
        }
        
        
    }
}