using Raylib_cs;
using System;




class TextureClass
{
    public static List<Texture2D> charTextures = new();
    public static List<Texture2D> backgroundTextures = new();
    public TextureClass()
    {
        charTextures.Add(Raylib.LoadTexture("IMG/charTexture.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charTextureFall.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charTextureJump.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charRunningTexture.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/ground.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/sky.png"));
    }
}

