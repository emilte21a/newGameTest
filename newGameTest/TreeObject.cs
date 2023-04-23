using System;
using Raylib_cs;


public class TreeEntity
{
    public int treeHealth = 100;
    public Rectangle TreeRect;
    public int breakTreeMethod()
    {
        treeHealth -= Variable.Damage;
        return treeHealth;
    }
}


public class TreeObject
{
    int treePos;
    int amountOfTrees = 20;
    public List<TreeEntity> Trees = new();

    public void LoadTrees()
    {
        Trees.Clear();
        for (var i = 0; i < amountOfTrees; i++)
        {
            Variable.Rand = new Random();
            treePos = Variable.Rand.Next(-1000, 1000);
            Trees.Add(new TreeEntity()
            {
                TreeRect = new Rectangle(120 * i + i * treePos, 420, 50, TextureClass.otherTextures[3].height)
            });
        }

    }




}