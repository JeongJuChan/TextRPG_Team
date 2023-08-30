using System.Net.NetworkInformation;

namespace TextRPG_Team
{
    public class BattleManager
    {
        private Random Random = new Random();
        private Character player;
        private Monster[] monsters;
        private Character prevPlayer;
        private List<Monster> killedMonster;

        //생성자
        public BattleManager(Character player, Monster[] monsters, Item[] inventory)
        {
            this.player = prevPlayer = player;
            this.monsters = monsters;
            prevPlayer = new Character(player);
            killedMonster = new List<Monster>();

            ShuffleMonsters(); // 몬스터 배열
        }

        //전투 시작과 진행
        public void StartBattle(Character player)
        {
            while (true)
            {
                if (CheckBattleEnd())
                {
                    DisplayerResult();
                    Program.DisplayGameIntro();
                }
                else
                {
                    DisplayBattleScreen(false); //전투 화면 표시
                    int input = Program.CheckValidInput(0, 1);
                    switch (input)
                    {
                        case 0:
                            DisplayerResult();
                            Program.DisplayGameIntro();
                            break;

                        case 1:
                            DisplayBattleScreen(true);
                            break;
                    }

                    PlayerTurn();
                    MonsterTurn();
                }
            }

        }


        //몬스터 랜덤 출현
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

