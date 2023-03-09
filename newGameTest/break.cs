using System;
using Raylib_cs;

public class breakTree
{
    static public int breakAmount = 25;
    public static int treeHealth = 100;
    public static int breakTreeMethod(){

        treeHealth-=breakAmount;
        return treeHealth;
    }

}
