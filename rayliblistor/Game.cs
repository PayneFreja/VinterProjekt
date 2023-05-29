using System;
using Raylib_cs;
public class Game
{
    public static bool CollisionCheck(Zoey zoey, Enemy enemy) // en metod som kollar kollision mellan zoey och enemy.
    {
        if (Raylib.CheckCollisionRecs(zoey.rect, enemy.rect)) // if-sats som kollar ifall kollisionen sker, returnera true, annars returnera false.
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static int timeSinceStart() // metod som konverterar en double till en int för att räkna tiden sedan start
    {
        double gameTime = Raylib.GetTime();
        int seconds = (int)gameTime; // konverterar "double gametime" till "int seconds"
        return seconds;
    }
    public static void DrawInts() // en metod som ritar spelets UI
    {
        Raylib.DrawText($"Kills:{Zoey.kills}", 1000, 35, 20, Color.WHITE);
        Raylib.DrawText($"Health:{Zoey.health}", 800, 35, 20, Color.WHITE);
        Raylib.DrawText($"Ammo:{Bullet.ammo}", 600, 35, 20, Color.WHITE);
        Raylib.DrawText($"Time: {timeSinceStart()}", 400, 35, 20, Color.WHITE);
    }

    public static void OutOfAmmo() // metod som gör nedanstående
    {
        if (Bullet.shouldDrawText == true) // if-sats som kollar om shouldDrawText är sant
        {
            Raylib.DrawText("Out of ammo", 750, 500, 20, Color.BLACK);  // ifall det är sant så ska texten "out of ammo" ritas upp på koordinaterna nedan i svart färg
            Bullet.NotDrawtext();
        }
    }

    public static void UpdateAll(Zoey zoey) // en metod som kallar på bullet, enemy, tank och zoeys update-metoder
    {
        Bullet.Update(zoey);
        Enemy.Update(zoey);
        Tank.Update(zoey);
        zoey.Update();
    }

    public static void DrawAll(Zoey zoey) // metod som kallar på alla motsvarande draw-metoder samt UI.
    {
        zoey.Draw();
        Enemy.DrawAll();
        Tank.DrawAll();
        Bullet.DrawAll();
        Bullet.Drawtext();
        OutOfAmmo();
        DrawInts();
    }


}
