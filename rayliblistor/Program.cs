/*
Göra så att gubben kan skjuta skott
Skapa en klass för bullets
Göra en lista för skotten så att man kan hänvisa till alla bullets i listan
Göra så att skotten hamnar på samma x position som gubben och hitta y position som är i bra höjd
Skapa delay mellan hur ofta man kan skjuta
Göra så att skott som  hamnar utanför skärmen och de som träffar en zombie tas bort
Göra så att zombiesarna tas bort (dör) när skotten träffat dom. 
Sen när det funkar göra så att det krävs 3 skott per vanlig zombie för att den ska dö.
Skapa en int som bestämmer hur mycket start ammo man har
Sen använda den för att programmera in att för varje skott man skjuter så får minus ett ammo
Göra en int som räknar kills och health på samma sätt
Göra så att om zombiesarna nuddar gubben så tappar den 10 health, och om den dödar en zombie får den 1 kill
Göra så att om gubben klarar sig till andra sidan av banan (skärmen) så tas man till nästa bana
Göra så att gubbens x position sätts i början av skärmen igen. 


*/


global using System.Numerics;
using Raylib_cs;

// Skapar ett fönster som är 1000 x 683px stort, döper den till Present fall
Raylib.InitWindow(1600, 900, "Base Defender");

// Sätter FPS till 60
Raylib.SetTargetFPS(60);

//---------------------------------------LADDAR IN BILDER----------------------------------------

//Laddar in homepage bilden
Texture2D home = Raylib.LoadTexture("homepage.png");
//Laddar in bakgrundsbilderna för de olika banorna
Texture2D bkg = Raylib.LoadTexture("background.png");
Texture2D bkg2 = Raylib.LoadTexture("background2.png");
Texture2D bkg3 = Raylib.LoadTexture("background3.png");
//Laddar in game over bilden
Texture2D snw = Raylib.LoadTexture("zombiegameover.png");




//---------------------------------------LISTOR FÖR BULLETS----------------------------------------

//Skapar en lista för bullets
List<Bullet> bullets = new List<Bullet>();
//Skapar en lista för bullets som ska tas bort
List<Bullet> bulletsToRemove = new List<Bullet>();

//---------------------------------------LISTOR FÖR ENEMIES----------------------------------------

//Skapar en lista för enemies
List<enemy> enemies = new List<enemy>();
//Skapar en lista för enemies som ska tas bort
List<enemy> enemiesToRemove = new List<enemy>();



// Skapar Zoey
Zoey zoey = new Zoey();



//---------------------------------------SPEL LOKIG----------------------------------------

// Kollar hur bred och hög skärmen är som spelet ska köras på
int screenWidht = Raylib.GetScreenWidth();
int screenHeight = Raylib.GetScreenHeight();

// Integers som skapar "score" och "life", de säger att kills ska starta som 0 och att liv ska börja som 1
int kills = 0;
int health = 100;
int ammo = 30;

// Sätter start delayen till 0
double delay = 0f;

// Skapar variabler för tiden det mellan när fienderna (presenterna) skapas
float timeBetweenEnemies = 5;
float enemyTimer = timeBetweenEnemies;

// En interger som säger att det ska ha skapats 10 fiender (presenter) innan svårigheten ökar
int enemiesBetweenDifficulties = 10;




// En bool som säger att påståendet game over är falskt
bool gameOver = false;

bool start = false;


string currentScene = "first";

