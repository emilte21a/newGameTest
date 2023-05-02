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
    int InventoryLength = 5;

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
            Itemdata.slot = UsableSlot;
            if (Amount != 0)
            {
                Itemdata.stacks += Amount;
            }
        }
        //Annars så är usableSlot funktionen findFirstEmptySlot som ser vilken inventoryslot som är ledig
        //Om itemet redan finns i inventoryt, så ta bort en tom usableSlot
        //Och lägg istället till ett nytt item i samma usableSlot
        //Lägg till itemet i inventoryt och dess data
        //Om amount inte är 0
        //Lägg till amount till stacks om det får stackas
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
    //För varje integer I som är mindre än InventoryLength
    //Om inventorySloten I är tom, så returna I
    //Fortsätt att kolla vilka inventoryslots som är lediga tills funktionen körts klart

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
        //Om föremålet på samma plats som itemIndex har i inventoryt
        //Om inventoryt innehåller item 
        //Gör currentActiveItem till den indexet man har.
        //Detta betyder att om du trycker på knapp 1, så är första itemet currentActiveItem
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
        //Varje item har olika egenskaper.
        //en träpickaxe har 20 damage och kan ta sönder sten med inte trä
        //en stenyxa har 25 damage och kan ta sönder trä men inte sten
        //resterande items har 10 damage och kan endast ta sönder trä
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
        //För varje Key & Value par vid namn ingredient som finns i receptet för varje InventoryItem
        //Om inventoryt inte innehåller ingredienten
        //Eller om mängden ingredienser är mindre än "hur mycket" det kostar att skapa det itemet man vill skapa
        //Returnera falsk
        //Annars returnera sant
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
    //Om man kan skapa det item man vill skapa, och det går att stacka
    //För varje Key & Value par ingredient som finns i dictionaryt recipe för varje Inventoryitem
    //Minska de ingredienser som användes vid skapandet med hur mycket som behövdes för receptet
    //Om ingrediensens värde blir lika med 0, så ta bort ingrediensen från inventoryt
    //Lägg till ett av det nya InventoryItem som man skapat

}