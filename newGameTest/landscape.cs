using Raylib_cs;
using System;

public class rocks{

public Rectangle Rock; 

}



public class landscape
{
    const int levelwidth = 10;
    const int levelheight = 1;
    public static int cellsize = 120;

    public static List<rocks> grid = new();

    public static void loadBlocks()
    {
        for (int x = 0; x < levelwidth; x++)
        {
            for (int y = 0; y < levelheight; y++)
            {
                grid.Add(new rocks()
                {
                    Rock = new Rectangle(x*cellsize, 900, TextureClass.backgroundTextures[0].width, TextureClass.backgroundTextures[0].height)
                }
                );
                
            }

        }

    }

}

