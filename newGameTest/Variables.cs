using System;
using Raylib_cs;
using System.Numerics;


public class Variable
{
    public const int screenHeight = 1050;
    public const int screenWidth = 1300;

    public static Vector2 gravity = new(0, 0);

    
}


public class Rectangles
{
    public static Rectangle Floor = new Rectangle(0, Variable.screenHeight-170, Variable.screenWidth, 200);

}

public class CharProp
{

    public static Rectangle characterRec = new Rectangle(60, 60, 100, 100);

    public static float speed = 4;
}