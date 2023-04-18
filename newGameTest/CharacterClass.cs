using System;
using Raylib_cs;
using System.Numerics;


public class playerAssets{
    
    public static Rectangle characterRec = new Rectangle(Variable.screenWidth / 2, TextureClass.blockTextures[0].height, TextureClass.charTextures[0].width, TextureClass.charTextures[0].height);
    
    public static Rectangle hitBox = new Rectangle(6, + 179, TextureClass.charTextures[0].width, 3);
    
    public static float speed = 4;
}
public class Player
{
    BlockObject BlockObject = new();

    
    playerAssets playerAssets = new();
    public void GravityPhysics()
    {   
        float gravity = 0.5f;
        float maxFallSpeed = 15;
        float minFallSpeed = -15;

        isColliding();

        if (!Variable.touchFloor)
        {
            Variable.gravity.Y += gravity;
            playerAssets.characterRec.y += Variable.gravity.Y;

            if (Variable.gravity.Y > maxFallSpeed)
            {
                Variable.gravity.Y = maxFallSpeed;
            }
        }

        else 
        {
            Variable.gravity.Y = 0;
            if (Variable.gravity.Y < minFallSpeed)
            {
                Variable.gravity.Y = minFallSpeed;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && Variable.touchFloor == true)
            {
                Variable.gravity.Y -= 15;
                playerAssets.characterRec.y += Variable.gravity.Y;
                jumpMechanics();
                Variable.touchFloor = false;
            }
        }
    }

    public void isColliding()
    {
        BlockObject.loadBlocks();
        for (var i = 0; i < BlockObject.floors.Count; i++)
        {  
            blockEntity floor = BlockObject.floors[i];
            if (!Raylib.CheckCollisionRecs(playerAssets.hitBox, floor.cellBlock))
            {
                Variable.touchFloor = false;
            }
            else if (Raylib.CheckCollisionRecs(playerAssets.hitBox, floor.cellBlock))
            {
                Variable.touchFloor = true;
                break;
            }
        }
    }

    public void jumpMechanics()
    {
        Variable.gravity.Y = -15f;
    }


    public float walkingX(float characterx, float speed)
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) 
        )
        {
            Variable.isMoving = true;
            characterx -= speed;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) 
        )
        {
            Variable.isMoving = true;
            characterx += speed;
        }
        return characterx;
    }


    public int jumpAnimation()
    {
        if (Variable.touchFloor == true)
        {
            return 0;
        }
        else if (Variable.touchFloor == false)
        {

            if (Variable.gravity.Y > 0)
            {
                return 1; //om gravitationens y-värde är större än 0 så returnas 1, vilket är ett index för falling texture
            }

            else
            {
                return 2; //Annars så returneras 2 vilket är ett annat index för jumping texturen
            }
        }
        else
        {
            return 4;
        }

    }

    public void resetVars()
    {
        Variable.gravity.Y = 0;
        playerAssets.characterRec.y = TextureClass.blockTextures[0].height;
        playerAssets.characterRec.x = Variable.screenWidth / 2;
    }


    int timer = 1;
    int timer2 = 1;
    int frame = 1;
    public int runningAnimation()
    {

        int maxFrames = 4;

        timer += 2;

        if (timer > 20)
        {
            timer = 0;
            Variable.runningFrame++;
        }
        Variable.runningFrame = Variable.runningFrame % maxFrames;
        return Variable.runningFrame;

    }

    public int punchAnimation()
    {
        int maxFrames = 6;

        timer2+=2;
        if (timer2 > 10)
        {
            timer2 = 0;
           Variable.punchFrame++;
        }

        if (Variable.punchFrame == maxFrames)
        {
            Variable.punchFrame = 0;
        }
        return Variable.punchFrame;
    }

    public int pickaxeAnimation()
    {

        timer2 += 1;

        if (timer > 16)
        {
            timer2 = 0;
            frame++;
        }
        return frame;
    }

    public void bothADdown()
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

