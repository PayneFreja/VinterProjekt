using Raylib_cs;


// Skapar en slumpgenerator och gör så att vi kan komma åt den med hjälp av variabeln "generator".

// Slumpar ett heltal mellan 0 och det största heltal int kan innehålla

public class Santa
{
    public Rectangle rect;
    public Texture2D image;

    //metod som skapar mannen med koordinaterna nedan
    public Santa()
    {
        image = Raylib.LoadTexture("man.png");

        rect = new Rectangle(400, 400, 100, 100);
    }

    //metod som ritar upp bilden av mannen
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
        float speed = 7;
        if (rect.x <= 0)
        {
            rect.x += 20;
        }
        if (rect.x >= Raylib.GetScreenWidth() - 100)
        {
            rect.x -= 20;
        }
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
            image = Raylib.LoadTexture("man.png");
        }
        // samma sak som åvan fast om man trycker på knappen A så rör sig gubben med speed åt vänster
        // då byter mannen riktning i bilden åt vänster

        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            rect.x -= speed;
            image = Raylib.LoadTexture("man2.png");
        }

        //     else if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        //     {
        //         rect.y -= speed;

        //     }
        //     else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        //     {
        //         rect.y += speed;

        //     }
    }










}