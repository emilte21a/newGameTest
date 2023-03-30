using Raylib_cs;
using System;
using System.Numerics;

public class inventoryEntitySlot
{
    public Rectangle inventorySlot;
}
public class InventorySystem
{

    static int inventorySlotsX = 3;
    static int inventorySlotsY = 2;

    int itemCount;
    public const int slotWidth = 100;

    bool isfull = false;

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
                    inventorySlot = new Rectangle(500+3*x+x * slotWidth, 500+y * slotWidth, slotWidth, slotWidth)
                });
                   
            }
        }

    }

int [,] array = {
{0, 0, 0}, 
{0, 0, 0},
};





}