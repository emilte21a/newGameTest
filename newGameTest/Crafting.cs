using Raylib_cs;
using System;
using System.Numerics;

public class inventoryEntitySlot{
    public Rectangle inventorySlot;
}

public class CraftingSystem{

const int inventorySlotsX = 7;
const int inventorySlotsY = 3;

const int slotWidth = 20;

int inventorySpotX;
int inventorySpotY;

public static List<inventoryEntitySlot> slots = new();

public static void loadInventory()
{
slots.Clear();
for (var x = 0; x < inventorySlotsX; x++)
{
    for (var y = 0; y < inventorySlotsY; y++)
    {
        slots.Add(new inventoryEntitySlot()
        {
            inventorySlot = new Rectangle(x*slotWidth, y*slotWidth, 20, 20)
        });
    }
}

}
}