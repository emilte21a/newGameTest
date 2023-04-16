using Raylib_cs;
using System;
using System.Numerics;
using System.Collections.Generic;
public class inventoryEntitySlot
{
    public Rectangle inventorySlot;
}
public class InventorySystem
{

    static int inventorySlotsX = 3;
    static int inventorySlotsY = 2; 
    public const int slotWidth = 100;
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
                    inventorySlot = new Rectangle(462+68*x+x * slotWidth, 342+68*y+y * slotWidth, slotWidth, slotWidth)

                });
                   
            }
        }

    }
}
