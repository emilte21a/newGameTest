using System;
using Raylib_cs;

public class TextureManager{

Dictionary<string, Texture2D> TexturesList = new Dictionary<string, Texture2D>();

public Texture2D LoadTexture(string filename)
{
if (TexturesList.ContainsKey(filename))
{
    return TexturesList[filename];
}

else
{
    Texture2D texture = Raylib.LoadTexture(filename);
    TexturesList[filename] = texture;
    return texture;
}

}

}