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



Color mountainColor = new Color(255, 255, 255, 155);


int charVariable;

foreach (Enemy en in enemies)
{
    Console.WriteLine(en.name);
}


float enemySpeed = 2;


Camera2D camera = new();
camera.zoom = 0.9f;
camera.rotation = 0;
camera.offset = new Vector2(1180 / 2, Variable.screenWidth/2);

//1180 = Screenwidth-120 (120 är texturens bredd)

string currentScene = "start";

while (!Raylib.WindowShouldClose())
{

    //Logik====================

    Vector2 characterPos = new Vector2(characterProperties.characterRec.x, characterProperties.characterRec.y);
    Vector2 skyPos = new Vector2(-Variable.screenWidth / 2, 0);
    Vector2 mountainPos = new Vector2(-Variable.screenWidth / 2, Variable.screenHeight / 3.5f);
    Vector2 hillsPos = new Vector2(-Variable.screenWidth /2, Variable.screenHeight/2);


    camera.target = characterPos; //Kamerans target är karaktärens position

    if (currentScene == "start")
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
            characterMethods.resetVars();
        }

    }

    else if (currentScene == "game")
    {
        BlockObject.loadBlocks();
        characterMethods.gravityMethod();
        characterProperties.characterRec.x = characterMethods.walkingX(characterProperties.characterRec.x, characterProperties.speed);
        //&& Rectangles.hitBox.y < Rectangles.Floor.y+100


        if (characterProperties.characterRec.y > Variable.screenHeight)
        {
            currentScene = "dead";
        }
    }

    else if (currentScene == "dead")
    {
        characterMethods.resetVars();

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
        charVariable = characterMethods.jumpAnim();
        characterMethods.runningLogic();
        characterMethods.bothADdown();
        Methods.meleeMethod();
        Methods.punchReturn();
        
        
        

        Color punchColor = new Color(255, 255, 255, Variable.punchColorAlpha);

        Rectangle sourceRec1 = new Rectangle(120 * Variable.frame, 0, Variable.way * 120, 180);
        Rectangle facing = new Rectangle(0, 0, Variable.way * 120, 180);
        Rectangle skyRec = new Rectangle(Variable.skyPlacementX/4, 0, TextureClass.backgroundTextures[1].width, TextureClass.backgroundTextures[1].height);
        Rectangle mountainRec = new Rectangle(Variable.skyPlacementX/2, 0, TextureClass.backgroundTextures[1].width, TextureClass.backgroundTextures[2].height);
        Rectangle hillsRec = new Rectangle(Variable.skyPlacementX, 0, TextureClass.backgroundTextures[1].width, TextureClass.backgroundTextures[4].height);
        
        
        Rectangle punchRec = new Rectangle(120 * Variable.punchFrame, 0, Variable.way * 120, 180);





        Raylib.DrawTextureRec(TextureClass.backgroundTextures[1], skyRec, skyPos, Color.WHITE);
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[2], mountainRec, mountainPos, mountainColor);
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[4], hillsRec, hillsPos, Color.WHITE);
        

        Raylib.BeginMode2D(camera);

        characterProperties.hitBox.x = characterProperties.characterRec.x;
        characterProperties.hitBox.y = characterProperties.characterRec.y + 180;



        /*
        foreach (var item in BlockObject.floors)
        {
            Raylib.DrawTexture(TextureClass.backgroundTextures[0], (int)item.cellBlock.x, (int)item.cellBlock.y, Color.WHITE);
        }
        */

        for (var i = 0; i < BlockObject.floors.Count; i++)
        {
            blockEntity floor = BlockObject.floors[i];
            Raylib.DrawTexture(TextureClass.backgroundTextures[0], (int)floor.cellBlock.x, (int)floor.cellBlock.y, Color.WHITE);
        }

        BlockObject.drawDirtBlocks();

        for (var i = 0; i < treeCollection.Trees.Count; i++)
        {
            TreeEntity tree = treeCollection.Trees[i];

            if (tree.treeHealth > 0)
            {
                //Raylib.DrawRectangleRec(tree.rect, Color.BLACK);
                Raylib.DrawTexture(TextureClass.otherTextures[3], (int)tree.rect.x, 420, Color.WHITE);

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F) && Raylib.CheckCollisionRecs(characterProperties.characterRec, tree.rect) && Variable.punchTimer==100)
                {
                    tree.breakTreeMethod();
                    if (tree.treeHealth == 0)
                    {
                        Variable.amountOfWood += 10;
                    }
                }


            }

        }



        //Raylib.DrawRectangle((int)Rectangles.hitBox.x, (int)Rectangles.hitBox.y, (int)Rectangles.hitBox.width, (int)Rectangles.hitBox.height, Color.LIME);

        if (Raylib.IsKeyReleased(KeyboardKey.KEY_D) && Variable.isMoving == true || (Raylib.IsKeyDown(KeyboardKey.KEY_D) && Variable.isMoving == true))
        {
            Variable.way = 1;
            Variable.skyPlacementX += 0.5f;
        }

        else if (Raylib.IsKeyReleased(KeyboardKey.KEY_A) && Variable.isMoving == true || (Raylib.IsKeyDown(KeyboardKey.KEY_A) && Variable.isMoving == true))
        {
            Variable.way = -1;
            Variable.skyPlacementX -= 0.5f;
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
        Raylib.DrawText($"{Variable.amountOfWood}", 60, 155, 30, Color.WHITE);
        Raylib.DrawRectangle(Variable.screenWidth/2, 0, 1, Variable.screenHeight, Color.ORANGE);
        Raylib.DrawRectangle(0, Variable.screenHeight/2, Variable.screenWidth, 1, Color.ORANGE);
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