        //전투 시작화면
        public void DisplayBattleScreen(bool showMonsterNumbers)
        {
            Console.Clear();
            WriteColored("Battle!!", ConsoleColor.Yellow);
            Console.WriteLine();

            for (int i = 0; i < monsters.Length; i++)
            {
                Monster curMonster = monsters[i];

                if (showMonsterNumbers) //몬스터 숫자 활성화
                {
                    //사망한 몬스터 색상 변경
                    if (curMonster.IsDead)
                        WriteColored($"{i + 1}. {curMonster.Name} dead\n", ConsoleColor.Red);
                    else
                        Console.WriteLine($"{i + 1}. {curMonster.Name}  HP {curMonster.CurrentHp}");
                }
                else
                {
                    if (curMonster.IsDead)
                        WriteColored($"{curMonster.Name}  dead\n", ConsoleColor.Red);
                    else
                        Console.WriteLine($"{curMonster.Name}  HP {curMonster.CurrentHp}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"{player.Name} (Lv.{player.Level})");
            Console.WriteLine($"HP {player.CurrentHp}/{player.MaxHp}");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }

        //플레이어 턴 동작 로직
        void PlayerTurn()
        {
            Console.WriteLine();
            Console.WriteLine("대상을 선택해주세요.");
            Console.Write(">> ");
            int targetIndex = Program.CheckValidInput(1, monsters.Length) - 1;

            if (monsters[targetIndex].IsDead)
            {
                Program.DisplayError("잘못된 입력입니다.");
                PlayerTurn();
                return;
            }

            int damage = CalculateDamage(player.Atk + Program.GetItemAtkAmount()); //아이템 추가 공격력 반영
            monsters[targetIndex].CurrentHp -= damage;

            Console.WriteLine();
            Console.WriteLine($"{player.Name}의 공격!");
            Console.WriteLine($"{monsters[targetIndex].Name}을(를) 맞췄습니다. [데미지 : {damage}]");
            if (monsters[targetIndex].IsDead)
            {
                Console.WriteLine($"{monsters[targetIndex].Name}");
                Console.WriteLine("HP 0 -> Dead");
                killedMonster.Add(monsters[targetIndex]);
            }
            Console.WriteLine();
            Console.WriteLine("아무 키를 눌러 계속");
            Console.Write(">> ");
            Console.ReadLine();
        }

        //몬스턴 턴 동작 로직
        void MonsterTurn()
        {
            foreach (var monster in monsters)
            {
                if (!monster.IsDead)
                {
                    int damage = CalculateDamage(monster.Atk);
                    player.CurrentHp -= damage;

                    Console.WriteLine();
                    Console.WriteLine($"{monster.Name}의 공격!");
                    Console.WriteLine($"{player.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
                    if (player.CurrentHp <= 0)
                        break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("아무 키를 눌러 계속");
            Console.Write(">> ");
            Console.ReadLine();
        }
        public int CalculateDamage(int baseAtk)
        {
            double errorRange = 0.1; // 10% 오차 범위
            double minMultiplier = 1 - errorRange; //최소 변동 배율
            double maxMultiplier = 1 + errorRange; //최대 변동 배율

            //변동 범위의 크기(max-min) 내의 랜덤한 실수값을 곱하여 변동 배율 결정
            double randomMultiplier = new Random().NextDouble() * (maxMultiplier - minMultiplier) + minMultiplier;
            //기본 공격력에 랜덤한 변동 배율을 곱해 최종 공격력 계산(반올림하여 정수 형태로)
            int finalAttack = (int)Math.Ceiling(baseAtk * randomMultiplier);

            return finalAttack;
        }
        //전투 종료 체크
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

            if (allMonstersDead || player.CurrentHp <= 0)
            {
                return true;
            }

            return false;
        }

        void DisplayerResult()
        {
            Console.Clear();

            // 경험치 및 드랍 테이블 작성
            List<Table.ExpTable>? expTable = JsonUtility.Load<List<Table.ExpTable>>("ExpTable");

            foreach (var monster in killedMonster)
            {
                player.CurrentExp += monster.Level * 1;
                player.Gold += monster.Gold;
            }

            foreach (var exp in expTable)
            {
                if ((exp.id == player.Level) && (player.CurrentExp >= (exp.NeedEXP + exp.StackEXP)))
                    player.Level += 1;
            }

            WriteColored("Battle!! - Result", ConsoleColor.Yellow);
            Console.WriteLine();

            string resultHeader = killedMonster.Count!=0 ? "Victory" : "You Lose";
            Console.WriteLine(resultHeader);
            Console.WriteLine();

            Console.WriteLine($"던전에서 몬스터 {killedMonster.Count}마리를 잡았습니다.");
            Console.WriteLine();

            string[] prefix = new string[3];
            prefix[0] = "Lv.";
            if (prevPlayer.Level != player.Level)
                prefix[0] += $"{prevPlayer.Level} {prevPlayer.Name} -> ";

            prefix[1] = "HP ";
            if (prevPlayer.CurrentHp != player.CurrentHp)
                prefix[1] += $"{prevPlayer.CurrentHp} -> ";

            prefix[2] = "EXP ";
            if (prevPlayer.CurrentExp != player.CurrentExp)
                prefix[2] += $"{prevPlayer.CurrentExp} -> ";

            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine(prefix[0] + $"{player.Level} {player.Name}");
            Console.WriteLine(prefix[1] + $"{player.CurrentHp}");
            Console.WriteLine(prefix[2] + $"{player.CurrentExp}");
            Console.WriteLine();

            if (killedMonster.Count != 0)
            {
                List<Item> drops = new List<Item>();
                int dropGold = 0;
                foreach (var monster in killedMonster)
                {
                    drops.Add(monster.DropItem);
                    dropGold += monster.Gold;
                }

                Console.WriteLine("[획득 아이템]");
                Console.WriteLine($"{dropGold} Gold");
                foreach (var item in drops)
                {
                    Console.WriteLine($"{item.Name} - {drops.Count(x => { return x.Name == item.Name; })}");
                }
                Console.WriteLine();

                player.Gold += dropGold;
                Program.DropItem(drops);// 드랍 관련 처리
            }

            Console.WriteLine("아무 키를 눌러 계속");
            Console.Write(">> ");
            Console.ReadLine();
        }

        //텍스트 색상 지정
        static void WriteColored(string text, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = originalColor;
        }
    }
}

