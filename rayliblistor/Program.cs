global using System.Numerics;
using Raylib_cs;




//ett sätt att göra koden mer effetkiv skulle kunna var att använda färre variabler, då de saktar ner programmet. Man skulle även kunna återanvända vissa funktioner, t.ex collisionfunktionerna, då de är samma funktion bara att mottagaren är utbytt. Nu finns det en collisionfunktion för varje objekt och zoey, t.ex tank och zoey, enemy och zoey osv. Istället hade jag velat skapa en generell collisionfunktion som sedan går att användas i olika situationer, istället för att skriva flera stycken.

// öppnar upp ett fönster 160o x 900 pixlar
Raylib.InitWindow(1600, 900, "Left4Dead PixelArt");
// sätter frames/second till 60
Raylib.SetTargetFPS(60);

Texture2D home;
Texture2D bkg;
Texture2D snw;

// ger bilderna en variabel så man inte behöver skriva ut hela texten varje gång
home = Raylib.LoadTexture("homescreen.png");
bkg = Raylib.LoadTexture("background.png");
snw = Raylib.LoadTexture("zombiegameover.png");

// skapar Zoey
Zoey zoey = new Zoey();
Game game = new Game();



int screenWidht = Raylib.GetScreenWidth();            // interger för skärmens bredd
int screenHeight = Raylib.GetScreenHeight();          // interger för skärmens höjd
bool gameOver = false;                                // en bool som säger att gameOver är lika med falskt
bool start = false;                                   // en bool som säger att start är lika med falskt
string currentScene = "first";                        // en string som bestämmer att current scene är first
//------------------------------------------------------------------------------------------------------------
while (!Raylib.WindowShouldClose())
{
    if (start == true) // en else if som säger att start == true kan inte hända samtidigt som start == false, när start == true så händer allt nedanför
    {
        if (gameOver == false) // en if som säger att sålänge gameover är lika med falskt så ska spelet köras.
        {
            Enemy.enemyTimer -= Raylib.GetFrameTime();  // subtraherar skillnaden i tid sen förra framen från inten enemyTimer
            Tank.tankTimer -= Raylib.GetFrameTime();   // subtraherar skillnaden i tid sen förra framen från inten tankTimer

            Game.UpdateAll(zoey); // metod som kör alla update funktioner
            if (Zoey.health <= 0) // if-sats som kollar om inten health (i klassen zoey) är mindre eller lika med noll. Nedanstående görs då.
            {
                gameOver = true; // boolen gameover sätts till true
            }
        }
    }
    //---------------------------------------SPEL GRAFIK----------------------------------------
    Raylib.BeginDrawing(); // börjar rita spelet
    Raylib.ClearBackground(Color.WHITE); // sätter bakgrunden till vit

    if (start == false) // if-sats som kollar om start är falskt. Nedanstående görs då.
    {
        Raylib.DrawTexture(home, 0, 0, Color.WHITE); // ritar upp hemskärmen

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT)) // if-sats som kollar ifall vänster mus knapp trycks. Nedanstående görs då.
        {
            start = true; // säger att boolen start blir true
        }
    }
    else // else som säger att när start inte är falskt så ska nedanstående göras.
    {
        if (currentScene == "first") // is-sats som kollar ifall stringen currentscene är "first"
        {
            Raylib.DrawTexture(bkg, 0, 0, Color.WHITE); // ritar upp bilden för scenen
        }

        if (gameOver == true) // if-sats som kollar om boolen gameover är sant. Nedanstående görs då.
        {
            Raylib.DrawTexture(snw, screenWidht / 2 - snw.width / 2, screenHeight / 2 - snw.height / 2, Color.WHITE); //  ritar upp game over bilden i mitten av skärmen 
        }

        Game.DrawAll(zoey); // metod som ritar alla DrawAll-metoder för tank, enemy, zoey. Ritar även spelets UI.
    }
    // avslutar drawing
    Raylib.EndDrawing();
}
