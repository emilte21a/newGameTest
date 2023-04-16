﻿using Raylib_cs;
using System;
using System.Numerics;


Raylib.InitWindow(Variable.screenWidth, Variable.screenHeight, "Road Rider");
Raylib.SetTargetFPS(60);

string[] names = { "mad", "hollo", "wal" };

TextureClass Textures = new();
BlockObject floorCollection = new BlockObject();
TreeObject treeCollection = new TreeObject();
inventory inventoryManager = new inventory();
TextureManager textureManager = new TextureManager();
Player Player = new();
Methods Methods = new();

Rectangle enemyRec = new Rectangle(900, 900, 120, 120);


Enemy e = new Enemy();
Enemy e2 = new Enemy();
Enemy e3 = new Enemy();

List<Enemy> enemies = new();

enemies.Add(new Enemy() { name = "Joseph" });
enemies.Add(new Enemy() { name = "Avdol" });
enemies.Add(new Enemy() { name = "Jean Pierre" });


int skyGreen = 215;
int skyBlue = 255;
int skyRed = 115;

Color skyColor = new Color(skyRed, skyGreen, skyBlue, 255);
/*
System.Timers.Timer timer = new (interval: 1000); 
timer.Elapsed += ( sender, e ) => DayCycle();

void DayCycle(){


}
*/

Color mountainColor = new Color(255, 255, 255, 155);


int charVariable;

foreach (Enemy en in enemies)
{
    Console.WriteLine(en.name);
}

//float enemySpeed = 2;


Camera2D camera = new();
camera.zoom = 0.9f;
camera.rotation = 0;
camera.offset = new Vector2(1180 / 2, Variable.screenWidth / 2);

//1180 = Screenwidth-120 (120 är texturens bredd)

void zoomFunction()
{
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_X))
    {
        camera.zoom += 0.05f;
    }

    if (Raylib.IsKeyPressed(KeyboardKey.KEY_Z))
    {
        camera.zoom -= 0.05f;
    }
}

string currentScene = "start";
wood wood = new();
stone stone = new();
stick stick = new();
woodPickaxe woodPickaxe = new();
stoneAxe stoneAxe = new();

