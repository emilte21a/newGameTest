using System;
using Raylib_cs;
using System.Numerics;


public class characterMethods
{

    public static void gravityMethod()
    {
        float gravity = 0.5f;
        float maxFallSpeed = 15;
        float minFallSpeed = -15;

        isColliding();

        if (!Variable.touchFloor)
        {
            Variable.gravity.Y += gravity;
            characterProperties.characterRec.y += Variable.gravity.Y;

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
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && Variable.touchFloor == true)
            {
                characterProperties.characterRec.y += Variable.gravity.Y;
                Variable.touchFloor = false;
                characterMethods.jumpMechanics();
            }
        }
    }


    public static void isColliding()
    {
        BlockObject floorCollection = new BlockObject();
        

        for (var i = 0; i < floorCollection.floors.Count; i++)
        {
            blockEntity floor = floorCollection.floors[i];
            if (!Raylib.CheckCollisionRecs(characterProperties.hitBox, floor.cellBlock))
            {
                Variable.touchFloor = false;
            }

            if (Raylib.CheckCollisionRecs(characterProperties.hitBox, floor.cellBlock))
            {
                Variable.touchFloor = true;
            }
        }
    }



    public static void jumpMechanics()
    {

        Variable.gravity.Y = -15f;
    }


    public static float walkingX(float characterx, float speed)
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) //&& characterx > 0
        )
        {
            Variable.isMoving = true;
            characterx -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) //&& characterx < Variable.screenWidth - 100
        )
        {
            Variable.isMoving = true;
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

            else
            {
                return 2; //Annars så returneras 2 vilket är ett annat index för jumping texure
            }
        }
        else
        {
            return 4;
        }

    }

    public static void resetVars()
    {
        Variable.gravity.Y = 0;
        characterProperties.characterRec.y = TextureClass.backgroundTextures[0].height;
        characterProperties.characterRec.x = Variable.screenWidth / 2;
    }



    public static void runningLogic()
    {

        int maxFrames = 4;

        Variable.timer += 2;

        if (Variable.timer > 20)
        {
            Variable.timer = 0;
            Variable.frame++;
        }
        Variable.frame = Variable.frame % maxFrames;
    }

    public static void punchLogic()
    {
        int maxFrames = 6;

        Variable.timer2 += 2;

        if (Variable.timer2 > 10)
        {
            Variable.timer2 = 0;
            Variable.punchFrame++;
        }

        if (Variable.punchFrame == maxFrames)
        {
            Variable.punchFrame = 0;
        }

    }


    public static void bothADdown()
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && (Raylib.IsKeyDown(KeyboardKey.KEY_A)))
        {
            Variable.bothButtonsPressed = true;
            Variable.isMoving = false;
        }

        else
        {
            Variable.bothButtonsPressed = false;
        }

    }

}

public class characterProperties
{

    public static Rectangle characterRec = new Rectangle(Variable.screenWidth / 2, TextureClass.backgroundTextures[0].height, TextureClass.charTextures[0].width, TextureClass.charTextures[0].height);
    
    public static Rectangle hitBox = new Rectangle(characterProperties.characterRec.x, characterProperties.characterRec.y + 179, characterProperties.characterRec.width, 3);
    
    public static float speed = 4;
}