using Raylib_cs;
using System;




class TextureClass
{
    public List<Texture2D> charTextures = new();
    public List<Texture2D> backgroundTextures = new();
    public TextureClass()
    {
        charTextures.Add(Raylib.LoadTexture("IMG/charTexture.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charTextureFall.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charTextureJump.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/ground.png"));
    }
}
