namespace Model
{
    public class Pickax
    {
        enum Name
        {
            x, y, z // todo
        }

        enum Strength
        {
             x = 0, y = 1 , z = 2, a = 3 //todo
        }

        public int Price { get; set; }
    }
}