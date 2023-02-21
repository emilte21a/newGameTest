using Raylib_cs;
using System;



public class Method
{
    public static void gravityMethod(){
    
    if (!Raylib.CheckCollisionRecs(CharProp.characterRec, Rectangles.Floor))
    {
        Variable.touchFloor = false;
        Variable.gravity.Y -= 0.5f;
        CharProp.characterRec.y -= Variable.gravity.Y;

        if (Variable.gravity.Y < -50)
        {
            Variable.gravity.Y = -50;
        }
    }
    
    if (Raylib.CheckCollisionRecs(CharProp.characterRec, Rectangles.Floor))
    {
        Variable.touchFloor = true;
        Variable.gravity.Y = 0;
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            CharProp.characterRec.y -= 10;
            Variable.gravity.Y = 15f;
        }
    }
    }


    public static float walkingX(float characterx, float speed) 
{
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) //&& characterx > 0
    )
    {
        characterx -= speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) //&& characterx < Variable.screenWidth - 100
    )
    {
        characterx += speed;
    }
    return characterx;
}


public static int jumpAnim()
{
if (Variable.touchFloor == true)
{
    return 0;
}
else if (Variable.touchFloor == false)
{  

    if (Variable.gravity.Y < 0)
    {
        return 1; //om gravitationens y-värde är mindre än 0 så returnas 1, vilket är ett index för falling texture
    }

    else{
        return 2; //Annars så returneras 2 vilket är ett annat index för jumping texure
    }
}
else {
    return 4;
}

}

public static void resetVars(){

CharProp.characterRec.y = TextureClass.backgroundTextures[0].height;
CharProp.characterRec.x = Variable.screenWidth / 2;
}

}