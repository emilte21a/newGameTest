using Raylib_cs;
using System;

public class Methods
{
  
    Player Player = new();
    
    public static void meleeMethod()
    {
        if (Variable.whilePunching == 0)
        {
            Variable.punchFrame = 1;
            Variable.whilePunching = 0;
        }

        if (Variable.punchTimer > 0)
        {
            Variable.punchRectWidth += 2;
            Variable.punchTimer -= 2;
        }

        if (Variable.punchTimer == 0)
        {
            Variable.punchColorAlpha = 0;
        }
    }
    public void punchReturn()
    {
        //int punchFrame = Player.punchAnimation();
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_F) && !Variable.isMoving && Variable.gravity.Y == 0 && Variable.whilePunching == 0 && Variable.punchTimer == 0)
        {
            Variable.punchColorAlpha = 170;
            Variable.punchTimer = 100;
            Variable.whilePunching = 25;
            Variable.punchRectWidth = 0;
        }

        if (Variable.whilePunching > 0)
        {
            Variable.whilePunching--;
            Player.punchAnimation();
        }
        //return punchFrame;
    }

    public void parallaxEffect()
    {

        if (Raylib.IsKeyReleased(KeyboardKey.KEY_D) && Variable.isMoving == true || (Raylib.IsKeyDown(KeyboardKey.KEY_D) && Variable.isMoving == true))
        {
            Variable.FacingDirection = 1;
            Variable.skyPlacementX += 0.5f;
        }

        else if (Raylib.IsKeyReleased(KeyboardKey.KEY_A) && Variable.isMoving == true || (Raylib.IsKeyDown(KeyboardKey.KEY_A) && Variable.isMoving == true))
        {
            Variable.FacingDirection = -1;
            Variable.skyPlacementX -= 0.5f;
        }
    }
    
    


}