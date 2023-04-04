using System;
using Raylib_cs;

//Item blueprint
public class InventoryItem
{
    public bool stackable;
    public string name;
    public bool craftable;
    public string Texture;
    public int stacks;
}

//Inherit
public class wood : InventoryItem
{
    //Construct
    public wood()
    {
        name = "wood";
        stackable = true;
        craftable = true;
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

public class inventory
{
    
    public Dictionary<string, InventoryItem> ItemsInInventory = new Dictionary<string, InventoryItem>();
    public Dictionary<int, string> InventorySlots = new Dictionary<int, string>();

    int InventoryLength = 4+7;
    

    public inventory()
    {
        for (int i = 0; i < InventoryLength; i++)
        {
            InventorySlots.Add(i, "Empty");
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
            if (Amount != 1)
            {
                Itemdata.stacks+=Amount;
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
}




