using Raylib_cs;

public class enemy
{
    public Rectangle rect;
    public Texture2D image;

    //metod som skapar zombies på en slumpmässig plats längs y axeln, med en rektangel på 100x100
    public enemy()
    {
        Random generator = new Random();
        int x = generator.Next(900);
        image = Raylib.LoadTexture("zombie.png");
        rect = new Rectangle(x, 0, 100, 100);
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
