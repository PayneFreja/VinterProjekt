global using System.Numerics;
using Raylib_cs;

// öppnar upp ett fönster 160o x 900 pixlar
Raylib.InitWindow(1600, 900, "Left4Dead PixelArt");
// sätter frames/second till 60
Raylib.SetTargetFPS(60);
//---------------------------------------LADDAR IN BILDER----------------------------------------
Texture2D home;
Texture2D bkg;
Texture2D snw;

// ger bilderna en förkortning så man inte behöver skriva ut hela texten varje gång
home = Raylib.LoadTexture("homescreen.png");
bkg = Raylib.LoadTexture("background.png");
snw = Raylib.LoadTexture("zombiegameover.png");

// skapar Zoey
Zoey zoey = new Zoey();

//---------------------------------------SPEL LOKIG----------------------------------------
int screenWidht = Raylib.GetScreenWidth();            // interger för skärmens bredd
int screenHeight = Raylib.GetScreenHeight();          // interger för skärmens höjd
// int kills = 0;                                        // interger för antal zombies man dödat
bool gameOver = false;                                // en bool som säger att gameOver är lika med falskt
bool start = false;                                   // en bool som säger att start är lika med falskt
string currentScene = "first";                        // en string som bestämmer att current scene är first
// float timerCount = 0;                                 // en float som säger att timercount börjar på 0               
int timeSinceStart()                                  // en metod som konverterar en double till en int för att räkna tiden sedan start
{
    double gameTime = Raylib.GetTime();
    int seconds = (int)gameTime;
    return seconds;
}

void DrawInts()
{
    Raylib.DrawText($"Kills:{Zoey.kills}", 1000, 35, 20, Color.WHITE);
    Raylib.DrawText($"Health:{Zoey.health}", 800, 35, 20, Color.WHITE);
    Raylib.DrawText($"Ammo:{Bullet.ammo}", 600, 35, 20, Color.WHITE);
    Raylib.DrawText($"Time: {timeSinceStart()}", 400, 35, 20, Color.WHITE);
}

void OutOfAmmo()
{
    if (Bullet.shouldDrawText == true) // if-sats som kollar om shouldDrawText är sant
    {
        Raylib.DrawText("Out of ammo", 750, 500, 20, Color.BLACK);  // ifall det är sant så ska texten "out of ammo" ritas upp på koordinaterna nedan i svart färg
        Bullet.NotDrawtext();
    }
}

while (!Raylib.WindowShouldClose())
{
    if (start == false && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT)) // if-sats som kollar ifall start är falsk samtidigt som man trycker ner på musen. om man gör det ska start vara true och spelet startas
    {
        start = true;
    }

    else if (start == true) // en else if som säger att start == true kan inte hända samtidigt som start == false, när start == true så händer allt nedanför
    {

        if (gameOver == false) // en if som säger att sålänge gameover är lika med falskt så ska spelet köras.
        {
            Enemy.enemyTimer -= Raylib.GetFrameTime();
            Tank.tankTimer -= Raylib.GetFrameTime();

            Bullet.Update(zoey);
            Enemy.Update(zoey);
            Tank.Update(zoey);
            zoey.Update();

            if (Zoey.health <= 0) // if-sats som säger att när zoeys health (health) är = eller < 0 så är game over = true, alltså förlorar man spelet. 
            {
                gameOver = true;
            }
        }
    }
    //---------------------------------------SPEL GRAFIK----------------------------------------
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    if (start == false) // if-sats som kollar om start = false alltså om man inte har startat spelet än och fortfarande är på homepagen när man är på homepagen så ritas homepage bilden upp och sedan väntar tills spelaren tyckt på skärmen för då blir start = true
    {
        Raylib.DrawTexture(home, 0, 0, Color.WHITE);

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            start = true;
        }

    }
    else // annars så ritas banan upp
    {
        if (currentScene == "first")
        {
            Raylib.DrawTexture(bkg, 0, 0, Color.WHITE);
        }

        if (gameOver == true) // gör så att game over bilden kommer upp när man dör, koordinaterna centrerar bilden
        {
            Raylib.DrawTexture(snw, screenWidht / 2 - snw.width / 2, screenHeight / 2 - snw.height / 2, Color.WHITE);
        }

        // ritar upp alla gubbar
        zoey.Draw();
        Enemy.DrawAll();
        Tank.DrawAll();
        Bullet.DrawAll();
        Bullet.Drawtext();
        OutOfAmmo();
        DrawInts();
    }

    Raylib.EndDrawing(); // avslutar drawing
}
