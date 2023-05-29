using Raylib_cs;

public class Enemy
{
    public Rectangle rect;

    public int health = 2;   // en int som sätter enemy health till 2
    public Texture2D image;  // texturen för enemy
    public static float timeBetweenEnemies = 7; // en float som bestämmer antalet sekunder mellan att enemies ska skapas
    public static float enemyTimer = 7; // antalet sekunder till den första enemyn skapas
    public static int enemiesBetweenDifficulties = 5; // en int som bestämmer antalet enemies tills svårighetsgraden ökas.

    public static List<Enemy> enemies = new List<Enemy>(); // skapar en lista för enemies
    public static List<Enemy> enemiesToRemove = new List<Enemy>(); // skapar en lista för enemies som ska tas bort
    static int l = 0; // int som håller koll på antalet enemies

    public static void Spawn() // metod som skapar en ny enemy
    {
        if (enemyTimer < 0) // if sats som kollar om enemytimer är mindre än noll. Nedanstående görs då.
        {
            enemyTimer = timeBetweenEnemies;
            enemies.Add(new Enemy()); // skapar en ny enemy
        }
    }

    // if satser som för spelet svårare genom att sänka tiden mellan zombisarna (enemies).
    // det är 7 sekunder mellan varje enemy men efter att 5 enemies har spawnat så multipliceras tiden med 0.8
    // då sänks tiden mellan varje zombie. 
    // den fortsätter att sänka tiden mellan varje zombies tills tiden är 0.5. Alltså så är minimum 0.5 sekunder mellan varje zombie. 

    public static void Difficulity() // metod som ökar svårigheten i spelet
    {
        if (enemyTimer < 0) // if-sats som kollar om enemytimer är mindre än noll. Nedanstående görs då.
        {
            enemyTimer = timeBetweenEnemies;
            enemies.Add(new Enemy()); // skapar ny enemy
            enemiesBetweenDifficulties--;
        }

        if (enemiesBetweenDifficulties == 0) // if-sats som kollar om inten enemiesbetweendifficulities är lika med noll. Nedanstående görs då. 
        {
            timeBetweenEnemies *= 0.8f; // gångrar timebetween enemies med o.8
            enemiesBetweenDifficulties = 5; // säger att
        }

        if (timeBetweenEnemies < 0.5) // if-sats som kollar om timebetween enemies är mindre än 0.5. Nedanstående görs då.
        {
            timeBetweenEnemies = 0.5f; // sätter timebetween enemies till 0.5. Så att spelet inte blir omöjligt.
        }
    }
    public static void Dead() // metod som kollar ifall inten health är lägre än eller lika med 0, och isådanafall läggs dom till i listan av enemies som ska tas bort
    {
        foreach (Enemy e in enemies)
        {
            if (e.health <= 0)
            {
                enemiesToRemove.Add(e);
                Zoey.kills++;
            }
        }
    }


    public static void Clear() // metod som tar bort enemies i listan enemiesToRemove
    {
        foreach (Enemy e in enemiesToRemove)
        {
            enemies.Remove(e);
        }
    }

    public static void Count() // metod som räknar enemies
    {
        foreach (Enemy e in enemies) // foreach för varje enemy i enemies
        {
            l += 1;
        }
    }

    public static void Direction(Zoey zoey) // en metod som bestämmer riktningen som enemies färdas i, mot zoey
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
            if (Game.CollisionCheck(zoey, e))
            {
                //Lägger till enemies i listan för att tas bort
                Enemy.enemiesToRemove.Add(e);
                Zoey.health -= 10;
                break;
            }
        }
    }

    public static void Update(Zoey zoey) // kallar på alla enemys logik-metoder
    {
        Spawn();
        Difficulity();
        Direction(zoey);
        Attack(zoey);
        Dead();
        Clear();
        Count();
    }
    public Enemy()  //metod som skapar zombies på en slumpmässig plats längs y axeln, med en rektangel på 500x200 px
    {
        Random generator = new Random();
        int y = generator.Next(900);
        image = Raylib.LoadTexture("normalzombie.png");
        rect = new Rectangle(1500, y, 500, 200);
    }
    public void Draw()  //metod som gör så man kan rita upp enemies
    {
        Raylib.DrawTexture(
            image,
            (int)rect.x,
            (int)rect.y,
            Color.WHITE
            );

    }

    public static void DrawAll() // en metod som ritar alla enemies
    {
        foreach (Enemy e in enemies)
        {
            e.Draw();
        }
    }
}