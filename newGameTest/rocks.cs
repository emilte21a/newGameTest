using System;
using Raylib_cs;
using System.Numerics;

public class rockEntity{
    public Rectangle rockRect;
    public int rockHealth = 100;

    public int breakStoneMethod(){
        rockHealth-= Variable.Damage;
        return rockHealth;
    }

}


public class rockObject{

int rockpos;
int amountOfRocks = 10;
public int rockTexture;

public List<rockEntity> Rocks = new();

public void loadRocks()
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