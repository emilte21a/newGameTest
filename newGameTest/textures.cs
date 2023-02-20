using Raylib_cs;
using System;




class TextureClass
{
    public List<Texture2D> charTextures = new();
    public List<Texture2D> backgroundTextures = new();
    public TextureClass()
    {
        charTextures.Add(Raylib.LoadTexture("IMG/charTexture.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/ground.png"));
    }
}

