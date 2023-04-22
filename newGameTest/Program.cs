using Raylib_cs;
using System;
using System.Numerics;
using System.Collections.Generic;

Raylib.InitWindow(Variable.screenWidth, Variable.screenHeight, "Terraria knockoff");
Raylib.SetTargetFPS(60);

InventoryItem invItem = new();
inventory inventoryManager = new inventory();
TextureManager textureManager = new TextureManager();
Player Player = new();
Methods Methods = new();
BlockObject BlockObject = new();
TreeObject TreeObject = new();
rockObject rockObject = new();
playerAssets playerAssets = new();

//Instanser av varje item
wood wood = new();
stone stone = new();
stick stick = new();
woodPickaxe woodPickaxe = new();
stoneAxe stoneAxe = new();


string currentScene = "start";
Color skyColor = new Color(115, 215, 255, 255);

/*
System.Timers.Timer timer = new (interval: 1000); 
timer.Elapsed += ( sender, e ) => DayCycle();

void DayCycle(){
}
*/

while (!Raylib.WindowShouldClose())
{
    // Vector2 playerPos = new Vector2(characterRec.x, characterRec.y);
    // Vector2 fiendePos = new Vector2(enemyRec.x, enemyRec.y);
    // Vector2 diff = playerPos - fiendePos;
    // Vector2 fiendeDirection = Vector2.Normalize(diff);


    Vector2 characterPos = new Vector2(playerAssets.characterRec.x, playerAssets.characterRec.y);
    Vector2 skyPos = new Vector2(-Variable.screenWidth / 2, -100);
    Vector2 mountainPos = new Vector2(-Variable.screenWidth / 2, Variable.screenHeight / 3.5f);
    Vector2 hillsPos = new Vector2(-Variable.screenWidth / 2, Variable.screenHeight / 2);
    

    Camera2D camera = new();
    camera.zoom = 0.9f;
    camera.rotation = 0;
    camera.offset = new Vector2(1180 / 2, Variable.screenWidth / 2);
    //1180 = Screenwidth-120 (120 är texturens bredd)

    camera.target = characterPos; //Kamerans target är karaktärens position

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    if (currentScene == "start")
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
            Player.resetVars();
            BlockObject.loadBlocks();
            TreeObject.LoadTrees();
            rockObject.loadRocks();
              inventoryManager.addToInventory("woodPickaxe", woodPickaxe, 1);
        }

        Raylib.ClearBackground(Color.WHITE);
        Raylib.DrawText("Press ENTER to start", Variable.screenWidth / 2, Variable.screenHeight / 2, 50, Color.GOLD);
    }
    else if (currentScene == "game")
    {
        Player.GravityPhysics();
        playerAssets.characterRec.x = Player.walkingX(playerAssets.characterRec.x, playerAssets.speed);

        Player.bothADdown();
        Methods.meleeMethod();
        Methods.parallaxEffect();
        Methods.punchReturn();
        inventoryManager.weaponDamageComponent();

        string currentActiveItem = "Empty";
        int activeItem = 1;
        int punchFrame = Variable.punchFrame;
        int runningFrame = Player.runningAnimation();
        int pickaxeFrame = Variable.pickaxeFrame;
        int charVariable = Player.jumpAnimation();


        //Bakgrundens texturers source rektanglar
        Rectangle skyRec = new Rectangle(Variable.skyPlacementX / 4, 0, TextureClass.backgroundTextures[3].width, TextureClass.backgroundTextures[3].height);
        Rectangle mountainRec = new Rectangle(Variable.skyPlacementX / 2, 0, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[1].height);
        Rectangle hillsRec = new Rectangle(Variable.skyPlacementX, 0, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[2].height);

        //Spring texturens source rektangel
        Rectangle sourceRec1 = new Rectangle(120 * runningFrame, 0, Variable.FacingDirection * 120, 180);

        //Spelarens textur rektangel med variabeln way på bredden för att rendera om vilket håll gubben är vänd
        Rectangle facing = new Rectangle(0, 0, Variable.FacingDirection * 120, 180);

        //Source rektangeln för pickaxe animationen
        Rectangle pickaxeRec = new Rectangle(180 * pickaxeFrame, 0, Variable.FacingDirection * 180, 180);

        //Source rektangeln för slå animationen 
        Rectangle punchRec = new Rectangle(120 * punchFrame, 0, Variable.FacingDirection * 120, 180);

        //Slag rektangeln och dess färg
        Color punchColor = new Color(255, 255, 255, Variable.punchColorAlpha);
        Color mountainColor = new Color(255, 255, 255, 155);

        //Raylib.DrawTextureRec(TextureClass.backgroundTextures[0], skyRec, skyPos, Color.WHITE);
        Raylib.DrawRectanglePro(
        new Rectangle(0, 0, Variable.screenWidth, Variable.screenHeight),
        new Vector2(0, 0),
        0,
        skyColor);

        //Rita ut bakgrundstexturerna
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[3], skyRec, skyPos, Color.WHITE);
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[1], mountainRec, mountainPos, mountainColor);
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[2], hillsRec, hillsPos, Color.WHITE);

        Raylib.BeginMode2D(camera);

        //Karaktärens hitbox
        playerAssets.hitBox.x = playerAssets.characterRec.x;
        playerAssets.hitBox.y = playerAssets.characterRec.y + 180;


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
                Raylib.DrawTexture(TextureClass.otherTextures[3], (int)tree.TreeRect.x - 90, 420, Color.WHITE);
                /*
                Om man trycker på F och karaktärens rektangel kolliderar med trädets Rektangel
                Starta då breakTreeMethod
                Om trädets HP är detsamma som 0 så ska spelarens mängd trä att öka med 10
                */

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F) && Raylib.CheckCollisionRecs(playerAssets.characterRec, tree.TreeRect) && Variable.punchTimer == 100 && Variable.canBreakWood == true)
                {
                    tree.breakTreeMethod();
                    if (tree.treeHealth == 0)
                    {
                        inventoryManager.addToInventory("wood", wood, 10);
                        //inventoryManager.addToInventory("stone", stone, 10);
                        //inventoryManager.addToInventory("stick", stick, 10);
                        //inventoryManager.addToInventory("woodPickaxe", woodPickaxe, 1);
                        //inventoryManager.addToInventory("stoneAxe", stoneAxe, 1);
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

        activeItem = inventoryManager.activeHotbarItem();
                currentActiveItem = inventoryManager.activeItem(inventoryManager.InventorySlots[activeItem], "Empty");
                Console.WriteLine(currentActiveItem);

     

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
            
            if (currentActiveItem == "woodPickaxe")
            {
                Player.pickaxeAnimation();
                if (Variable.FacingDirection == -1)
                {
                    characterPos.X -=60;
                }
                Raylib.DrawTextureRec(TextureClass.charTextures[5], pickaxeRec, characterPos, Color.WHITE);
              
            }

            else
            {
                Raylib.DrawTextureRec(TextureClass.charTextures[4], punchRec, characterPos, Color.WHITE);
            }
        }
            //Raylib.DrawTextureRec(TextureClass.charTextures[5], pickaxeRec, characterPos, Color.WHITE);

        else
        {
            Variable.isMoving = false;
            Raylib.DrawTextureRec(TextureClass.charTextures[charVariable], facing, characterPos, Color.WHITE);
        }

        Raylib.DrawText("WASD to move", 300, 300, 70, Color.BLACK);
        Raylib.DrawText("Press F to punch", 300, 500, 70, Color.BLACK);


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
                Raylib.DrawTexture(textureManager.LoadTexture("IMG/itemChosen.png"), 40 + activeItem * 120, 60, Color.WHITE);
            }

            //Rita rektanglar i inventoryt när man trycker tab
            else if (itemPos > 3 && tab == true)
            {
                int X = (int)InventorySystem.slots[itemPos - 4].inventorySlot.x;
                int Y = (int)InventorySystem.slots[itemPos - 4].inventorySlot.y;

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

        if (playerAssets.characterRec.y > Variable.screenHeight)
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

        Raylib.ClearBackground(Color.WHITE);
        Raylib.DrawText("you died", Variable.screenWidth / 2, Variable.screenHeight / 2, 50, Color.GOLD);
    }

    Raylib.EndDrawing();
}

//Startskärmen

//En rörande bakgrund där man kan välja att spela eller lämna

//Spelet

//2D survival-i spel där man kan gå höger, vänster och hoppa
//Man ska kunna skapa en träpickaxe med trä
//Kunna hugga träd samt hacka sten 
//Inventorysystem som fungerar korrekt eller delvis korrekt