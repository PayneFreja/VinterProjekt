using Raylib_cs;

public class tank
{
    public Rectangle rect;
    public Texture2D image;

    //metod som skapar zombies på en slumpmässig plats längs y axeln, med en rektangel på 100x100
    public tank()
    {
        image = Raylib.LoadTexture("tank.png");
        rect = new Rectangle(1650, 450, 500, 450);
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
