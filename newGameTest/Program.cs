using Raylib_cs;
using System;
using System.Numerics;



Raylib.InitWindow(Variable.screenWidth, Variable.screenHeight, "Road Rider");
Raylib.SetTargetFPS(60);

string[] names = { "mad", "hollo", "wal" };

TextureClass t = new();


Rectangle enemyRec = new Rectangle(900, 900, 100, 100);


Enemy e = new Enemy();
Enemy e2 = new Enemy();
Enemy e3 = new Enemy();

List<Enemy> enemies = new();

enemies.Add(new Enemy() { name = "Joseph" });
enemies.Add(new Enemy() { name = "Avdol" });
enemies.Add(new Enemy() { name = "Jean Pierre" });

List<Rectangle> walls = new();
walls.Add(new Rectangle(800, 700,100, 50));
walls.Add(new Rectangle(800, 700,100, 50));
walls.Add(new Rectangle(800, 700,100, 50));
walls.Add(new Rectangle(800, 700,100, 50));

static float walkingX(float characterx, float speed) 
{
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && characterx > 0)
    {
        characterx -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && characterx < Variable.screenWidth - 100)
    {
        characterx += speed;
    }
    return characterx;
}


foreach (Enemy en in enemies)
{
    Console.WriteLine(en.name);
}


float enemySpeed = 2;


Camera2D camera = new();
camera.zoom = 1;
camera.rotation = 0;
camera.offset = new Vector2(Variable.screenHeight/2, Variable.screenWidth/2);



while (!Raylib.WindowShouldClose())
{
    Vector2 characterPos = new Vector2(CharProp.characterRec.x, CharProp.characterRec.y);
    camera.target = characterPos; //Kamerans target är karaktärens position

    

    CharProp.characterRec.x = walkingX(CharProp.characterRec.x, CharProp.speed);

    



    // Vector2 playerPos = new Vector2(characterRec.x, characterRec.y);
    // Vector2 fiendePos = new Vector2(enemyRec.x, enemyRec.y);
    // Vector2 diff = playerPos - fiendePos;
    // Vector2 fiendeDirection = Vector2.Normalize(diff);


    Raylib.BeginDrawing();
    Raylib.BeginMode2D(camera);
    Raylib.ClearBackground(Color.WHITE);
    Raylib.DrawText($"{e.name}", 400, 400, 50, Color.BLACK);
    Raylib.DrawTexture(t.charTextures[0],(int)CharProp.characterRec.x, (int)CharProp.characterRec.y, Color.WHITE);

    //Raylib.DrawRectangle((int)enemyRec.x, (int)enemyRec.y, 60, 60, Color.RED);
    Raylib.DrawTexture(t.backgroundTextures[0], 0, 800, Color.WHITE); //Ground

    Raylib.EndDrawing();

}





