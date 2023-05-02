using System;
using Raylib_cs;
using System.Numerics;


public class PlayerAssets
{

    public static Rectangle characterRec = new Rectangle(Variable.screenWidth / 2, -1000, TextureClass.charTextures[0].width, TextureClass.charTextures[0].height);
    public static Rectangle hitBox = new Rectangle(6, +179, TextureClass.charTextures[0].width, 3);
    public static float speed = 4;
}

public class Player
{
    BlockObject BlockObject = new();
    //PlayerAssets playerAssets = new();
    //AnimationClass animationClass = new();
    
    public void GravityPhysics()
    {
        //Lokala variabler
        float gravity = 0.5f;
        float maxFallSpeed = 15; //Max hastighet
        float minFallSpeed = -15; //Min hastighet

        isColliding();

        if (!Variable.touchFloor)
        {
            Variable.gravity.Y += gravity;
            PlayerAssets.characterRec.y += Variable.gravity.Y;

            if (Variable.gravity.Y > maxFallSpeed)
            {
                Variable.gravity.Y = maxFallSpeed;
            }
        }  
        //Om karaktärens rektangel inte kolliderar med marken
        //Variabeln gravitys(Vector) Y-värde ska öka med gravity
        //Karaktärens Y-värde ska adderas med vektorn gravitys Y-värde
        //Om gravitys Y-värde är större än MaxFallSpeed så är den lika med MaxFallSpeed 

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
                PlayerAssets.characterRec.y += Variable.gravity.Y;
                jumpMechanics();
                Variable.touchFloor = false;
            }
        }
        //Annars är gravitys Y-värde är detsamma som 0
        //Om gravitys Y-värde är mindre än MinFallSpeed så är den lika med MinFallSpeed
        //Om SPACE knappen trycks och variabeln touchfloor är sann
        //Gör gravitys Y-värde till -15
        //Addera karaktärens y värde med gravitys Y-värde
        //Kör funktionen jump mechanics
        //Variabeln toucfloor är lika med falsk
    }

    public void isColliding()
    {
        BlockObject.loadBlocks();
        for (var i = 0; i < BlockObject.floors.Count; i++)
        {
            blockEntity floor = BlockObject.floors[i];
            if (!Raylib.CheckCollisionRecs(PlayerAssets.hitBox, floor.cellBlock))
            {
                Variable.touchFloor = false;
            }
            else if (Raylib.CheckCollisionRecs(PlayerAssets.hitBox, floor.cellBlock))
            {
                Variable.touchFloor = true;
                break;
            }
        }
        //För varje block i listan BlockObject.floors
        //Om karaktärens hitbox inte kolliderar med något av objekten i listan Floors
        //Variabeln touchfloor är lika med falsk
        //Om karaktärens hitbox kolliderar med något av objekten i listan Floors
        //Variabeln touchfloor är lika med sann
        //Bryt sedan loopen
    }

    public void jumpMechanics()
    {
        Variable.gravity.Y = -10f;
    }


    //Gammal metod från Theo
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

    //Praktiskt sätt samma som metoden över fast med bestämda "hopp"
    public float skippingY(float characterY)
    {

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
        {
            characterY += 90;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
        {
            characterY -= 90;
        }

        if (characterY < 400)
        {
            characterY = 490;
        }
        else if (characterY > 490)
        {
            characterY = 400;
        }
        return characterY;
    }

    public void resetVars()
    {
        Variable.gravity.Y = 0;
        PlayerAssets.characterRec.y = 0;
        PlayerAssets.characterRec.x = Variable.screenWidth / 2;
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

        //Om D-knappen är nere samtidigt som A-knappen är nere
        //Variabeln bothButtonsPressed är sann
        //Variabeln isMoving är falsk
        //Annars är variabeln bothbuttonspressed falsk
    }
}
