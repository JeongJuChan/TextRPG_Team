
#region 데이터

public class Item
{
    public string Name { get; }
    public string Description { get; }

    public int Atk { get; }
    public int Def { get; }

    public bool IsEquiped { get; set; }

    public Item(string name, string description, int atk, int def)
    {
        Name = name;
        Description = description;
        Atk = atk;
        Def = def;

        IsEquiped = false;
    }

}

#endregion
