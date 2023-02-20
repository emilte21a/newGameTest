using Raylib_cs;
using System;



public class Method
{

    void gravityMethod(){
        if (!Raylib.CheckCollisionRecs(CharProp.characterRec, Rectangles.Floor))
    {
       
        Variable.gravity.Y -= 0.5f;
        CharProp.characterRec.y -= Variable.gravity.Y;
    }
    if (CharProp.characterRec.y > 700){
            CharProp.characterRec.y = 700;
        }

    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
    {
        CharProp.characterRec.y -= 1;
        Variable.gravity.Y = 15f;
    }

    }


}