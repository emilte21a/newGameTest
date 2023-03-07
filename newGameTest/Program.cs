using Raylib_cs;
using System;
using System.Numerics;



Raylib.InitWindow(Variable.screenWidth, Variable.screenHeight, "Road Rider");
Raylib.SetTargetFPS(60);

string[] names = { "mad", "hollo", "wal" };

TextureClass t = new();
Rectangles r = new();


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
camera.offset = new Vector2(Variable.screenHeight / 2, Variable.screenWidth / 2);

string currentScene = "start";

while (!Raylib.WindowShouldClose())
{

    //Logik====================

    Vector2 characterPos = new Vector2(characterProperties.characterRec.x, characterProperties.characterRec.y);
    Vector2 skyPos = new Vector2(-Variable.screenWidth / 2, 0);
    Vector2 mountainPos = new Vector2(-Variable.screenWidth / 2, Variable.screenHeight/2.5f);

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

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) ;
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
        
        Console.WriteLine(Variable.whilePunching);
        Console.WriteLine(Variable.punchFrame);

        if (Variable.whilePunching > 0)
        {
            Variable.whilePunching--;
            characterMethods.punchLogic();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F) && !Variable.isMoving && Variable.gravity.Y == -15 && Variable.whilePunching == 0)
        {
            
            Variable.whilePunching = 25;
            
        }

        if (Variable.whilePunching == 0)
        {
            Variable.punchFrame = 1;
            Variable.whilePunching = 0;
        }



        Rectangle sourceRec1 = new Rectangle(120 * Variable.frame, 0, Variable.way * 120, 180);
        Rectangle facing = new Rectangle(0, 0, Variable.way * 120, 180);
        Rectangle skyRec = new Rectangle(Variable.skyPlacementX * 1, 0, TextureClass.backgroundTextures[1].width, TextureClass.backgroundTextures[1].height);
        Rectangle mountainRec = new Rectangle(Variable.skyPlacementX * 0.5f, 0, TextureClass.backgroundTextures[1].width, TextureClass.backgroundTextures[2].height);
        Rectangle punchRec = new Rectangle(120* Variable.punchFrame, 0, Variable.way * 120, 180);


        Raylib.ClearBackground(Color.WHITE);



        Raylib.DrawTextureRec(TextureClass.backgroundTextures[1], skyRec, skyPos, Color.WHITE);
        Raylib.DrawTextureRec(TextureClass.backgroundTextures[2], mountainRec, mountainPos, mountainColor);
        
        
        Raylib.BeginMode2D(camera);

        Rectangles.hitBox.x = characterProperties.characterRec.x;
        Rectangles.hitBox.y = characterProperties.characterRec.y + 180;





        //Raylib.DrawRectangle((int)Rectangles.Floor.x, (int)Rectangles.Floor.y, (int)Rectangles.Floor.width, (int)Rectangles.Floor.height, Color.BLUE);


        Raylib.DrawTexture(TextureClass.backgroundTextures[0], (int)Rectangles.Floor2.x, (int)Rectangles.Floor2.y, Color.WHITE);
        //Raylib.DrawTexture(TextureClass.backgroundTextures[0], (int)Rectangles.Floor2.x, (int)Rectangles.Floor2.y, Color.WHITE);
        //Raylib.DrawTexture(TextureClass.backgroundTextures[0], (int)Rectangles.Floor3.x, (int)Rectangles.Floor3.y, Color.WHITE);


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
        Raylib.DrawRectangle(-1300, 1150, 4200, 1600, Color.BLACK);
        Raylib.EndMode2D();
        Raylib.DrawTexture(TextureClass.otherTextures[0], 10, 0, Color.WHITE);
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