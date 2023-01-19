using Raylib_cs;

// Skapar ett fönster som är 1000 x 683px stort, döper den till Present fall
Raylib.InitWindow(1000, 683, "Base Defender");

// Sätter FPS till 60
Raylib.SetTargetFPS(60);

//Laddar in bakgrundsbilden
Texture2D bkg = Raylib.LoadTexture("zombiebackground.png");

//Laddar in game over bilden
Texture2D snw = Raylib.LoadTexture("zombiegameover.png");

//Lista för enemies
List<enemy> enemies = new List<enemy>();


// Skapar santa
Santa santa = new Santa();

//--------------------------------LOGIK--------------------------------//

// Kollar hur bred och hög skärmen är som spelet ska köras på
int screenWidht = Raylib.GetScreenWidth();
int screenHeight = Raylib.GetScreenHeight();

// Integers som skapar "score" och "life", de säger att kills ska starta som 0 och att liv ska börja som 1
int kills = 0;
int life = 1;

// Skapar variabler för tiden det mellan när fienderna (presenterna) skapas
float timeBetweenEnemies = 2;
float enemyTimer = timeBetweenEnemies;

// En interger som säger att det ska ha skapats 10 fiender (presenter) innan svårigheten ökar
int enemiesBetweenDifficulties = 10;

// En bool som säger att påståendet game over är falskt
bool gameOver = false;

// While loop som hela spelet körs i sålänge life är större än 0
while (!Raylib.WindowShouldClose())
{
    while (gameOver == false)
    {


        enemyTimer -= Raylib.GetFrameTime();



        if (Raylib.GetFrameTime() > 1200)
        {
            if (enemyTimer < 0)
            {
                enemyTimer = 1.2f;
                enemies.Add(new enemy());
            }
        }
        // if satser som för spelet svårare för varje 10 zombies som spawnar genom att sänka tiden emellan dem. 
        if (enemyTimer < 0)
        {
            enemyTimer = timeBetweenEnemies;
            enemies.Add(new enemy());
            enemiesBetweenDifficulties--;
        }

        if (enemiesBetweenDifficulties == 0)
        {
            timeBetweenEnemies *= 0.9f;
            enemiesBetweenDifficulties = 10;
        }

        if (timeBetweenEnemies < 0.5)
        {
            timeBetweenEnemies = 0.5f;
        }

        // flyttar zombiesarna en pixel varje frame
        foreach (enemy e in enemies)
        {
            e.rect.y++;

        }


        santa.Move();


        int l = 0;

        foreach (enemy it in enemies)
        {
            l += 1;
        }

        //if sats som kollar ifall zombiesarna nuddar mannen, ifall dom nuddar mannen så dör dom och man får e kill
        if (l > 0)
        {
            if (Raylib.CheckCollisionRecs(santa.rect, enemies[0].rect))
            {
                kills += 1;
                enemies.RemoveAll(e => e.rect.y >= santa.rect.y - 100);
            }
        }


        // Läser av fönstrets höjd (minus 200) då zombiesarna kommit in i basen och om dom gör det så dör man och det blir game over
        int removed = enemies.RemoveAll(e => e.rect.y > Raylib.GetScreenHeight() - 200);

        // om en zombie blir removead pga att den kom in i basen så försvinner sitt liv och man dör
        if (removed > 0)
        {
            life--;
        }

        //--------------------------------GRAFIK--------------------------------//
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);
        Raylib.DrawTexture(bkg, 0, 0, Color.WHITE);

        Raylib.DrawText($"Kills:{kills}", 900, 35, 20, Color.BLACK);

        santa.Draw();

        foreach (enemy e in enemies)
        {
            e.Draw();
        }

        // gör så att game over bilden kommer upp när man dör, koordinaterna centrerar bilde
        if (life <= 0)
        {
            gameOver = true;
            Raylib.DrawTexture(snw, screenWidht / 2 - snw.width / 2, screenHeight / 2 - snw.height / 2, Color.WHITE);
        }



        Raylib.EndDrawing();
    }
}
