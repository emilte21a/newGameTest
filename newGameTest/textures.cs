using Raylib_cs;
using System;




class TextureClass
{
    public static List<Texture2D> charTextures = new();
    public static List<Texture2D> backgroundTextures = new();
    public static List<Texture2D> otherTextures = new();
    public TextureClass()
    {
        charTextures.Add(Raylib.LoadTexture("IMG/charTexture.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charTextureFall.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charTextureJump.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charRunningTexture.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/punchTexture.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/ground.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/sky.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/mountains.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/inventoryspot.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/punchCoolDown.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/abilityFrame.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/tree.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/woodTexture.png"));
    }
}

