using Raylib_cs;
using System;
using System.Numerics;
using System.Collections.Generic;

Raylib.InitWindow(Variable.screenWidth, Variable.screenHeight, "Terraria knockoff");
Raylib.SetTargetFPS(60);

//Initialiserar alla klasser
InventoryItem invItem = new();
inventory inventoryManager = new();
TextureManager textureManager = new();
Player Player = new();
Methods Methods = new();
BlockObject BlockObject = new();
TreeObject TreeObject = new();
rockObject rockObject = new();
CraftingSystem craftingSystem = new();


//Instanser av varje item
wood wood = new();
stone stone = new();
stick stick = new();
woodPickaxe woodPickaxe = new();
stoneAxe stoneAxe = new();

//Lokala variabler inom program.cs
float parallaxPos = 1f;
string currentScene = "start";
Color skyColor = new Color(115, 215, 255, 255);

//Sätt karaktärens y position till 400
PlayerAssets.characterRec.y = 400;

while (!Raylib.WindowShouldClose())
{
    //Karaktärens positionsvektor
    Vector2 characterPos = new Vector2(PlayerAssets.characterRec.x, PlayerAssets.characterRec.y);

    //Bakgrundens vektorer
    Vector2 skyPos = new Vector2(0, -100);
    Vector2 mountainPos = new Vector2(-Variable.screenWidth / 2, Variable.screenHeight / 4f);
    Vector2 hillsPos = new Vector2(-Variable.screenWidth / 2, Variable.screenHeight / 2);

    //Kameran
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
        PlayerAssets.characterRec.height = 50;
        PlayerAssets.characterRec.y = Player.skippingY(PlayerAssets.characterRec.y);
        //Karaktärens Y värde är lika med funktionen SkippingY som tar emot karaktärens Y värde som parameter och returnerar sedan den

        //Öka parallaxpos med 0.5 varje frame
        parallaxPos += 0.5f;
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
            Player.resetVars();
            BlockObject.loadBlocks();
            TreeObject.LoadTrees();
            rockObject.loadRocks();
            //Spawna varje objekt in i spelet
        }

        Raylib.ClearBackground(Color.WHITE);

        //Start skärmens bakgrundstexturer
        Raylib.DrawTextureRec(textureManager.LoadTexture("IMG/startskybackground.png"),
        new Rectangle(parallaxPos / 4, 0, Variable.screenWidth, Variable.screenHeight),
        new Vector2(2, 0),
        Color.WHITE);

        Raylib.DrawTextureRec(textureManager.LoadTexture("IMG/startmountbackground.png"),
        new Rectangle(parallaxPos / 2, 0, Variable.screenWidth, Variable.screenHeight),
        new Vector2(2, 0),
        Color.WHITE);

        Raylib.DrawTextureRec(textureManager.LoadTexture("IMG/startlandbackground.png"),
        new Rectangle(parallaxPos, 0, Variable.screenWidth, Variable.screenHeight),
        new Vector2(2, 0),
        Color.WHITE);

        //Title textur
        Raylib.DrawTexture(textureManager.LoadTexture("IMG/title.png"), 300, 100, Color.WHITE);

        Raylib.DrawText("Press ENTER to", 540, 400, 30, Color.GOLD);
        Raylib.DrawText("START", 530, 430, 80, Color.GOLD);
        Raylib.DrawText("Instructions", 50, 500, 50, Color.BLACK);
        Raylib.DrawText("A-D to move", 50, 550, 30, Color.BLACK);
        Raylib.DrawText("SPACE to jump", 50, 600, 30, Color.BLACK);
        Raylib.DrawText("Press C to access your inventory", 50, 650, 30, Color.BLACK);
    }

    else if (currentScene == "game")
    {
         parallaxPos += 0.05f;
        //Sätt karaktärrektangelns höjd till 180
        PlayerAssets.characterRec.height = 180;
        Player.GravityPhysics();
        PlayerAssets.characterRec.x = Player.walkingX(PlayerAssets.characterRec.x, PlayerAssets.speed);
        //Karaktärens X värde är lika med funktionen WalkingX som tar emot karaktärens x värde och variabeln speed som paratmetrar

        Player.bothADdown();
        Methods.MeleeMethod();
        Methods.BackgroundParallaxEffect();
        Methods.punchReturn();
        inventoryManager.weaponDamageComponent();

        //Lokala variabler inom Game
        int activeItem = 1;
        int punchFrame = Variable.punchFrame;
        int runningFrame = Player.runningAnimation();
        int pickaxeFrame = Variable.pickaxeFrame;
        int charVariable = Player.jumpAnimation();
        //Bakgrundens texturers source rektanglar
        Rectangle skyRec = new Rectangle(Variable.skyPlacementX / 4+parallaxPos, 0, TextureClass.backgroundTextures[3].width, TextureClass.backgroundTextures[3].height);
        Rectangle mountainRec = new Rectangle(Variable.skyPlacementX / 2, 0, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[1].height);
        Rectangle hillsRec = new Rectangle(Variable.skyPlacementX, 0, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[2].height);

        //Slag rektangeln och dess färg
        Color punchColor = new Color(255, 255, 255, Variable.punchColorAlpha);

        //Bakgrundsbergtexturens färg
        Color mountainColor = new Color(255, 255, 255, 155);

        //Rita ut himmelen
        Raylib.DrawRectanglePro(
        new Rectangle(0, 0, Variable.screenWidth, Variable.screenHeight),
        new Vector2(0, 0),
        0,
        skyColor);

        //Rita ut bakgrundstexturerna
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[3], skyRec, skyPos, Color.WHITE);
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[1], mountainRec, mountainPos, mountainColor);
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[2], hillsRec, hillsPos, Color.WHITE);

        Raylib.BeginMode2D(camera); // Starta kameraläge

        //Karaktärens hitbox är lika med karaktärens rektangel
        //Hitboxens Y värde är karaktärens y värde plus 180
        PlayerAssets.hitBox.x = PlayerAssets.characterRec.x;
        PlayerAssets.hitBox.y = PlayerAssets.characterRec.y + 180;

        activeItem = inventoryManager.activeHotbarItem();
        Variable.currentActiveItem = inventoryManager.activeItem(inventoryManager.InventorySlots[activeItem], "Empty");
        //Det nuvarande aktiva item indexet är lika med activeHotBarItem funktionen

        for (var i = 0; i < BlockObject.floors.Count; i++)
        {
            blockEntity floor = BlockObject.floors[i];
            Raylib.DrawTexture(TextureClass.blockTextures[0], (int)floor.cellBlock.x, (int)floor.cellBlock.y, Color.WHITE);
        }
        //För varje block i listan BlockObject.floors så ska gräs texturen ritas ut 
        //Grästexturen ritas ut på varje cellBlocks position

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
                Om trädets HP är detsamma som 0 så ska spelarens mängd trä att öka med 3
                */
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F) && Raylib.CheckCollisionRecs(PlayerAssets.characterRec, tree.TreeRect) && Variable.punchTimer == 100 && Variable.canBreakWood == true)
                {
                    tree.breakTreeMethod();
                    if (tree.treeHealth == 0)
                    {
                        inventoryManager.addToInventory("wood", wood, 3);
                    }
                }
                if (tree.treeHealth < 100)
                {
                    Raylib.DrawText($"{tree.treeHealth}", (int)tree.TreeRect.x + 50, (int)tree.TreeRect.y + 30, 50, Color.RED);
                }
                //Om trädets liv är mindre än 100
                //Rita ut trädets mängd liv kvar bredvid
            }
        }

        //Rita ut stenar
        for (var i = 0; i < rockObject.Rocks.Count; i++)
        {
            rockEntity rock = rockObject.Rocks[i];

            if (rock.rockHealth > 0)
            {
                Raylib.DrawTexture(TextureClass.rockTextures[rockObject.rockTexture], (int)rock.rockRect.x, (int)rock.rockRect.y, Color.WHITE);
                /*
               Om man trycker på F och karaktärens rektangel kolliderar med stenens Rektangel
               Starta då breakStoneMethod
               Om stenens HP är detsamma som 0 så ska spelarens mängd trä att öka med 10
               */
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F) && Raylib.CheckCollisionRecs(PlayerAssets.characterRec, rock.rockRect) && Variable.punchTimer == 100 && Variable.canBreakStone == true)
                {
                    rock.breakStoneMethod();
                    if (rock.rockHealth == 0)
                    {
                        inventoryManager.addToInventory("stone", stone, 1);
                    }
                }
                if (rock.rockHealth < 100)
                {
                    Raylib.DrawText($"{rock.rockHealth}", (int)rock.rockRect.x + 10, (int)rock.rockRect.y, 50, Color.RED);
                }
                //Om stenens liv är mindre än 100
                //Rita ut stenens mängd liv kvar bredvid
            }
        }
        //Spring texturens source rektangel
        Rectangle sourceRec1 = new Rectangle(120 * runningFrame, 0, Variable.FacingDirection * 120, 180);

        //Spelarens textur rektangel med variabeln way på bredden för att rendera om vilket håll gubben är vänd
        Rectangle facing = new Rectangle(0, 0, Variable.FacingDirection * 120, 180);

        //Source rektangeln för pickaxe animationen
        Rectangle pickaxeRec = new Rectangle(180 * pickaxeFrame, 0, Variable.FacingDirection * 180, 180);

        //Source rektangeln för slå animationen 
        Rectangle punchRec = new Rectangle(120 * punchFrame, 0, Variable.FacingDirection * 120, 180);

        //Spring animationerna===============================================

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && Variable.touchFloor == true && Variable.bothButtonsPressed == false)
        {
            Raylib.DrawTextureRec(TextureClass.charTextures[3], sourceRec1, characterPos, Color.WHITE);
        }

        //Om knappen D eller A är nedtryckt och touchFloor är sann och om både A och D inte trycks så ska spring texturen ritas ut

        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && Variable.touchFloor == true && Variable.bothButtonsPressed == false)
        {
            Raylib.DrawTextureRec(TextureClass.charTextures[3], sourceRec1, characterPos, Color.WHITE);
        }

        else if (Variable.whilePunching > 0)
        {
            if (Variable.currentActiveItem == "woodPickaxe")
            {
                Player.swingingAnimation();
                
                if (Variable.FacingDirection == -1)
                {
                    characterPos.X -= 60;
                }
                Raylib.DrawTextureRec(TextureClass.charTextures[5], pickaxeRec, characterPos, Color.WHITE);
            }
            else if (Variable.currentActiveItem == "stoneAxe")
            {
                Player.swingingAnimation();
                if (Variable.FacingDirection == -1)
                {
                    characterPos.X -= 60;
                }
                Raylib.DrawTextureRec(TextureClass.charTextures[6], pickaxeRec, characterPos, Color.WHITE);
            }
            else
            {
                
                Raylib.DrawTextureRec(TextureClass.charTextures[4], punchRec, characterPos, Color.WHITE);
            }
            //Om variabeln whilePunching är större än 0
            //Om det aktiva itemet är woodPickaxe
            //rita ut träpickaxe animationen
            //Om det aktiva itemet är stoneAxe
            //rita ut stenyx animationen
            //Annars rita ut normala slag animationen
        }

        else
        {
            Variable.isMoving = false;
            Raylib.DrawTextureRec(TextureClass.charTextures[charVariable], facing, characterPos, Color.WHITE);
        }
        // Annars om gubben inte rör på sig så ska idle animationen ritas ut

        //TEXTS============================================
        Raylib.DrawText("Try punching down a tree", 50, 400, 30, Color.BLACK);
        Raylib.DrawText("Try punching down a tree", 52, 402, 30, Color.DARKGREEN);

        Raylib.DrawText("Try crafting a pickaxe to break a rock", 100, 600, 30, Color.BLACK);
        Raylib.DrawText("Try crafting a pickaxe to break a rock", 102, 602, 30, Color.DARKGRAY);
        Raylib.EndMode2D();

        //Texturer som ritas utanför kameran, alltså stannar de fast på skärmen
        Raylib.DrawTexture(TextureClass.otherTextures[0], 10, -10, Color.WHITE);
        Raylib.DrawTexture(TextureClass.otherTextures[2], 1100, 25, Color.WHITE);
        Raylib.DrawTexture(TextureClass.otherTextures[1], 1110, 35, Color.WHITE);
        Raylib.DrawRectangle(1110, 35, Variable.punchRectWidth, 100, punchColor);
        Raylib.DrawText("F", 1130, 140, 70, Color.BLACK);
        Raylib.DrawTexture(textureManager.LoadTexture("IMG/craftingtableicon.png"), 670, 50, Color.WHITE);
        Raylib.DrawText("C", 680, 140, 70, Color.BLACK);

        //INVENTORY========================================
        Console.WriteLine(Variable.currentActiveItem);
        bool tab = false;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
        {
            tab = true;
        }
        //Om tab = true

        int itemPos = 0;

        foreach (var item in inventoryManager.InventorySlots)
        {
            if (itemPos < inventoryManager.InventorySlots.Count())
            {
                if (item.Value != "Empty" && inventoryManager.ItemsInInventory.ContainsKey(item.Value))
                {
                    //Console.WriteLine(item.Value);
                    InventoryItem item1 = inventoryManager.ItemsInInventory[item.Value];
                    Raylib.DrawTexture(textureManager.LoadTexture(item1.Texture), 50 + 120 * itemPos, 70, Color.WHITE);
                    Raylib.DrawText($"{item1.stacks}", 110 + 120 * itemPos, 130, 20, Color.WHITE);
                }
                Raylib.DrawTexture(textureManager.LoadTexture("IMG/itemChosen.png"), 40 + activeItem * 120, 60, Color.WHITE);
            }
            itemPos++;
            if (itemPos >= inventoryManager.InventorySlots.Count())
            {
                //itemPos = inventoryManager.findFirstEmptySlot();
                Console.WriteLine(itemPos);
            }
            //För varje item i InventorySlots
            //Om Itempositionen är mindre än mängden inventorySlots
            //Om itemets value inte är Empty och om inventoryt innehåller itemet
            //Itemet i inventoryt är lika med valutan i dictionaryt ItemsInInventory
            //Rita ut itemets textur
            //Rita ut varje items mängd med deras variabel int Stacks
            //Öka itempos med 1 
            //Om itempos är större eller lika med mängden inventorySlots
            //Gör itempositionen till den första tomma platsen i inventoryt
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
        {
            currentScene = "craftingTable";
        }

        //Du dör om din karaktärs position är större än höjden på spelet
        if (PlayerAssets.characterRec.y > Variable.screenHeight)
        {
            currentScene = "dead";
        }
    }

    else if (currentScene == "craftingTable")
    {
        int itemPos = 0;
        foreach (var item in inventoryManager.InventorySlots)
        {
            if (item.Value != "Empty" && inventoryManager.ItemsInInventory.ContainsKey(item.Value))
            {
                InventoryItem item1 = inventoryManager.ItemsInInventory[item.Value];
                //Console.WriteLine("item.Value: " + item.Value);
            }
            itemPos++;
            if (itemPos >= inventoryManager.InventorySlots.Count())
            {
                itemPos = inventoryManager.findFirstEmptySlot();
            }
        }
        //För varje item i InventorySlots
        //Om Itempositionen är mindre än mängden inventorySlots
        //Om itemets value inte är Empty och om inventoryt innehåller itemet
        //Itemet i inventoryt är lika med valutan i dictionaryt ItemsInInventory
        //Öka itempos med 1 
        //Om itempos är större eller lika med mängden inventorySlots
        //Gör itempositionen till 0

        Raylib.DrawTexture(textureManager.LoadTexture("IMG/craftingTable.png"), 0, 0, Color.WHITE);

        craftingSystem.CraftingSyst();
        //Starta craftingSyst funktionen som kollar kontroller och skriver ut texter
        if (Variable.whichItem == 0)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && inventoryManager.CanCraft(stick))
            {
                inventoryManager.CraftItem(stick);
            }
            Raylib.DrawText($"Wood:{wood.stacks}", 700, 420, 20, Color.WHITE);
            Raylib.DrawText($"{stick.name}", 240, 300, 20, Color.WHITE);
            Raylib.DrawText($"{stick.stacks}", 350, 340, 20, Color.WHITE);
        }
        //Om variabeln whichItem är lika med 0
        //Om SPACE knappen trycks
        //Crafta itemet stick
        //Rita ut mängden trä som du har
        //Rita ut itemet sticks namn
        //Rita ut mängden sticks som du har i inventoryt

        else if (Variable.whichItem == 1)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && inventoryManager.CanCraft(woodPickaxe))
            {
                inventoryManager.CraftItem(woodPickaxe);
            }
            Raylib.DrawText($"Stick:{stick.stacks}", 700, 420, 20, Color.WHITE);
            Raylib.DrawText($"Wood:{wood.stacks}", 700, 440, 20, Color.WHITE);
            Raylib.DrawText($"{woodPickaxe.name}", 240, 300, 20, Color.WHITE);
            Raylib.DrawText($"{woodPickaxe.stacks}", 350, 340, 20, Color.WHITE);
        }
        //Om variabeln whichItem är lika med 1
        //Om SPACE knappen trycks
        //Crafta itemet woodPickaxe
        //Rita ut mängden pinnar som du har
        //Rita ut mängden trä som du har
        //Rita ut itemet woodPickaxe namn
        //Rita ut mängden woodPickaxes som du har i inventoryt

        else if (Variable.whichItem == 2)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && inventoryManager.CanCraft(stoneAxe))
            {
                inventoryManager.CraftItem(stoneAxe);
            }
            Raylib.DrawText($"Stick:{stick.stacks}", 700, 420, 20, Color.WHITE);
            Raylib.DrawText($"Stone:{stone.stacks}", 700, 440, 20, Color.WHITE);
            Raylib.DrawText($"{stoneAxe.name}", 240, 300, 20, Color.WHITE);
            Raylib.DrawText($"{stoneAxe.stacks}", 350, 340, 20, Color.WHITE);
        }

        //Om variabeln whichItem är lika med 2
        //Om SPACE knappen trycks
        //Crafta itemet stoneAxe
        //Rita ut mängden pinnar som du har
        //Rita ut mängden sten som du har
        //Rita ut itemet stoneAxe namn
        //Rita ut mängden stoneAxe som du har i inventoryt

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
        {
            currentScene = "game";
        }
        //Om C knappen trycks
        //Gör currentscene till game
    }

    else if (currentScene == "dead")
    {
        Player.resetVars();

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "start";
        }

        Raylib.ClearBackground(Color.RED);
        Raylib.DrawText("you died", Variable.screenWidth / 2, Variable.screenHeight / 2, 50, Color.BLACK);
        Raylib.DrawText("you died", Variable.screenWidth / 2 + 5, Variable.screenHeight / 2 + 5, 50, Color.WHITE);
    }
    Raylib.EndDrawing();
}

//Startskärmen

//En rörande bakgrund där man kan starta spelet när man trycker på Enter¨
//Instruktioner vid sidan om som berättar "grund" funktionerna i spelet

//Spelet

//2D survival-i spel där man kan gå höger, vänster och hoppa
//Man ska kunna skapa en träpickaxe med trä
//Kunna hugga träd samt hacka sten med yxa respektive pickaxe
//Inventorysystem som fungerar korrekt eller delvis korrekt
//Crafting system som fungerar korrekt eller delvis korrekt
//Det ska vara ett enkelt pixelerat gränssnitt som är simpelt men tydligt
//Man ska kunna gå in i crafting table när man trycker på C

//Crafting table

//I crafting tablet så ska man kunna se vilket item man ska skapa, vad som krävs för att skapa det och hur många av det itemet man har i inventoryt
//Det ska även stå hur mycket av de items som krävs som du har i ditt inventory 

//Slutskärmen
//Om man går av spelets gräns så ska man dö och spelet startas om