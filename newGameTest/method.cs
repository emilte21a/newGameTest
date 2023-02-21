using Raylib_cs;
using System;



public class Method
{
    public static void gravityMethod(){
        if (!Raylib.CheckCollisionRecs(CharProp.characterRec, Rectangles.Floor))
    {
        Variable.gravity.Y -= 0.5f;
        CharProp.characterRec.y -= Variable.gravity.Y;
    }
    if (CharProp.characterRec.y > 700){
            CharProp.characterRec.y = 700;
        }
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
    {
        CharProp.characterRec.y -= 1;
        Variable.gravity.Y = 15f;
    }
    }

    public static float walkingX(float characterx, float speed) 
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


public static int jumpAnim(){

if (Variable.gravity.Y > 0)
{
    return 1;
}
else if (Variable.gravity.Y < 0)
{
    return 2;
}
else if (Variable.gravity.Y == 0)
{
    return 0;

}

}