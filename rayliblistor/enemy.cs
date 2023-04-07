using Raylib_cs;

public class enemy
{
    public Rectangle rect;

    public int health = 2;
    public Texture2D image;

    //metod som skapar zombies på en slumpmässig plats längs y axeln, med en rektangel på 100x100
    public enemy()
    {
        //Random generator = new Random();
        //int x = generator.Next(900);
        image = Raylib.LoadTexture("normalzombie.png");
        rect = new Rectangle(1650, 450, 500, 200);
    }


    //metod som ritar upp själva bilden av zombiesarna
    public void Draw()
    {
        Raylib.DrawTexture(
            image,
            (int)rect.x,
            (int)rect.y,
            Color.WHITE
            );
    }



    // ------------------------------------------------------------------------
    //Lista för enemies
    public int health = 100;
    public static List<enemy> enemies = new List<enemy>();
    public static List<enemy> enemiesToRemove = new List<enemy>();

    public static void deadenemy()
    {
        foreach (enemy enemy in enemies)
        {
            if (enemy.health <= 0)
            {
                enemiesToRemove.Add(enemy);
            }
        }
    }

    public static void attack()
    {
        foreach (enemy enemy in enemies)
        {
            // if sats som kollar ifall gubben och någon av enemies i listan kolliderar med varandra. 
            if (Raylib.CheckCollisionRecs(zoey.rect, enemy.rect))
            {
                // lägger till enemies i listan för att tas bort
                enemiesToRemove.Add(enemy);
                // tar bort ett liv
                health -= 10;
                break;
            }
        }
    }







}
