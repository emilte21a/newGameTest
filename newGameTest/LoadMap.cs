using System;
using Raylib_cs;

//Entity är vad varje objekt ska ha för egenskaper som t.ex HP osv
public class TreeEntity
{
    public int treeHealth = 100;
    public Rectangle TreeRect;
    public int breakTreeMethod()
    {
        treeHealth -= Variable.Damage;
        return treeHealth;
    }
}

public class rockEntity
{
    public Rectangle rockRect;
    public int rockHealth = 100;

    public int breakStoneMethod()
    {
        rockHealth -= Variable.Damage;
        return rockHealth;
    }
}

public class blockEntity
{
    public Rectangle cellBlock;
}

public class TreeObject
{
    int treePos;
    int amountOfTrees = 20;
    public List<TreeEntity> Trees = new();

    public void LoadTrees()
    {
        Trees.Clear();
        for (var i = 0; i < amountOfTrees; i++)
        {
            Variable.Rand = new Random();
            treePos = Variable.Rand.Next(-1000, 1000);
            Trees.Add(new TreeEntity()
            {
                TreeRect = new Rectangle(120 * i + i * treePos, 420, 50, TextureClass.otherTextures[3].height)
            });
        }

    }
}

/*
För varje variabel i som är mindre än amountOfTrees
skapa en ny random
gör trädets position till en random inom intervallet -1000 till 1000
Lägg till en ny trädentity rektangel i listan Trees
*/

public class rockObject
{
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
                rockRect = new Rectangle(i * rockpos, 720, TextureClass.rockTextures[rockTexture].width, TextureClass.rockTextures[rockTexture].height)
            });
        }
    }
}

public class BlockObject
{
    const int levelwidth = 70;
    const int levelheight = 1;
    public int cellsize = 120;
    public List<blockEntity> floors = new();
    public void loadBlocks()
    {
        floors.Clear();
        for (int x = -levelwidth; x < levelwidth; x++)
        {
            for (int y = 0; y < levelheight; y++)
            {
                floors.Add(new blockEntity()
                {
                    cellBlock = new Rectangle(x * cellsize, y * cellsize + 900, TextureClass.blockTextures[0].width, TextureClass.blockTextures[0].height)
                });
            }
        }
    }

    //För varje int X som är mindre än negativa levelwidthen
    //För varje int Y som är mindre än levelheighten
    //Lägg till en ny blockEntity i listan floors
    
    public void drawDirtBlocks()
    {

        for (var x = -levelwidth; x < levelwidth; x++)
        {
            for (var y = 0; y < 2; y++)
            {
                Raylib.DrawTexture(TextureClass.blockTextures[1], x * cellsize, y * cellsize + 1020, Color.WHITE);
            }
        }
    }
}