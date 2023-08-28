internal class Program
{
    private static Character player;
    private static Character[] jobs;
    private static Item[] inventory;
    private static int ItemCount;

    static void Main(string[] args)
    {
        GameDataSetting();
        //게임 데이터 있을 경우 DisplayGameIntro 실행 / 없을 경우 DisplayCharacterCustom 실행
        //DisplayGameIntro();
        DisplayCharacterCustom();
    }

    #region 초기화

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        // 인벤토리 생성
        inventory = new Item[10];
        
        //직업 정보 세팅
        jobs = new Character[4];
        jobs[0] = new Character(player.Name, "전사", 1, 5, 10, 100, 1500);
        jobs[1] = new Character(player.Name, "궁수", 1, 6, 9, 80, 1500);
        jobs[2] = new Character(player.Name, "마법사", 1, 9, 6, 80, 1500);
        jobs[3] = new Character(player.Name, "도적", 1, 10, 5, 80, 1500);

        // 아이템 추가
        AddItem(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 5));
        AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0));
    }

    #endregion

    #region 아이템 관리

    static void AddItem(Item item)
    {
        inventory[ItemCount] = item;
        ++ItemCount;
    }

    static void EquipItem(Item item)
    {
        item.IsEquiped = true;
    }

    static void UnequipItem(Item item)
    {
        item.IsEquiped = false;
    }

    static int GetItemAtkAmount()
    {
        int itemAtk = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
                break;

            Item curItem = inventory[i];

            if (curItem.IsEquiped)
                itemAtk += curItem.Atk;
        }

        return itemAtk;
    }

    static int GetItemDefAmount()
    {
        int itemDef = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
                break;

            Item curItem = inventory[i];

            if (curItem.IsEquiped)
                itemDef += curItem.Def;
        }

        return itemDef;
    }

    #endregion

    #region 게임 화면 출력
    static void DisplayCharacterCustom()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("원하시는 이름을 설정해주세요.");
        Console.Write(">>");
        string chrName = Console.ReadLine();
        player.Name = chrName;
        Console.WriteLine("직업을 선택해주세요.");
        Console.WriteLine("1. 전사  2. 궁수  3. 마법사  4. 도적");
        Console.Write(">>");
        int input = CheckValidInput(1, 4);
        switch (input)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                player = jobs[input - 1];
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        DisplayTitle("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");

        int itemAtk = GetItemAtkAmount();
        Console.Write($"공격력 :{player.Atk + itemAtk}");
        if (itemAtk != 0)
            Console.Write($"(+{itemAtk})");
        Console.WriteLine();

        int itemDef = GetItemDefAmount();
        Console.Write($"방어력 : {player.Def + itemDef}");
        if (itemDef != 0)
            Console.Write($"(+{itemDef})");
        Console.WriteLine();

        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }


    static void DisplayInventory()
    {
        Console.Clear();

        DisplayTitle("인벤토리");

        Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();

        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
                break;

            Item curItem = inventory[i];

            if (curItem.IsEquiped)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[E] ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write($"{curItem.Name} | ");
            if (curItem.Atk != 0) Console.Write($" 공격력 +{curItem.Atk} ");
            if (curItem.Def != 0) Console.Write($" 방어력 +{curItem.Def} ");
            Console.Write($" | {curItem.Description}");
            Console.WriteLine();
        }
        Console.ResetColor();

        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;

            case 1:
                DisplayManageEquipment();
                break;
        }
    }

    static void DisplayManageEquipment()
    {
        Console.Clear();

        DisplayTitle("인벤토리 - 장착 관리");
        Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();

        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
                break;

            Item curItem = inventory[i];

            Console.Write($"{i + 1} ");
            if (curItem.IsEquiped)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[E] ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write($"{curItem.Name} | ");
            if (curItem.Atk != 0) Console.Write($" 공격력 +{curItem.Atk} ");
            if (curItem.Def != 0) Console.Write($" 방어력 +{curItem.Def} ");
            Console.Write($" | {curItem.Description}");
            Console.WriteLine();
        }
        Console.ResetColor();

        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, ItemCount);
        if (input == 0)
        {
            DisplayInventory();
        }
        else if (input > 0 && input <= ItemCount)
        {

            Item curItem = inventory[input - 1];
            if (curItem.IsEquiped)
                UnequipItem(curItem);
            else
                EquipItem(curItem);

            DisplayManageEquipment();
        }
    }

    #endregion

    #region Utility

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            DisplayError("잘못된 입력입니다.");
        }
    }

    static void DisplayTitle(string title)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(title);
        Console.ResetColor();
    }

    static void DisplayError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }

    #endregion
}


#region 데이터

public class Character
{
    public string Name { get; set; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Gold { get; }

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }
}

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
