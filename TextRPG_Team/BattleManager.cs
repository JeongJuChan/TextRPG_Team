public class BattleManager
{
    private Random Random = new Random();
    private Character player;
    private Monster[] monsters;

    public BattleManager(Character player, Monster[] monsters)
    {
        this.player = player;
        this.monsters = monsters;
        ShuffleMonsters(); // 몬스터 배열 섞기
    }

    public void StartBattle(Character player)
    {
        
        while (true)
        {
            DisplayBattleScreen(); //전투 화면 표시
            int input = CheckValidInput(0, 1);

            if (input == 0)
                break;

            PlayerTurn();
            MonsterTurn();

            if (CheckBattleEnd())
                break;
        }

    }



    public void ShuffleMonsters()
    {
        int numberOfMonsters = Random.Next(1, 5); // 1부터 4까지 랜덤한 숫자
        List<Monster> newMonstersList = new List<Monster>();

        for (int i = 0; i < numberOfMonsters; i++)
        {
            int randomIndex = Random.Next(0, monsters.Length);
            Monster randomMonster = monsters[randomIndex].Clone(); // 몬스터를 복제하여 추가
            newMonstersList.Add(randomMonster);
        }

        monsters = newMonstersList.ToArray(); // 복제된 몬스터들을 배열로 변환하여 할당
    }

    public void DisplayBattleScreen()
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();

        foreach (var monster in monsters)
        {
            Console.WriteLine($"{monster.Name}  HP {monster.CurrentHp}");
        }

        Console.WriteLine();
        Console.WriteLine("[내정보]");
        Console.WriteLine($"{player.Name} (Lv.{player.Level})");
        Console.WriteLine($"HP {player.CurrentHp}/{player.MaxHp}");
        Console.WriteLine();
        Console.WriteLine("1. 공격");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
    }

    void PlayerTurn()
    {
        Console.WriteLine();
        Console.WriteLine("대상을 선택해주세요.");
        Console.Write(">> ");
        int targetIndex = CheckValidInput(1, monsters.Length) - 1;

        if (monsters[targetIndex].IsDead)
        {
            DisplayError("잘못된 입력입니다.");
            PlayerTurn();
            return;
        }

        int damage = player.CalculateDamage();
        monsters[targetIndex].CurrentHp -= damage;

        Console.WriteLine();
        Console.WriteLine($"{player.Name}의 공격!");
        Console.WriteLine($"{monsters[targetIndex].Name}을(를) 맞췄습니다. [데미지 : {damage}]");
        if (monsters[targetIndex].IsDead)
        {
            Console.WriteLine($"{monsters[targetIndex].Name}");
            Console.WriteLine("HP 0 -> Dead");
        }
        Console.WriteLine();
        Console.WriteLine("0. 다음");
        Console.Write(">> ");
        Console.ReadLine();
    }

    void MonsterTurn()
    {
        foreach (var monster in monsters)
        {
            if (!monster.IsDead)
            {
                int damage = monster.CalculateDamage();
                player.CurrentHp -= damage;

                Console.WriteLine();
                Console.WriteLine($"{monster.Name}의 공격!");
                Console.WriteLine($"{player.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
                if (player.CurrentHp <= 0)
                    break;
            }
        }

        Console.WriteLine();
        Console.WriteLine("0. 다음");
        Console.Write(">> ");
        Console.ReadLine();
    }

    bool CheckBattleEnd()
    {
        bool allMonstersDead = true;
        foreach (var monster in monsters)
        {
            if (!monster.IsDead)
            {
                allMonstersDead = false;
                break;
            }
        }

        if (allMonstersDead)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine("Victory");
            Console.WriteLine($"던전에서 몬스터 {monsters.Length}마리를 잡았습니다.");
            Console.WriteLine();

            Console.WriteLine($"{player.Name} (Lv.{player.Level})");
            Console.WriteLine($"HP {player.CurrentHp}/{player.MaxHp}");
            Console.WriteLine();

            return true;
        }

        if (player.CurrentHp <= 0)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine("You Lose");
            Console.WriteLine();
            Console.WriteLine($"{player.Name} (Lv.{player.Level})");
            Console.WriteLine($"HP {player.CurrentHp}/{player.MaxHp}");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.Write(">> ");
            Console.ReadLine();
            return true;
        }

        return false;
    }

    static void DisplayError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }

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
}