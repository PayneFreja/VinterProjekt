using Raylib_cs;

public class Zoey
{
    public Rectangle rect;
    public Texture2D image;

    // interger som bestämmer att zoeys health är 100 från början
    public static int health = 100; // int för zoeys health
    public static int kills = 0; // int för kills 

    // en metod med if-satser som kollar om zoey går utanför skärmen och sedan flyttar hennes position 20 px åt andra hållet för att hålla henne innanför skärmen
    public void WallCollision()
    {
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
    }
    // en metod som bestämmer hastigheten som zoey rör sig med och if-satser som ser till att hon rör sig
    // åt till exempel höger när tangenten D trycks ner
    public void Move()
    {
        float speed = 3;

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

    public void Update() // en metod som kallar på zoeys rörelse- och väggkollisionsmetoder
    {
        Move();
        WallCollision();
    }

    // skapar zoeys rektangel på skriva koordinater och laddar in texturen hennes bild. 
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


}