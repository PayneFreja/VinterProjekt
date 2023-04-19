using Raylib_cs;

public class Zoey
{
    public Rectangle rect;
    public Texture2D image;
    public int health = 100;


    //metod som skapar mannen med koordinaterna nedan
    public Zoey()
    {
        image = Raylib.LoadTexture("zoey.png");

        rect = new Rectangle(10, 450, 100, 100);


    }

    //metod som ritar upp zoey
    public void Draw()
    {
        Raylib.DrawTexture(
            image,
            (int)rect.x,
            (int)rect.y,
            Color.WHITE
            );
    }

    // en metod som bestämmer hastigheten som mannen ska kunna röra sig med och if satser som gör så att mannen inte kan gå utanför skärmen
    public void Move()
    {
        float speed = 4;
        if (rect.x <= 0)
        {
            rect.x += 20;
        }
        // if (rect.x >= Raylib.GetScreenWidth() - 100)
        // {
        //     rect.x -= 20;
        // }
        if (rect.y >= Raylib.GetScreenHeight() - 230)
        {
            rect.y -= 20;
        }
        if (rect.y <= 10)
        {
            rect.y += 20;
        }

        // is sats som körs ifall man trcker på knappen D på tangenbordet. då rör sig gubben med speed åt höger 
        // samtidigt som detta byter även mannen rikting i bilden åt höger
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            rect.x += speed;

        }

        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            rect.x -= speed;

        }

        else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            rect.y -= speed;

        }

        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            rect.y += speed;

        }
    }
}