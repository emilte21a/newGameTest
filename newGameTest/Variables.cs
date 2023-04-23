using System;
using Raylib_cs;
using System.Numerics;

public static class Variable
{
    //En statisk klass med globala variabler som används hela tiden i olika funktioner

    public const int screenHeight = 1050;
    public const int screenWidth = 1300;

    public static Vector2 gravity = new(0, 0);

    public static bool touchFloor = false;

    public static float skyPlacementX = 1;

    /*
        public static int frame = 1;
        public static int punchFrame = 1;
        public static int pickaxeFrame = 1;
    */

    public static int punchFrame = 0;
    public static int runningFrame = 1;

    public static int pickaxeFrame = 0;
    public static int punchColorAlpha;
    public static int punchRectWidth = 0;
    public static int punchTimer = 0;

    public static int FacingDirection = 1;
    //Vart karaktärspriten riktas mot. 
    //Höger: 1 / vänster: -1

    public static int Damage = 10;

    public static bool canBreakWood;
    public static bool canBreakStone;

    public static int whilePunching = 0;
    public static bool bothButtonsPressed = false;

    public static bool isMoving = true;

    public static bool isPunching = false;

    public static int amountOfWood = 0;

    public static int whichItem = 0;

    public static Random Rand = new Random();
}
