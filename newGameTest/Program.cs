using Raylib_cs;
using System;
using System.Numerics;


Raylib.InitWindow(Variable.screenWidth, Variable.screenHeight, "Road Rider");
Raylib.SetTargetFPS(60);

string[] names = { "mad", "hollo", "wal" };

TextureClass Textures = new();
BlockObject floorCollection = new BlockObject();
TreeObject treeCollection = new TreeObject();


Rectangle enemyRec = new Rectangle(900, 900, 120, 120);


Enemy e = new Enemy();
Enemy e2 = new Enemy();
Enemy e3 = new Enemy();

List<Enemy> enemies = new();

enemies.Add(new Enemy() { name = "Joseph" });
enemies.Add(new Enemy() { name = "Avdol" });
enemies.Add(new Enemy() { name = "Jean Pierre" });

/*
System.Timers.Timer timer = new (interval: 1000); 
timer.Elapsed += ( sender, e ) => methodClass.HandleTimer();

int HandleTimer(int ){

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

string currentScene = "start";

while (!Raylib.WindowShouldClose())
{

    //Logik====================

    Vector2 characterPos = new Vector2(characterProperties.characterRec.x, characterProperties.characterRec.y);
    Vector2 skyPos = new Vector2(-Variable.screenWidth / 2, 0);
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
        Player.runningLogic();
        Player.bothADdown();
        Methods.meleeMethod();
        Methods.punchReturn();
        Methods.parallaxEffect();

        //Spring texturens source rektangel
        Rectangle sourceRec1 = new Rectangle(120 * Variable.frame, 0, Variable.way * 120, 180);

        //Spelarens textur rektangel med variabeln way på bredden för att rendera om vilket håll gubben är vänd
        Rectangle facing = new Rectangle(0, 0, Variable.way * 120, 180);


        //Bakgrundens texturers source rektanglar
        Rectangle skyRec = new Rectangle(Variable.skyPlacementX / 4, 0, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[0].height);
        Rectangle mountainRec = new Rectangle(Variable.skyPlacementX / 2, 0, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[1].height);
        Rectangle hillsRec = new Rectangle(Variable.skyPlacementX, 0, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[2].height);
        Rectangle punchRec = new Rectangle(120 * Variable.punchFrame, 0, Variable.way * 120, 180);

        //Slag rektangeln och dess färg
        Color punchColor = new Color(255, 255, 255, Variable.punchColorAlpha);

        //Rita ut bakgrundstexturerna
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[0], skyRec, skyPos, Color.WHITE);
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
                        Variable.amountOfWood += 10;
                    }
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

        else
        {

            Variable.isMoving = false;
            Raylib.DrawTextureRec(TextureClass.charTextures[charVariable], facing, characterPos, Color.WHITE);
        }

        Raylib.DrawText($"{e.name}", 400, 400, 50, Color.BLACK);
        Raylib.EndMode2D();
        Raylib.DrawTexture(TextureClass.otherTextures[0], 10, 0, Color.WHITE);
        Raylib.DrawTexture(TextureClass.otherTextures[2], 1100, 25, Color.WHITE);
        Raylib.DrawTexture(TextureClass.otherTextures[1], 1110, 35, Color.WHITE);
        Raylib.DrawRectangle(1110, 35, Variable.punchRectWidth, 100, punchColor);

        Raylib.DrawFPS(2, 2);
        Raylib.DrawTexture(TextureClass.otherTextures[4], 20, 150, Color.WHITE);

        if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
        {
            InventorySystem.loadInventory();

            Raylib.DrawTexture(TextureClass.otherTextures[5], 400, 10, Color.WHITE);
            for (int i = 0; i < InventorySystem.slots.Count; i++)
            {
                inventoryEntitySlot invSlot = InventorySystem.slots[i];
                Raylib.DrawRectangle((int)invSlot.inventorySlot.x, (int)invSlot.inventorySlot.y, InventorySystem.slotWidth, InventorySystem.slotWidth, Color.RED);
                //break;
            }
        }

        Raylib.DrawText($"{Variable.amountOfWood}", 60, 155, 30, Color.WHITE);
        // Raylib.DrawRectangle(Variable.screenWidth / 2, 0, 1, Variable.screenHeight, Color.ORANGE);
        // Raylib.DrawRectangle(0, Variable.screenHeight / 2, Variable.screenWidth, 1, Color.ORANGE);
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

//2D survival-ish spel där man kan gå höger, vänster och hoppa
//Man ska kunna skapa tools med en crafting table 
//Kunna hugga träd samt hacka sten och döda monster
//Försök till day-night time cycle


//