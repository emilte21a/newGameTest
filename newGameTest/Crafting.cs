using Raylib_cs;
using System;
using System.Numerics;
using System.Collections.Generic;

public class CraftingSystem
{

    inventory inventory = new();
    InventoryItem invitem = new();
    TextureManager textureManager = new();

    wood wood = new();
    stone stone = new();
    stick stick = new();
    woodPickaxe woodPickaxe = new();
    stoneAxe stoneAxe = new();

    public void craftingSyst()
    {

        //foreach (var item in inventory.InventorySlots)
        //{

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                Variable.whichItem--;
                if (Variable.whichItem < 0)
                {
                    Variable.whichItem = 2;
                }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                Variable.whichItem++;
                if (Variable.whichItem > 2)
                {
                    Variable.whichItem = 0;
                }
            }

            if (Variable.whichItem == 0)
            {
                Raylib.DrawTexture(textureManager.LoadTexture("IMG/stickTexture.png"), 180, 400, Color.WHITE);
                Raylib.DrawText("Wood: 1", 700, 320, 20, Color.WHITE);
            }

            if (Variable.whichItem == 1)
            {
                Raylib.DrawTexture(textureManager.LoadTexture("IMG/woodenPickaxeTexture.png"), 180, 400, Color.WHITE);
                Raylib.DrawText("Stick: 2", 700, 320, 20, Color.WHITE);
                Raylib.DrawText("Wood: 3", 700, 340, 20, Color.WHITE);
            }

            if (Variable.whichItem == 2)
            {
                Raylib.DrawTexture(textureManager.LoadTexture("IMG/stoneAxeTexture.png"), 180, 400, Color.WHITE);
                Raylib.DrawText("Stick: 2", 700, 320, 20, Color.WHITE);
                Raylib.DrawText("Stone: 3", 700, 340, 20, Color.WHITE);
            }
        //}

        Raylib.DrawText("Press left arrow for previous item", 470, 750, 20, Color.WHITE);
        Raylib.DrawText("Press right arrow for next item", 484, 800, 20, Color.WHITE);
        Raylib.DrawText("Item:", 180, 300, 20, Color.SKYBLUE);
        Raylib.DrawText("Amount crafted:", 180, 340, 20, Color.BLUE);
        Raylib.DrawText("Ingredients needed:", 700, 300, 20, Color.RED);
        Raylib.DrawText("Ingredients in storage:", 700, 400, 20, Color.SKYBLUE);

        Raylib.DrawText("Press SPACE to craft:", 451, 701, 30, Color.BLACK);
        Raylib.DrawText("Press SPACE to craft:", 450, 700, 30, Color.ORANGE);
    }
}
