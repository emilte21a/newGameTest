using Raylib_cs;
using System;



public class blockEntity
{
    public Rectangle cellBlock;

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

    /*
    public BlockObject()
    {
        floors.Add(new blockEntity() { cellBlock = Floor });

    }
    public static Rectangle Floor = new Rectangle(360, 900, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[0].height);
*/
}