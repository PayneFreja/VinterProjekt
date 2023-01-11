using Raylib_cs;

// Skapar ett fönster som är 1000 x 683px stort, döper den till Present fall
Raylib.InitWindow(1000, 683, "present fall");

// Sätter FPS till 60
Raylib.SetTargetFPS(60);

//
Texture2D bkg = Raylib.LoadTexture("background.png");

//
Texture2D snw = Raylib.LoadTexture("finalsnow.png");

//Lista för enemies
List<enemy> enemies = new List<enemy>();

//Lista för liv
List<heart> hearts = new List<heart>();

// Skapar santa
Santa santa = new Santa();

//--------------------------------LOGIK--------------------------------//

int screenWidht = Raylib.GetScreenWidth();
int screenHeight = Raylib.GetScreenHeight();



// En int som skapar "score" och säger att den ska  och "life", 
int score = 0;
int life = 5;

// En while loop som skapar en ny enemy (present) varje 2 sekunder och får dom att röra sig längst med y-axeln
float timeBetweenEnemies = 2;
float enemyTimer = timeBetweenEnemies;
int enemiesBetweenDifficulties = 10;
bool gameOver = false;

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
        if (l > 0)
        {
            if (Raylib.CheckCollisionRecs(santa.rect, enemies[0].rect))
            {
                score += 1;
                enemies.RemoveAll(e => e.rect.y >= santa.rect.y - 64);
            }
        }


        // Läser av fönstrets höjd och tar bort dom som nuddar botten av skärmen
        int removed = enemies.RemoveAll(e => e.rect.y > Raylib.GetScreenHeight() - 45);

        if (removed > 0)
        {
            life--;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && gameOver == true)
        {
            gameOver = false;
        }


        //--------------------------------GRAFIK--------------------------------//
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);
        Raylib.DrawTexture(bkg, 0, 0, Color.WHITE);

        Raylib.DrawText($"Score:{score}", 900, 45, 20, Color.BLACK);
        Raylib.DrawText($"Life:{life}", 900, 75, 20, Color.BLACK);

        santa.Draw();

        foreach (enemy e in enemies)
        {
            e.Draw();
        }


        /* if (life > 0)
         {
             if (removed > 0)
             {
                 life--;
             }
         }
         else
         {
             Raylib.DrawTexture(snw, 341, 500, Color.WHITE);
         }*/

        if (life <= 0)
        {
            gameOver = true;
            Raylib.DrawTexture(snw, screenWidht / 2 - snw.width / 2, screenHeight / 2 - snw.height / 2, Color.WHITE);
        }



        Raylib.EndDrawing();
    }
}
