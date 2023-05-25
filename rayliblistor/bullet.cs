using Raylib_cs;

public class Bullet
{
    public Rectangle rect;
    public Texture2D image;

    public static List<Bullet> bullets = new List<Bullet>();
    public static List<Bullet> bulletsToRemove = new List<Bullet>();
    public static bool ammoLeft = true;
    public static double delay = 0f;
    public static int ammo = 3;
    public static bool shouldDrawText = false;
    public static float timerCount = 0;

    public static void Move()
    {
        foreach (Bullet b in bullets)
        {
            // flyttar skotten 7 pixel varje frame
            b.rect.x = b.rect.x + 7;
            if (b.rect.x > Raylib.GetScreenWidth())
            {
                bullets.Remove(b);
                break;
            }
        }
    }

    public static void Clear()
    {
        foreach (Bullet b in bulletsToRemove)
        {
            bullets.Remove(b);
        }
    }

    // det nedan ska gälla för alla skott i listan bullets. 
    public static void Collision()
    {
        foreach (Bullet b in Bullet.bullets)
        {
            // en foreach-loop som gäller för alla enemies i listan enemies, if-satsen kollar om dom kolliderar med nått av skåtten. om dom gör det läggs både skottet och zombien in i en lista för att tas bort. 
            foreach (Enemy e in Enemy.enemies)
            {
                if (Raylib.CheckCollisionRecs(b.rect, e.rect))
                {
                    // lägger till bullets och enemies till listan för att tas bort
                    bulletsToRemove.Add(b);
                    // kills++;
                    e.health--;
                    break;
                }
            }

            // en foreach-loop som gäller för alla tanks i listan tanks, if-satsen kollar om dom kolliderar med nått av skåtten. mm dom gör det läggs både skottet och tanken in i en lista för att tas bort. 
            foreach (Tank t in Tank.tanks)
            {
                if (Raylib.CheckCollisionRecs(b.rect, t.rect))
                {
                    bulletsToRemove.Add(b);
                    //kills++;
                    t.health--;
                    break;
                }
            }
        }
    }

    public static void Fire(Zoey zoey)
    {
        // gör så att man bara kan skjuta om man trycker space & delayen har passerat, när man skjuter så ritas skotten upp på zoeys position med lite extra pixlar år både x och y axeln för att det ska se ut som dom kommer från hennes vapen. det tas även bort 1 ammo från int'en ammo. 
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && Raylib.GetTime() >= delay && ammoLeft == true)
        {
            Bullet bullet = new Bullet();
            bullet.rect.x = zoey.rect.x + 130;
            bullet.rect.y = zoey.rect.y + 30;
            delay = Raylib.GetTime() + 2f;
            bullets.Add(bullet);
            ammo--;
        }
    }

    public static void Update(Zoey zoey)
    {
        Clear();
        Collision();
        Move();
        Fire(zoey);
    }

    // laddar in texturen till bullet
    public Bullet()
    {
        image = Raylib.LoadTexture("bullet.png");
        rect = new Rectangle(220, 410, 25, 25);
    }

    public static void Drawtext()
    {
        if (ammo <= 0 && Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) // håller koll ifall ammo är noll samtidigt som spelaren försöker skjuta. Ifall ammo är noll samtidigt som spelaren försöker skjuta får dom upp ett meddelande som berättar vad dom gör fel, alltså att deras ammo är slut. med hjälp av en timer så syns bara texten i två sekunder och dyker upp i yttligare två sekunder om man försöker skjuta igen. Ifall timern är >= 2 så försvinner texten och timer sätts tillbaka till noll igen. om timern inte är >= 0 så fortsätter timern att räkna tills den är det. 
        {
            ammoLeft = false;
            shouldDrawText = true;
        }
    }

    public static void NotDrawtext()
    {
        if (timerCount >= 2) // ifall timer count är större eller lika med 2 så ska shouldDrawText vara lika med falskt och timern ställs om till 0. ( detta gör så att texten bara syns i 2 sekunder)
        {
            shouldDrawText = false;
            timerCount = 0;
        }
        else // else som säger att när timerCount inte är större eller lika med 2 så ska den fortsätta räkna
        {
            timerCount += Raylib.GetFrameTime();
        }
    }
    public static void DrawAll()
    {
        foreach (Bullet b in bullets)
        {
            b.Draw();
        }

    }

    // en metid som gör så man kan rita upp bullets
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