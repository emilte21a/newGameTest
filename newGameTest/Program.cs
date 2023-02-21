using Raylib_cs;
using System;
using System.Numerics;



Raylib.InitWindow(Variable.screenWidth, Variable.screenHeight, "Road Rider");
Raylib.SetTargetFPS(60);

string[] names = { "mad", "hollo", "wal" };

TextureClass t = new();


Rectangle enemyRec = new Rectangle(900, 900, 120, 120);


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

int charVariable;

foreach (Enemy en in enemies)
{
    Console.WriteLine(en.name);
}


float enemySpeed = 2;


Camera2D camera = new();
camera.zoom = 1;
camera.rotation = 0;
camera.offset = new Vector2(Variable.screenHeight/2, Variable.screenWidth/2);

string currentScene = "Start";

while (!Raylib.WindowShouldClose())
{

//Logik====================

    Vector2 characterPos = new Vector2(CharProp.characterRec.x, CharProp.characterRec.y);
    camera.target = characterPos; //Kamerans target är karaktärens position

    switch (currentScene)
    {
        
        case ("Start"):

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE));
        {
            currentScene="Game";
        }

        break;

        case ("Game"):
        Method.gravityMethod();
        CharProp.characterRec.x = Method.walkingX(CharProp.characterRec.x, CharProp.speed);
        break;
    }


    



    // Vector2 playerPos = new Vector2(characterRec.x, characterRec.y);
    // Vector2 fiendePos = new Vector2(enemyRec.x, enemyRec.y);
    // Vector2 diff = playerPos - fiendePos;
    // Vector2 fiendeDirection = Vector2.Normalize(diff);

//Grafik===========================================

Raylib.BeginDrawing();

switch (currentScene)
{
        
case ("Start"):
    Raylib.ClearBackground(Color.WHITE);

break;

case ("Game"):
    charVariable = Method.jumpAnim();

    Raylib.BeginMode2D(camera);
    Raylib.ClearBackground(Color.WHITE);
    Raylib.DrawRectangle(-100000, -10000, 1000000, 1000000, Color.BLUE);
    Raylib.DrawTexture(t.backgroundTextures[0], (int)Rectangles.Floor.x, (int)Rectangles.Floor.y, Color.WHITE);
    Raylib.DrawTexture(t.charTextures[charVariable],(int)CharProp.characterRec.x, (int)CharProp.characterRec.y, Color.WHITE);
    Raylib.DrawText($"{e.name}", 400, 400, 50, Color.BLACK);

break;
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