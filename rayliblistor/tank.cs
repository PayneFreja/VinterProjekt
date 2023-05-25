using Raylib_cs;

public class Tank
{
    public Rectangle rect;

    // en int som säger att tanks health är 5
    public int health = 5;
    public Texture2D image;

    public static bool getAmmo = false;
    public static float tankTimer = 10;
    public static float timeBetweenTanks = 10;
    public static List<Tank> tanks = new List<Tank>();
    public static List<Tank> tanksToRemove = new List<Tank>();
    static int l = 0;

    //metod som skapar tanks 
    public static void Spawn()
    {
        if (tankTimer < 0)
        {
            tanks.Add(new Tank());
            tankTimer = timeBetweenTanks;
        }
    }
    public static void Dead()
    {
        foreach (Tank t in tanks)
        {
            if (t.health <= 0)
            {
                tanksToRemove.Add(t);
                Bullet.ammo += 10;
            }
        }
    }

    public static void Clear()
    {
        foreach (Tank t in tanksToRemove)
        {
            tanks.Remove(t);
        }
    }

    public static void Count()
    {
        foreach (Tank it in tanks)
        {
            l += 1;
        }
    }

    public static void Direction(Zoey zoey)
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
    public static void Attack(Zoey zoey)
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

    public static void Update(Zoey zoey)
    {
        Spawn();
        Direction(zoey);
        Count();
        Attack(zoey);
        Clear();
        Dead();
    }

    public static void DrawAll()
    {
        foreach (Tank t in tanks)
        {
            t.Draw();
        }
    }

    public Tank()
    {
        image = Raylib.LoadTexture("tank.png");
        rect = new Rectangle(1650, 450, 500, 450);
    }


    //metod som gör så man kan rita upp tanks
    public void Draw()
    {
        Raylib.DrawTexture(
            image,
            (int)rect.x,
            (int)rect.y,
            Color.WHITE
            );
    }


}
