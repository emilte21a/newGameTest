using Raylib_cs;
using System;
using System.Numerics;

const int screenHeight = 1050;
const int screenWidth = 1300;

Raylib.InitWindow(screenWidth, screenHeight, "Topdown game");
Raylib.SetTargetFPS(60);

string[] names = { "mad", "hollo", "wal" };


Rectangle Floor = new Rectangle(0, screenHeight-200, screenWidth, 200);

Rectangle enemyRec = new Rectangle(900, 900, 60, 60);
Rectangle characterRec = new Rectangle(60, 60, 60, 60);

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



foreach (Enemy en in enemies)
{
    Console.WriteLine(en.name);
}

float speed = 4;
float enemySpeed = 2;


Camera2D camera = new();
camera.zoom = 1.2f;
camera.rotation = 0;
camera.offset = new Vector2(screenHeight/2, screenWidth/2);

Vector2 gravity = new(0, 0);

while (!Raylib.WindowShouldClose())
{
    Vector2 characterPos = new Vector2(characterRec.x, characterRec.y);
    camera.target = characterPos; //Kamerans target är karaktärens position

    if (!Raylib.CheckCollisionRecs(characterRec, Floor))
    {
       
        gravity.Y -= 0.5f;
        characterRec.y -= gravity.Y;
    }
    if (characterRec.y > 740){
            characterRec.y = 740;
        }

    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
    {
        characterRec.y -= 1;
        gravity.Y = 15f;
    }


    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && characterRec.x > 0)
    {
        characterRec.x -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && characterRec.x < 1000 - 60)
    {
        characterRec.x += speed;
    }

    




    // Vector2 playerPos = new Vector2(characterRec.x, characterRec.y);
    // Vector2 fiendePos = new Vector2(enemyRec.x, enemyRec.y);
    // Vector2 diff = playerPos - fiendePos;
    // Vector2 fiendeDirection = Vector2.Normalize(diff);


    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    Raylib.DrawText($"{e.name}", 400, 400, 50, Color.BLACK);
    Raylib.DrawRectangle((int)characterRec.x, (int)characterRec.y, 60, 60, Color.BLUE);
    //Raylib.DrawRectangle((int)enemyRec.x, (int)enemyRec.y, 60, 60, Color.RED);
    Raylib.DrawRectangle(0, screenHeight-250, screenWidth, 200, Color.GREEN); //Ground

    Raylib.EndDrawing();

}





