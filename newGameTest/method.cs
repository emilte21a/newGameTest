using Raylib_cs;
using System;



public class Method
{
public static void gravityMethod()
{
    float gravity = 0.5f;
    float maxFallSpeed = 15;
    float minFallSpeed = -15;
    
    isColliding();

    if(!Variable.touchFloor)
    {
        Variable.gravity.Y += gravity;
        CharProp.characterRec.y += Variable.gravity.Y;

        if (Variable.gravity.Y > maxFallSpeed)
        {
            Variable.gravity.Y = maxFallSpeed;
        }
    }

    else
    {
        Variable.gravity.Y -= 15;
        if (Variable.gravity.Y < minFallSpeed)
        {
            Variable.gravity.Y = minFallSpeed;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)  && Variable.touchFloor==true)
        {
            CharProp.characterRec.y += Variable.gravity.Y;
            Variable.touchFloor= false;
            Method.jumpMechanics();
        }
    }
}

public static void isColliding(){
Rectangles r = new();

//foreach (var rect in r.floors)
//{
    
    if(!Raylib.CheckCollisionRecs(Rectangles.hitBox, Rectangles.Floor2))
    {
        Variable.touchFloor = false;
    }

    else
    {
        Variable.touchFloor = true;
    }
//}
}


public static void jumpMechanics(){
    
    Variable.gravity.Y = -15f; 
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

    if (Variable.gravity.Y > 0)
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
Variable.gravity.Y = 0;
CharProp.characterRec.y = TextureClass.backgroundTextures[0].height;
CharProp.characterRec.x = Variable.screenWidth / 2;
}



public static void runningLogic()
{
    
    int maxFrames = 4;
    

    Variable.timer+=2;
    

    if (Variable.timer>20)
    {
        Variable.timer=0;
        Variable.frame++;
    }
    Variable.frame = Variable.frame % maxFrames;
}




public static void bothADdown(){
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && (Raylib.IsKeyDown(KeyboardKey.KEY_A)))
    {
        Variable.bothButtonsPressed = true;
    }

    else
    {
        Variable.bothButtonsPressed = false;
    }

    
        
    
}

}