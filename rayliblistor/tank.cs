using Raylib_cs;

public class Tank
{
    public Rectangle rect;

    public int health = 5; // en int för tank health
    public Texture2D image; // tanks textur

    public static bool getAmmo = false; // bool getAmmo som börjar på falskt, när den är sann får spelaren ammo
    public static float tankTimer = 30; // float tankTimer som börjar på 30, vilket innebär att det tar 30 sekunder tills en tank kommer
    public static float timeBetweenTanks = 30; // float tiden mellan nya tanks
    public static List<Tank> tanks = new List<Tank>(); // en lista för tanks
    public static List<Tank> tanksToRemove = new List<Tank>(); // en lista för tanks som ska tas bort
    static int l = 0; // int för antalet tanks

    //metod som skapar tanks 
    public static void Spawn() // lägger til en ny tank i listan tanks när tankTimer är mindre än 0
    {
        if (tankTimer < 0)
        {
            tanks.Add(new Tank()); // lägger till ny tank i listan tanks
            tankTimer = timeBetweenTanks;
        }
    }
    public static void Dead() // metod för tanks som dör  
    {
        foreach (Tank t in tanks)
        {
            if (t.health <= 0) // fi-stas som kollar ifall tanks hp är lika med eller mindre än 0
            {
                tanksToRemove.Add(t); // lägger till tanks i listan tanksToRemove
                Bullet.ammo += 10; // antalet ammo ökar med 10
                Zoey.kills++; // antalet kills ökar med 1

            }
        }
    }

    public static void Clear() // en metod som rensar listan tanksToRemove från tanks
    {
        foreach (Tank t in tanksToRemove)
        {
            tanks.Remove(t);
        }
    }

    public static void Count() // en metod som håller koll på antalet tanks
    {
        foreach (Tank it in tanks)
        {
            l += 1;
        }
    }

    public static void Direction(Zoey zoey) // en metod som bestämmer tanks färdriktning mot zoey
    {
        foreach (Tank e in tanks)
        {
            Vector2 zpos = new Vector2(zoey.rect.x, zoey.rect.y);
            Vector2 epos = new Vector2(e.rect.x, e.rect.y);

            Vector2 diff = zpos - epos;
            diff = Vector2.Normalize(diff);

            e.rect.x += diff.X;
            e.rect.y += diff.Y;
        }
    }
    public static void Attack(Zoey zoey) // ifall zoey och tank kolliderar så läggs tank i listan tankToRemove och zoey tappar hp
    {
        foreach (Tank t in Tank.tanks)
        {
            // if sats som kollar ifall gubben och någon av enemies i listan kolliderar med varandra. 
            if (Raylib.CheckCollisionRecs(zoey.rect, t.rect))
            {
                Tank.tanksToRemove.Add(t);
                Zoey.health -= 80;
                break;
            }
        }
    }

    public static void Update(Zoey zoey) // kallar på alla tanks logik-metoder
    {
        Spawn();
        Direction(zoey);
        Count();
        Attack(zoey);
        Clear();
        Dead();
    }

    public static void DrawAll() //ritar alla tanks
    {
        foreach (Tank t in tanks)
        {
            t.Draw();
        }
    }

    public Tank() // en konstruktor för tank
    {
        image = Raylib.LoadTexture("tank.png");
        rect = new Rectangle(1650, 450, 500, 450);
    }



    public void Draw()  //metod som gör så man kan rita upp tanks
    {
        Raylib.DrawTexture(
            image,
            (int)rect.x,
            (int)rect.y,
            Color.WHITE
            );
    }


}
