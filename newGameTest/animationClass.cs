using Raylib_cs;
using System;

public class AnimationClass{
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

    public int swingingAnimation()
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
}