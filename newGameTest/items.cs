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
    }
}
public class stoneAxe2 : InventoryItem
{
    public stoneAxe2()
    {
        name = "stoneAxe2";
        stackable = false;
        craftable = true;
        Texture = "IMG/stoneAxeTexture.png";
    }
}

public class inventory
{
    
    /*
    Dictionary med key: string och value: IntentoryItem. 
    Detta gör det möjligt att "kalla" på valutan som är InventoryItem med en string.
    */
    public Dictionary<string, InventoryItem> ItemsInInventory = new Dictionary<string, InventoryItem>();
    
    /*
    En dictionary med key: int och value: string
    Den gör det möjligt att lägga till en ny "empty" string för varje inventoryslot
    */
    public Dictionary<int, string> InventorySlots = new Dictionary<int, string>();

    //Hotbaren är 4 stycken slots
    //När man trycker på tab så får man 6 stycken extra slots
    int InventoryLength = 4+6;

    //En struct av inventory    
    public inventory()
    {
        for (int invSlot = 0; invSlot < InventoryLength; invSlot++)
        {
            InventorySlots.Add(invSlot, "Empty");
        }
    //För varje int invSlot i InventoryLength
    //Lägg till en ny tom inventoryslot
    
    }
    

    //Funktion för att lägga till nytt item i inventoryt
    public void addToInventory(string item, InventoryItem Itemdata, int Amount)
    {
        
        if (ItemsInInventory.ContainsKey(item))
        {
            if (Itemdata.stackable == true)
            {
                Itemdata.stacks += Amount;
            }
        }
        //Om dictionaryn ItemsInInventory redan innehåller nyckeln item
        //OCH Om det föremålet får stackas
        //Lägg till i stacks med amount vilket anges när man kallar på funktionen

        else
        {
            int UsableSlot = findFirstEmptySlot();
            InventorySlots.Remove(UsableSlot);
            InventorySlots.Add(UsableSlot, Itemdata.name);
            ItemsInInventory.Add(Itemdata.name, Itemdata);
            if (Amount != 0)
            {
                Itemdata.stacks+=Amount;
            }
        }


        //Annars så är usableSlot funktionen findFirstEmptySlot som ser vilken inventoryslot som är ledig
        //Om itemet redan finns i inventoryt, så ta bort en tom usableSlot
        //Och lägg istället till ett nytt item i samma usableSlot
        //Lägg till itemet i inventoryt och dess data
        //Om amount inte är 0
        //Lägg till amount till stacks om det får stackas
    
    }
    public void removeFromInventory(string item, InventoryItem Itemdata, int Amount){
        
        if (ItemsInInventory.ContainsKey(item))
        {
            if (Itemdata.stackable == true)
            {
                Itemdata.stacks -= Amount;
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

    public string activeItem(string currentActiveItem){
        
        for (int i = 0; i < InventoryLength; i++)
        {
            
        }

        return currentActiveItem;
    }
    
//För varje integer I som är mindre än InventoryLength
//Om inventorySloten I är tom, så returna I
//Fortsätt att kolla vilka inventoryslots som är lediga tills funktionen körts klart
}




