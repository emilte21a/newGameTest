using Raylib_cs;
using System.Numerics;
using System;
using System.Collections.Generic;


public class gameStates{

inventory inventoryManager = new inventory();
TextureManager textureManager = new TextureManager();
Player Player = new();
Methods Methods = new();
BlockObject BlockObject = new();
TreeObject TreeObject = new();
rockObject rockObject = new();
playerAssets playerAssets = new();
wood wood = new();
stone stone = new();
stick stick = new();
woodPickaxe woodPickaxe = new();
stoneAxe stoneAxe = new();
string currentScene = "start";
Color skyColor = new Color(115, 215, 255, 255);




public void Game(){

}


public void Start(){

}

public void Dead(){

}

}