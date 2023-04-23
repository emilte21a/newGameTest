using System;
using Raylib_cs;
using System.Numerics;


public class playerAssets
{

    public static Rectangle characterRec = new Rectangle(Variable.screenWidth / 2, -1000, TextureClass.charTextures[0].width, TextureClass.charTextures[0].height);

    public static Rectangle hitBox = new Rectangle(6, +179, TextureClass.charTextures[0].width, 3);

    public static float speed = 4;
}

public class Player
{
    BlockObject BlockObject = new();
    playerAssets playerAssets = new();
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
            playerAssets.characterRec.y += Variable.gravity.Y;

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
                playerAssets.characterRec.y += Variable.gravity.Y;
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
        //För varje block i listan BlockObject.floors
        //Om karaktärens hitbox inte kolliderar med något av objekten i listan Floors
        //Variabeln touchfloor är lika med falsk
        //Om karaktärens hitbox kolliderar med något av objekten i listan Floors
        //Variabeln touchfloor är lika med sann
        //Bryt sedan loopen
        
    }

    public void jumpMechanics()
    {
        Variable.gravity.Y = -15f;
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

    //Praktiskt sätt samma som metoden över
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


    public int jumpAnimation()
    {
        if (Variable.touchFloor == true)  //Om variabeln touchfloor är sann. Returnera 0
        {
            return 0;
        }
        else if (Variable.touchFloor == false) //Om den är falsk
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
            return 4; //Annars returnera 4
        } 
        
        

    }

    public void resetVars()
    {
        Variable.gravity.Y = 0;
        playerAssets.characterRec.y = -500;
        playerAssets.characterRec.x = Variable.screenWidth / 2;
    }

    //Variabler för de olika animationerna
    int timer = 1;
    int timer2 = 1; //Två olika timers för att jag är lat
    int frame = 1;
    public int runningAnimation()
    {
        int maxFrames = 4;

        timer += 2; //Öka timer med 2 varje frame (60 gånger per sekund)

        if (timer > 20) //Om timer är större än 20
        {
            timer = 0; //gör timer till 0
            Variable.runningFrame++; //Öka runningframe med 1
        }
        Variable.runningFrame = Variable.runningFrame % maxFrames; 
        //Modulus gör så att runningFrame delas och blir 0 när den uppnår maxframes
        return Variable.runningFrame;
    }

    
    public int punchAnimation()
    {
        int maxFrames = 6;

        timer2 += 2;
        if (timer2 > 14)
        {
            timer2 = 0;
            Variable.punchFrame++;
        }

        Variable.punchFrame %= maxFrames;
        return Variable.punchFrame;
        //Samma som ovan
    }

    public int pickaxeAnimation()
    {
        int maxFrames = 10;
        timer2 += 2;

        if (timer > 12)
        {
            timer2 = 0;
            Variable.pickaxeFrame++;
        }
        Variable.pickaxeFrame %= maxFrames;
        return Variable.pickaxeFrame;
        //Samma som ovan
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
