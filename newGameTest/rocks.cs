using System;
using Raylib_cs;
using System.Numerics;

public class rockEntity{

    public Rectangle rockRect;
}


public class rockObject{

static int rockpos;

static int amountOfRocks = 3;
public static int rockTexture;

public static List<rockEntity> Rocks = new();

public static void loadRocks()
{
    Rocks.Clear();
    for (var i = 0; i < amountOfRocks; i++)
    {
        Variable.Rand = new Random();
        rockpos = Variable.Rand.Next(-1000, 1000);
        rockTexture = Variable.Rand.Next(0, 2);
        Rocks.Add(new rockEntity()
        {
            rockRect = new Rectangle(i*rockpos, 720, TextureClass.rockTextures[rockTexture].width, TextureClass.rockTextures[rockTexture].height)
        });
    }

}

}