using System;
using Raylib_cs;

public class TreeEntity
{

    public int treeHealth = 100;

    public Rectangle rect;

    public int breakTreeMethod()
    {
        treeHealth -= 25;
        return treeHealth;
    }
}


public class TreeObject
{

    public List<TreeEntity> Trees = new();

    public TreeObject()
    {
        Trees.Add(new TreeEntity() {rect = tree1});
        Trees.Add(new TreeEntity() {rect = tree2});
        Trees.Add(new TreeEntity() {rect = tree3});

    }

    public static Rectangle tree1 = new Rectangle(780, 420, TextureClass.otherTextures[3].width, TextureClass.otherTextures[3].height);
    public static Rectangle tree2 = new Rectangle(120, 420, TextureClass.otherTextures[3].width, TextureClass.otherTextures[3].height);
    public static Rectangle tree3 = new Rectangle(400, 420, TextureClass.otherTextures[3].width, TextureClass.otherTextures[3].height);



}
