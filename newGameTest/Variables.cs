using System;
using Raylib_cs;
using System.Numerics;


public class Variable
{
    public const int screenHeight = 1050;
    public const int screenWidth = 1300;

    public static Vector2 gravity = new(0, 0);

    public static bool touchFloor = false;

    public static float skyPlacementX = 1;

    public static int frame = 1;

    public static int punchFrame = 1;

    public static int punchColorAlpha;
    public static int punchRectWidth = 0;
    public static int punchTimer = 0;

    public static int timer = 1;
    public static int timer2 = 1;

    public static int way = 1;

    public static int whilePunching = 0;
    public static bool bothButtonsPressed = false;

    public static bool isMoving = true;

    public static bool isPunching = false;

    public static int amountOfWood = 0;
}



