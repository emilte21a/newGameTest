using Raylib_cs;
using System;

public class Rectangles
{

    public List<Rectangle> floors = new();

    public Rectangles()
    {
        floors.Add(Floor);
        floors.Add(Floor2);
        floors.Add(Floor3);
    }

    public static Rectangle Floor = new Rectangle(-1300, 900, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[0].height);
    public static Rectangle Floor2 = new Rectangle(0, 900, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[0].height);
    public static Rectangle Floor3 = new Rectangle(1300, 900, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[0].height);

    public static Rectangle hitBox = new Rectangle(characterProperties.characterRec.x, characterProperties.characterRec.y + 179, characterProperties.characterRec.width, 3);



}

