internal class Program
{
    private static Character player;
    private static Item[] inventory;
    private static int ItemCount;
    private static Monster[] monsters;
    private static int MonsterCount;

    static void Main(string[] args)
    {
        GameDataSetting();
        DisplayGameIntro();
    }

    #region 초기화

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        // 인벤토리 생성
        inventory = new Item[10];

        // 아이템 추가
        AddItem(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 5));
        AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 20, 0));

        monsters = new Monster[10];
        //몬스터 정보 세팅
        AddMonster(new Monster("미니언", 2, 5, 15));
        AddMonster(new Monster("공허충", 3, 9, 10));
        AddMonster(new Monster("대포 미니언", 5, 8, 25));
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

    public static int GetItemAtkAmount()
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

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 전투시작");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 3);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;

            case 3:
                DisplayBattle();
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

    public static int CheckValidInput(int min, int max)
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

    public static void DisplayTitle(string title)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(title);
        Console.ResetColor();
    }

    public static void DisplayError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }

    #endregion
    
    //몬스터 배열에 몬스터 추가
    static void AddMonster(Monster monster)
    {
        monsters[MonsterCount] = monster;
        ++MonsterCount;
    }

    //전투 화면 출력
    public static void DisplayBattle()
    {
        Console.Clear();
        DisplayTitle("Battle!");
        Console.WriteLine();

        // monsters 배열에서 몬스터를 랜덤하게 선택하여 battleMonsters 리스트에 추가
        int numMonsters = new Random().Next(1, 5);
        List<Monster> battleMonsters = new List<Monster>();

        for (int i = 0; i < numMonsters; i++)
        {
            int randomIndex = new Random().Next(0, MonsterCount);
            battleMonsters.Add(monsters[randomIndex]);
        }

        // 선택되어 전투에 참여하는 몬스터 표시
        for (int i = 0; i < battleMonsters.Count; i++)
        {
            Monster curMonster = battleMonsters[i];
            Console.WriteLine($"Lv.{curMonster.Level} {curMonster.Name} HP {curMonster.Hp}");
        }

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 전투시작");

        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;

            case 1:
                DisplayBattleStart(battleMonsters);
                break;
        }
    }

    //전투시작 화면
    static void DisplayBattleStart(List<Monster> battleMonsters)
    {
        Console.Clear();
        DisplayTitle("Battle!");
        Console.WriteLine();

        //BattleManager 객체 생성 및 플레이어와 전투에 참여하는 몬스터 리스트 전달
        BattleManager battle1 = new BattleManager(player, battleMonsters);

        for (int i = 0; i < battleMonsters.Count; i++)
        {
            Monster curMonster = battleMonsters[i];
            Console.Write($"{i + 1}. ");
            Console.WriteLine($"Lv.{curMonster.Level} {curMonster.Name} HP {curMonster.Hp}");
        }

        Console.WriteLine();
        Console.WriteLine("적 번호를 입력하여 대상을 공격하세요.");
        Console.WriteLine("0. 취소");

        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">> ");

        battle1.StartBattle();

    }
}

