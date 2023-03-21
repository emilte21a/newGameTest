using Raylib_cs;
using System;


public class blockEntity
{
    public Rectangle cellBlock;

}

public class BlockObject
{
    const int levelwidth = 20;
    const int levelheight = 1;
    public static int cellsize = 120;

    public static List<blockEntity> floors = new();

    public static void loadBlocks()
    {
        for (int x = 1; x < levelwidth; x++)
        {
            for (int y = 0; y < levelheight; y++)
            {
                floors.Add(new blockEntity()
                {
                    cellBlock = new Rectangle(x * cellsize, 900, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[0].height)
                }

                );

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