while (!Raylib.WindowShouldClose())
{

    //Logik====================

    Vector2 characterPos = new Vector2(characterProperties.characterRec.x, characterProperties.characterRec.y);
    Vector2 skyPos = new Vector2(-Variable.screenWidth / 2, -100);
    Vector2 mountainPos = new Vector2(-Variable.screenWidth / 2, Variable.screenHeight / 3.5f);
    Vector2 hillsPos = new Vector2(-Variable.screenWidth / 2, Variable.screenHeight / 2);


    camera.target = characterPos; //Kamerans target är karaktärens position

    if (currentScene == "start")
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
            Player.resetVars();
            BlockObject.loadBlocks();
            TreeObject.loadTrees();
            rockObject.loadRocks();
        }

    }

    else if (currentScene == "game")
    {

        Player.GravityPhysics();
        characterProperties.characterRec.x = Player.walkingX(characterProperties.characterRec.x, characterProperties.speed);

        if (characterProperties.characterRec.y > Variable.screenHeight)
        {
            currentScene = "dead";
        }
    }

    else if (currentScene == "dead")
    {
        Player.resetVars();

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "start";
        }
    }



    // Vector2 playerPos = new Vector2(characterRec.x, characterRec.y);
    // Vector2 fiendePos = new Vector2(enemyRec.x, enemyRec.y);
    // Vector2 diff = playerPos - fiendePos;
    // Vector2 fiendeDirection = Vector2.Normalize(diff);

    //Grafik===========================================

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    if (currentScene == "start")
    {

        Raylib.ClearBackground(Color.WHITE);
        Raylib.DrawText("Press ENTER to start", Variable.screenWidth / 2, Variable.screenHeight / 2, 50, Color.GOLD);

    }


    else if (currentScene == "game")
    {

        charVariable = Player.jumpAnim();
        int runningFrame = Player.runningAnimation();
        Player.bothADdown();
        Methods.meleeMethod();
        int punchFrame = Methods.punchReturn();
        int pickaxeFrame = Player.pickaxeAnimation();
        Methods.parallaxEffect();
        zoomFunction();

        //Spring texturens source rektangel
        Rectangle sourceRec1 = new Rectangle(120 * runningFrame, 0, Variable.way * 120, 180);

        //Spelarens textur rektangel med variabeln way på bredden för att rendera om vilket håll gubben är vänd
        Rectangle facing = new Rectangle(0, 0, Variable.way * 120, 180);


        //Bakgrundens texturers source rektanglar
        Rectangle skyRec = new Rectangle(Variable.skyPlacementX / 4, 0, TextureClass.backgroundTextures[3].width, TextureClass.backgroundTextures[3].height);
        Rectangle mountainRec = new Rectangle(Variable.skyPlacementX / 2, 0, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[1].height);
        Rectangle hillsRec = new Rectangle(Variable.skyPlacementX, 0, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[2].height);
        Rectangle punchRec = new Rectangle(120 * punchFrame, 0, Variable.way * 120, 180);
        Rectangle pickaxeRec = new Rectangle(180*pickaxeFrame, 0, Variable.way*180, 180);
        //Slag rektangeln och dess färg
        Color punchColor = new Color(255, 255, 255, Variable.punchColorAlpha);

        //Rita ut bakgrundstexturerna
        //Raylib.DrawTextureRec(TextureClass.backgroundTextures[0], skyRec, skyPos, Color.WHITE);
        Raylib.DrawRectanglePro(
        new Rectangle(0, 0, Variable.screenWidth, Variable.screenHeight),
        new Vector2(0, 0),
        0,
        skyColor);

        Raylib.DrawTextureRec(TextureClass.backgroundTextures[3], skyRec, skyPos, Color.WHITE);
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[1], mountainRec, mountainPos, mountainColor);
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[2], hillsRec, hillsPos, Color.WHITE);


        Raylib.BeginMode2D(camera);

        //Karaktärens hitbox
        characterProperties.hitBox.x = characterProperties.characterRec.x;
        characterProperties.hitBox.y = characterProperties.characterRec.y + 180;



        //För varje block i listan BlockObject.floors så ska gräs texturen ritas ut
        for (var i = 0; i < BlockObject.floors.Count; i++)
        {
            blockEntity floor = BlockObject.floors[i];
            Raylib.DrawTexture(TextureClass.blockTextures[0], (int)floor.cellBlock.x, (int)floor.cellBlock.y, Color.WHITE);
        }

        //Rita ut alla dirtblocks som bara är texturer
        BlockObject.drawDirtBlocks();



        //För varje träd i treeCollection.Trees så ska en textur ritas ut
        for (var i = 0; i < TreeObject.Trees.Count; i++)
        {
            TreeEntity tree = TreeObject.Trees[i];

            if (tree.treeHealth > 0)
            {
                Raylib.DrawTexture(TextureClass.otherTextures[3], (int)tree.TreeRect.x, 420, Color.WHITE);
                /*
                Om man trycker på F och karaktärens rektangel kolliderar med trädets Rektangel
                Starta då breakTreeMethod
                Om trädets HP är detsamma som 0 så ska spelarens mängd trä att öka med 10
                */

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F) && Raylib.CheckCollisionRecs(characterProperties.characterRec, tree.TreeRect) && Variable.punchTimer == 100)
                {
                    tree.breakTreeMethod();
                    if (tree.treeHealth == 0)
                    {
                        inventoryManager.addToInventory("wood", wood, 10);
                        inventoryManager.addToInventory("stone", stone, 10);
                        inventoryManager.addToInventory("stick", stick, 10);
                        inventoryManager.addToInventory("woodPickaxe", woodPickaxe, 1);
                        inventoryManager.addToInventory("stoneAxe", stoneAxe, 1);
                    }
                }
                if (tree.treeHealth < 100)
                {
                    Raylib.DrawText($"{tree.treeHealth}", (int)tree.TreeRect.x + 50, (int)tree.TreeRect.y - 30, 50, Color.RED);
                }
            }
        }


        //Rita ut stenar
        for (var i = 0; i < rockObject.Rocks.Count; i++)
        {
            rockEntity rock = rockObject.Rocks[i];


            Raylib.DrawTexture(TextureClass.rockTextures[rockObject.rockTexture], (int)rock.rockRect.x, (int)rock.rockRect.y, Color.WHITE);

        }




        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && Variable.touchFloor == true && Variable.bothButtonsPressed == false)
        {

            Raylib.DrawTextureRec(TextureClass.charTextures[3], sourceRec1, characterPos, Color.WHITE);
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && Variable.touchFloor == true && Variable.bothButtonsPressed == false)
        {

            Raylib.DrawTextureRec(TextureClass.charTextures[3], sourceRec1, characterPos, Color.WHITE);
        }

        else if (Variable.whilePunching > 0)
        {
            Raylib.DrawTextureRec(TextureClass.charTextures[4], punchRec, characterPos, Color.WHITE);

        }
        
        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_R))
        {
            Raylib.DrawTextureRec(TextureClass.charTextures[5], pickaxeRec, characterPos, Color.WHITE);
        }

        else
        {

            Variable.isMoving = false;
            Raylib.DrawTextureRec(TextureClass.charTextures[charVariable], facing, characterPos, Color.WHITE);
        }

        Raylib.DrawText($"{e.name}", 400, 400, 50, Color.BLACK);
        Raylib.EndMode2D();

        Raylib.DrawTexture(TextureClass.otherTextures[0], 10, -10, Color.WHITE);
        Raylib.DrawTexture(TextureClass.otherTextures[2], 1100, 25, Color.WHITE);
        Raylib.DrawTexture(TextureClass.otherTextures[1], 1110, 35, Color.WHITE);
        Raylib.DrawRectangle(1110, 35, Variable.punchRectWidth, 100, punchColor);

        bool tab = false;
        InventorySystem.loadInventory();
        if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
        {
            tab = true;
            Raylib.DrawTexture(TextureClass.otherTextures[5], 400, 100, Color.WHITE);
        }
        int itemPos = 0;


        foreach (var item in inventoryManager.InventorySlots)
        {

            if (itemPos <= 3)
            {

                if (item.Value != "Empty")
                {
                    InventoryItem item1 = inventoryManager.ItemsInInventory[item.Value];
                    Raylib.DrawTexture(textureManager.LoadTexture(item1.Texture), 50 + 120 * itemPos, 70, Color.WHITE);
                    Raylib.DrawText($"{item1.stacks}", 110 + 120 * itemPos, 130, 20, Color.WHITE);
                }
            }


            //Draw rektangler i inventoryt när man trycker tab
            else if (itemPos > 4 && tab == true)
            {
                int X = (int)InventorySystem.slots[itemPos - 5].inventorySlot.x;
                int Y = (int)InventorySystem.slots[itemPos - 5].inventorySlot.y;

                if (item.Value != "Empty")
                {
                    InventoryItem item1 = inventoryManager.ItemsInInventory[item.Value];
                    Raylib.DrawTexture(textureManager.LoadTexture(item1.Texture), X, Y, Color.WHITE);
                    Raylib.DrawText($"{item1.stacks}", X, Y + 40, 50, Color.WHITE);
                }
            }

            itemPos++;
            if (itemPos >= inventoryManager.InventorySlots.Count())
            {
                itemPos = 0;
            }
        }





    }


    else if (currentScene == "dead")
    {

        Raylib.ClearBackground(Color.WHITE);
        Raylib.DrawText("you died", Variable.screenWidth / 2, Variable.screenHeight / 2, 50, Color.GOLD);
    }

    Raylib.EndDrawing();



}




//Startskärmen

//En rörande bakgrund där man kan välja att spela, ändra inställningar eller lämna


//Spelet

//2D survival-i spel där man kan gå höger, vänster och hoppa
//Man ska kunna skapa tools med en crafting table 
//Kunna hugga träd samt hacka sten och döda monster
//Försök till day-night time cycle


//