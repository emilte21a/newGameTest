using System;
using Raylib_cs;

public class TextureManager
{

    Dictionary<string, Texture2D> TexturesList = new Dictionary<string, Texture2D>();
    //En dictionary med nyckeln av datatypen string och valutan av datatypen Texture2D


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
        //Om TexturesList dictionaryt innehåller parametern filename
        //returnera värdet på Textureslist med nyckeln filename

        //Annars 
        //Gör Texture2D texture detsamma som att ladda upp texturen med parametern filename som inmatning
        //Texturlistan med nyckeln filename är lika med texture
        //returnera texture
    }

}