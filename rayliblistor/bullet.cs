using Raylib_cs;

public class Bullet
{
    public Rectangle rect;
    public Texture2D image;

    public Bullet()
    {
        image = Raylib.LoadTexture("bullet.png");
        rect = new Rectangle(220, 510, 25, 25);
    }

    public void Draw()
    {
        Raylib.DrawTexture(
            image,
            (int)rect.x,
            (int)rect.y,
            Color.WHITE
            );
    }

    //-----------------------------------------------------------------------------

    //Lista för skotten
    public static List<Bullet> bullets = new List<Bullet>();
    public static List<Bullet> bulletsToRemove = new List<Bullet>();

    public static void deadbullets()
    {
        foreach (Bullet e in bullets)
        {
            e.rect.x = e.rect.x + 5;
            // Tar bort skotten som åker utanför fönstret

            if (e.rect.x > Raylib.GetScreenWidth())
            {
                bullets.Remove(e);
                break;
            }
        }
    }

}