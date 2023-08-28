public class BattleManager
{
    private Character player;
    private List<Monster> monsters;

    //생성자
    public BattleManager(Character player, List<Monster> monsters)
    {
        this.player = player;
        this.monsters = monsters;
    }

    //전투의 시작과 진행, 각각의 턴을 번갈아가며 실행.
    public void StartBattle()
    {
        while (true)
        {
            PlayerTurn();

            if (IsBattleOver())
            {
                break;
            }

            MonstersTurn();

            if (IsBattleOver())
            {
                break;
            }
        }

        DisplayBattleResult();
    }

    //플레이어 턴 동작
    private void PlayerTurn()
    {
        int input = Program.CheckValidInput(0, monsters.Count);
        if (input == 0)
        {
            Program.DisplayBattle();
        }

        Monster targetMonster = monsters[input - 1];

        if (!targetMonster.IsDead) //살아있는 몬스터만 공격
        {
            int atkPower = CalculateAttackPower(player.Atk + Program.GetItemAtkAmount()); //아이템으로 인한 추가 수치 기준으로 계산
            targetMonster.Hp -= atkPower; //적 체력 = 기존 체력 - 공격력

            DisplayAttackResult(player.Name, targetMonster.Name, atkPower, targetMonster.Hp); //공격 결과
            
            if (targetMonster.IsDead)
            {
                Console.WriteLine($"{targetMonster.Name}이(가) 죽었습니다!");
                Console.WriteLine();
            }
        }
        else
        {
            Program.DisplayError("잘못된 입력입니다.");
            PlayerTurn();
        }
    }

    //몬스터 턴 동작
    private void MonstersTurn()
    {
        foreach (var monster in monsters) 
        {
            if (!monster.IsDead) //살아있는 몬스터는 순차적으로 플레이어 공격
            {
                int atkPower = CalculateAttackPower(monster.Atk);
                player.Hp -= atkPower;
                DisplayAttackResult(monster.Name, player.Name, atkPower, player.Hp);
                if (player.IsDead)
                {
                    break;
                }
            }
        }
    }

    //최종 공격력을 계산
    private int CalculateAttackPower(int baseAttack)
    {
        double errorRange = 0.1; // 10% 오차 범위
        double minMultiplier = 1 - errorRange; //최소 변동 배율
        double maxMultiplier = 1 + errorRange; //최대 변동 배율

        //범위 내의 랜덤한 값을 곱하여 변동 배율 결정
        double randomMultiplier = new Random().NextDouble() * (maxMultiplier - minMultiplier) + minMultiplier;
        //기본 공격력에 랜덤한 변동 배율을 곱해 최종 공격력 계산(반올림하여 정수 형태로)
        int finalAttack = (int)Math.Ceiling(baseAttack * randomMultiplier);

        return finalAttack;
    }

    //전투가 종료되었는지 판단
    private bool IsBattleOver()
    {
        if (player.IsDead)
        {
            return true;
        }

        return monsters.All(monster => monster.IsDead); //모든 몬스터가 죽었는지 판단 후 전투가 끝났는지의 여부 반환
    }

    //공격 결과를 출력(공격자, 공격 대상, 데미지, 체력)
    private void DisplayAttackResult(string attacker, string target, int damage, int health)
    {
        Thread.Sleep(2000);
        Program.DisplayTitle($"{attacker}의 공격!");
        Console.WriteLine($"{target}을(를) 맞췄습니다.[데미지: {damage}]");
        Console.WriteLine();
        Console.WriteLine($"{target}");
        Console.WriteLine($"HP -> {health}");
        Console.WriteLine();
        Console.Write(">> ");
    }

    //전투 결과를 출력
    private void DisplayBattleResult()
    {
        if (player.IsDead)
        {
            Console.WriteLine("패배하셨습니다. 다음에 다시 도전하세요.");
        }
        else
        {
            Console.WriteLine("축하합니다! 승리하셨습니다!");
        }
    }
}
