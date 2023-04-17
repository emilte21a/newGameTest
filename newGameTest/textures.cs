using Raylib_cs;
using System;

public class TextureClass
{
    public static List<Texture2D> charTextures = new();
    public static List<Texture2D> backgroundTextures = new();
    public static List<Texture2D> otherTextures = new();
    public static List<Texture2D> blockTextures = new();

    public static List<Texture2D> rockTextures = new();
    static TextureClass()
    {
        charTextures.Add(Raylib.LoadTexture("IMG/charTexture.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charTextureFall.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charTextureJump.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charRunningTexture.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/punchTexture.png"));
        charTextures.Add(Raylib.LoadTexture("IMG/charPickaxeAnim.png"));

        blockTextures.Add(Raylib.LoadTexture("IMG/blockTexture.png"));
        blockTextures.Add(Raylib.LoadTexture("IMG/dirtBlockTexture.png"));

        rockTextures.Add(Raylib.LoadTexture("IMG/rock.png"));
        rockTextures.Add(Raylib.LoadTexture("IMG/rock2.png"));

        backgroundTextures.Add(Raylib.LoadTexture("IMG/sky.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/mountains.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/parallaxbackground.png"));
        backgroundTextures.Add(Raylib.LoadTexture("IMG/clouds.png"));

        otherTextures.Add(Raylib.LoadTexture("IMG/inventoryspot.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/punchCoolDown.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/abilityFrame.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/tree.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/woodTexture.png"));
        otherTextures.Add(Raylib.LoadTexture("IMG/InventoryTexture.png"));
    }
}

