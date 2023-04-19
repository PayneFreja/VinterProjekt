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
}