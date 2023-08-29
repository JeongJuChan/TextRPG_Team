namespace TextRPG_Team
{
    internal class Program
    {
        private static Character player;
        private static Character[] jobs;

        private static Item[] inventory;
        private static int ItemCount;

        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        #region 초기화

        static void GameDataSetting()
        {
            CharacterSkills characterSkills = new CharacterSkills();


            //초기 캐릭터 정보 세팅
            player = new Character("초기값", "초기값", 1, 10, 5, 100, 50, 1500);

            #region 캐릭터 저장 및 로드
            var characterList = JsonUtility.Load<List<Character>>("characterList");

            //직업별 스킬 json 테이블이 있을 때 Load / 없으면 Save 처리
            if(characterList == null)
            {
                // 직업 정보 세팅
                jobs = new Character[4];
                jobs[0] = new Character("전사", "전사", 1, 5, 10, 100, 50, 1500);
                jobs[1] = new Character("궁수", "궁수", 1, 6, 9, 80, 70, 1500);
                jobs[2] = new Character("마법사", "마법사", 1, 9, 6, 80, 100, 1500);
                jobs[3] = new Character("도적", "도적", 1, 10, 5, 80, 70, 1500);

                // 스킬 정보 세팅
                List<Character> charList = new List<Character>(jobs);
                charList[0].Skills.Add(new SigleSkill("알파 스트라이크", $"공격력 * 2 로 하나의 적을 공격합니다.", 10, player.Atk, 2, characterSkills.AttackSigleTarget));
                charList[0].Skills.Add(new MultipleSkill("더블 스트라이크", "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.", 15, player.Atk, 1.5f, characterSkills.AttackMutipleTarget));

                charList[1].Skills.Add(new SigleSkill("라이징 샷", "공격력 * 2.25 로 하나의 적을 공격합니다.", 15, player.Atk, 2.25f, characterSkills.AttackSigleTarget));
                charList[1].Skills.Add(new MultipleSkill("한 발에 두 놈", "공격력 * 1.75 로 2명의 적을 랜덤으로 공격합니다.", 20, player.Atk, 1.75f, characterSkills.AttackMutipleTarget));

                charList[2].Skills.Add(new SigleSkill("파이어 볼", "공격력 * 2 로 하나의 적을 공격합니다.", 20, player.Atk, 2.5f, characterSkills.AttackSigleTarget));
                charList[2].Skills.Add(new MultipleSkill("메테오", "공격력 * 1.75로 모든 적을 공격합니다.", 25, player.Atk, 2f, characterSkills.AttackMutipleTarget));

                charList[3].Skills.Add(new SigleSkill("급소 베기", "공격력 * 2로 하나의 적을 공격합니다.", 15, player.Atk, 2f, characterSkills.AttackSigleTarget));
                charList[3].Skills.Add(new MultipleSkill("암기 던지기", "공격력 * 2로 두 명의 적을 공격합니다.", 20, player.Atk, 2f, characterSkills.AttackMutipleTarget));

                JsonUtility.Save(charList, "characterlist");
            }

            #endregion

            //캐릭터 데이터 불러오기
            Character Save_player = JsonUtility.Load<Character>("player");

            //게임 데이터 있을 경우 DisplayGameIntro 실행 / 없을 경우 DisplayCharacterCustom 실행
            if (Save_player == null)
            {
                DisplayCharacterCustom();
                JsonUtility.Save(player, "player");
            }
            else
            {
                player = Save_player;
            }

            // 인벤토리 생성
            inventory = new Item[10];

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
            Console.WriteLine("");
            Console.WriteLine("직업을 선택해주세요.");
            Console.WriteLine("1. 전사  2. 궁수  3. 마법사  4. 도적");
            Console.Write(">>");
            int input = CheckValidInput(1, 4);

            var characterList = JsonUtility.Load<List<Character>>("characterList");
            player = characterList[input - 1];
            player.Name = chrName;

            // 초기 스킬 하나일 경우
            // player = jobs[input - 1];
            // player.Skills.Add(characterList[input - 1].Skills[0]);
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

        #region 배틀구현 이후 추가할 메서드들 - 주찬

        int criticalPercentage = 15;
        int dodgePercentage = 10;
        float criticalMod = 1.6f;

        void DummyBattleLogic()
        {
            // 크리티컬 데미지
            float damage = 0;
            damage = IsCritical() ? GetCriticalDamage(damage) : damage;

            // 피하기 로직
            string attackSentence = "~을";
            attackSentence += IsDodged() ? GetDodgeSentence() : "";
        }

        bool IsCritical()
        {
            int randomPercentage = Utility.rand.Next(0, 100);
            if (randomPercentage < criticalPercentage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        float GetCriticalDamage(float damage)
        {
            return damage * criticalMod;
        }

        bool IsDodged()
        {
            int randomPercentage = Utility.rand.Next(0, 100);
            if (randomPercentage < dodgePercentage)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        public string GetDodgeSentence()
        {
            return "공격했지만 아무일도 일어나지 않았습니다.";
        }
        #endregion

    }
}
