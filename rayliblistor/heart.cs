using Raylib_cs;


// Skapar en slumpgenerator och gör så att vi kan komma åt den med hjälp av variabeln "generator".

// Slumpar ett heltal mellan 0 och det största heltal int kan innehålla

public class heart
{
    public Rectangle rect;
    public Texture2D image;

    //metod som skapar enemies
    public heart()
    {
        Random generator = new Random();
        int x = generator.Next(936);
        image = Raylib.LoadTexture("heart.png");
        rect = new Rectangle(x, 0, 64, 64);
    }


    //metod som ritar upp enemies
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