// While loop som hela spelet körs i sålänge life är större än 0
while (!Raylib.WindowShouldClose())
{

    if (start == false)
    {

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            start = true;
        }

    }
    else if (start == true)
    {
        if (gameOver == false)
        {
            enemyTimer -= Raylib.GetFrameTime();

            if (Raylib.GetFrameTime() > 1200)
            {
                if (enemyTimer < 0)
                {
                    enemyTimer = 2f;
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
                enemiesBetweenDifficulties = 5;
            }

            if (timeBetweenEnemies < 0.5)
            {
                timeBetweenEnemies = 0.5f;
            }

            // flyttar zombiesarna en pixel varje frame
            foreach (enemy e in enemies)
            {
                Vector2 zpos = new Vector2(zoey.rect.x, zoey.rect.y);
                Vector2 epos = new Vector2(e.rect.x, e.rect.y);

                Vector2 diff = zpos - epos;
                diff = Vector2.Normalize(diff);

                e.rect.x += diff.X;
                e.rect.y += diff.Y;


            }

            //Flyttar skotten en pixel varje frame
            foreach (Bullet e in bullets)
            {
                e.rect.x = e.rect.x + 5;
                //Tar bort skotten som åker utanför fönstret

                if (e.rect.x > Raylib.GetScreenWidth())
                {
                    bullets.Remove(e);
                    break;
                }
            }

            if (zoey.rect.x > Raylib.GetScreenWidth())
            {
                zoey.rect.x = 10;

            }



            zoey.Move();


            int l = 0;

            foreach (enemy it in enemies)
            {
                l += 1;
            }




            foreach (Bullet bullet in bullets)
            {
                foreach (enemy enemy in enemies)
                {
                    if (Raylib.CheckCollisionRecs(bullet.rect, enemy.rect))
                    {
                        // Lägger till bullets och enemies till listan för att tas bort
                        bulletsToRemove.Add(bullet);
                        kills++;
                        enemy.health--;
                        break;
                    }
                }
            }

            //Foreach loop som säger att följande ska gälla för varje enemy i listan enemies 
            foreach (enemy enemy in enemies)
            {
                //If sats som kollar ifall gubben och någon av enemies i listan kolliderar med varandra. 
                if (Raylib.CheckCollisionRecs(zoey.rect, enemy.rect))
                {
                    //Lägger till enemies i listan för att tas bort
                    enemiesToRemove.Add(enemy);
                    //Tar bort ett liv
                    health -= 10;
                    break;
                }
            }


            foreach (enemy enemy in enemies)
            {
                if (enemy.health <= 0)
                {
                    enemiesToRemove.Add(enemy);
                }
            }

            foreach (enemy enemy in enemies)
            {
                if (enemy.health <= 0)
                {
                    enemiesToRemove.Add(enemy);
                }
            }

            if (zoey.rect.x > Raylib.GetScreenWidth() && currentScene == "first")
            {
                zoey.rect.x = 10;
                currentScene = "second";
                foreach (enemy enemy in enemies)
                {
                    enemiesToRemove.Add(enemy);
                }

            }

            if (zoey.rect.x > Raylib.GetScreenWidth() && currentScene == "second")
            {
                zoey.rect.x = 10;
                currentScene = "third";
                foreach (enemy enemy in enemies)
                {
                    enemiesToRemove.Add(enemy);
                }

            }

            // Tar bort bullets utanför loopen
            foreach (Bullet bullet in bulletsToRemove)
            {
                bullets.Remove(bullet);
            }

            foreach (enemy enemy in enemiesToRemove)
            {
                enemies.Remove(enemy);
            }





            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && Raylib.GetTime() >= delay)

            {
                Bullet bullet = new();
                bullet.rect.x = zoey.rect.x + 270;
                bullet.rect.y = zoey.rect.y + 70;
                delay = Raylib.GetTime() + 2f;
                bullets.Add(bullet);
                ammo--;
            }

            if (health <= 0)
            {
                gameOver = true;
            }
        }
    }
    //---------------------------------------SPEL GRAFIK----------------------------------------
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    if (start == false)
    {
        Raylib.DrawTexture(home, 0, 0, Color.WHITE);

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            start = true;
        }

    }
    else
    {
        if (currentScene == "first")
        {
            Raylib.DrawTexture(bkg, 0, 0, Color.WHITE);
        }
        else if (currentScene == "second")
        {
            Raylib.DrawTexture(bkg2, 0, 0, Color.WHITE);
        }

        else if (currentScene == "third")
        {
            Raylib.DrawTexture(bkg3, 0, 0, Color.WHITE);
        }




        // gör så att game over bilden kommer upp när man dör, koordinaterna centrerar bilde
        if (gameOver == true)
        {
            Raylib.DrawTexture(snw, screenWidht / 2 - snw.width / 2, screenHeight / 2 - snw.height / 2, Color.WHITE);
        }

        //Ritar upp zoey
        zoey.Draw();

        //Ritar upp varje enemy
        foreach (enemy e in enemies)
        {
            e.Draw();
        }

        //Ritar upp varje bullet
        foreach (Bullet e in bullets)
        {
            e.Draw();
        }

        Raylib.DrawText($"Kills:{kills}", 900, 35, 20, Color.BLACK);
        Raylib.DrawText($"Health:{health}", 800, 35, 20, Color.BLACK);
        Raylib.DrawText($"Ammo:{ammo}", 700, 35, 20, Color.BLACK);
    }
    Raylib.EndDrawing();
}