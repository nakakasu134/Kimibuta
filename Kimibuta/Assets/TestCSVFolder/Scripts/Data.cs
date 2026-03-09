public class Data
{
    public string name;
    public string type1;
    public string type2;
    public int HP;
    public int Attack;
    public int Defense;
    public int SpAttack;
    public int SpDefense;
    public int Speed;

    public Data()
    {
        name = "";
        type1 = "";
        type2 = "";
        HP = 0;
        Attack = 0;
        Defense = 0;
        SpAttack = 0;
        SpDefense = 0;
        Speed = 0;
    }

    public int Total
    {
        get
        {
            return HP + Attack + Defense + SpAttack + SpDefense + Speed;
        }
    }
}