using Raylib_cs;

public class Enemy
{
    public Rectangle rect;

    // en int som sätter enemy health till 2
    public int health = 2;
    public Texture2D image;

    public static float timeBetweenEnemies = 7;
    public static float enemyTimer = 7;
    public static int enemiesBetweenDifficulties = 5;

    public static List<Enemy> enemies = new List<Enemy>();
    public static List<Enemy> enemiesToRemove = new List<Enemy>();
    static int l = 0;

    // metod som spawner en enemy när enemytimer är lika med timebetweenenemies
    public static void Spawn()
    {
        if (enemyTimer < 0)
        {
            enemyTimer = timeBetweenEnemies;
            enemies.Add(new Enemy());
        }
    }

    // if satser som för spelet svårare genom att sänka tiden mellan zombisarna (enemies).
    // det är 7 sekunder mellan varje enemy men efter att 5 enemies har spawnat så multipliceras tiden med 0.8
    // då sänks tiden mellan varje zombie. 
    // den fortsätter att sänka tiden mellan varje zombies tills tiden är 0.5. Alltså så är minimum 0.5 sekunder mellan varje zombie. 

    public static void Difficulity()
    {
        if (enemyTimer < 0)
        {
            enemyTimer = timeBetweenEnemies;
            enemies.Add(new Enemy());
            enemiesBetweenDifficulties--;
        }

        if (enemiesBetweenDifficulties == 0)
        {
            timeBetweenEnemies *= 0.8f;
            enemiesBetweenDifficulties = 5;
        }

        if (timeBetweenEnemies < 0.5)
        {
            timeBetweenEnemies = 0.5f;
        }
    }
    public static void Dead()
    {
        foreach (Enemy e in enemies)
        {
            if (e.health <= 0)
            {
                enemiesToRemove.Add(e);

            }
        }
    }

    public static void Clear()
    {
        foreach (Enemy e in enemiesToRemove)
        {
            enemies.Remove(e);
        }
    }

    public static void Count()
    {
        foreach (Enemy it in enemies)
        {
            l += 1;
        }
    }

    public static void Direction(Zoey zoey)
    {
        foreach (Enemy e in enemies)
        {
            Vector2 zpos = new Vector2(zoey.rect.x, zoey.rect.y);
            Vector2 epos = new Vector2(e.rect.x, e.rect.y);

            Vector2 diff = zpos - epos;
            diff = Vector2.Normalize(diff);

            e.rect.x += diff.X;
            e.rect.y += diff.Y;
        }
    }

    public static void Attack(Zoey zoey)
    {
        //Foreach loop som säger att följande ska gälla för varje enemy i listan enemies 
        foreach (Enemy e in Enemy.enemies)
        {
            //If sats som kollar ifall gubben och någon av enemies i listan kolliderar med varandra. 
            if (Raylib.CheckCollisionRecs(zoey.rect, e.rect))
            {
                //Lägger till enemies i listan för att tas bort
                Enemy.enemiesToRemove.Add(e);
                Zoey.health -= 10;
                break;
            }
        }
    }

    public static void Update(Zoey zoey)
    {
        Spawn();
        Difficulity();
        Direction(zoey);
        Attack(zoey);
        Dead();
        Clear();
        Count();
    }

    //metod som skapar zombies på en slumpmässig plats längs y axeln, med en rektangel på 500x200 px
    public Enemy()
    {
        Random generator = new Random();
        int y = generator.Next(900);
        image = Raylib.LoadTexture("normalzombie.png");
        rect = new Rectangle(1500, y, 500, 200);
    }


    //metod som gör så man kan rita upp enemies
    public void Draw()
    {
        Raylib.DrawTexture(
            image,
            (int)rect.x,
            (int)rect.y,
            Color.WHITE
            );

    }

    public static void DrawAll()
    {
        foreach (Enemy e in enemies)
        {
            e.Draw();
        }
    }
}