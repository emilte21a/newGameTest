using System;
using Raylib_cs;
using System.Numerics;


public class Variable
{
    public const int screenHeight = 1050;
    public const int screenWidth = 1300;

    public static Vector2 gravity = new(0, 0);

    public static bool touchFloor = false;

   
    
}

public class Rectangles
{
    TextureClass t = new();
    public List<Rectangle> floors = new();
    public static Rectangle Floor = new Rectangle(0, 900, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[0].height);

    public static Rectangle hitBox = new Rectangle(CharProp.characterRec.x, CharProp.characterRec.y+179, CharProp.characterRec.width, 3);
}

public class CharProp
{

    public static Rectangle characterRec = new Rectangle(Variable.screenWidth / 2, TextureClass.backgroundTextures[0].height, TextureClass.charTextures[0].width, TextureClass.charTextures[0].height);
    public static float speed = 4;
}