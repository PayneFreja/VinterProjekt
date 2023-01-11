using Raylib_cs;


// Skapar en slumpgenerator och gör så att vi kan komma åt den med hjälp av variabeln "generator".

// Slumpar ett heltal mellan 0 och det största heltal int kan innehålla

public class Santa
{
    public Rectangle rect;
    public Texture2D image;

    public Santa()
    {
        image = Raylib.LoadTexture("bigsantaright.png");

        rect = new Rectangle(100, 570, 64, 64);
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

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            rect.x += speed;
            image = Raylib.LoadTexture("bigsantaright.png");
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            rect.x -= speed;
            image = Raylib.LoadTexture("bigsantaleft.png");
        }

    }




}