using Raylib_cs;
using System;

public class Methods
{

public static void meleeMethod(){
     if (Variable.whilePunching == 0)
        {
            Variable.punchFrame = 1;
            Variable.whilePunching = 0;
        }   

        if (Variable.punchTimer > 0)
        {
            Variable.punchRectWidth+=2;
            Variable.punchTimer-=2;
        }

        if (Variable.punchTimer == 0)
        {
            Variable.punchColorAlpha = 0;
        }       
}



}