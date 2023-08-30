using System.Collections.Generic;

namespace TextRPG_Team
{
    public class Program 
    {
        private static Character player;
        private static Character[] jobs;

        private static Monster[] monsters;

        private static Item[] Items;
        private static int ItemCount;
        private static int equipmentCount;


        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

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

                JsonUtility.Save(charList, "characterList");
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
            Items = new Item[10];

            // 아이템 추가
            AddItem(new Equipment("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 5, true, 500, 300));
            AddItem(new Equipment("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 9, false, 1500, 1000));
            AddItem(new Equipment("황금 갑옷", "황금으로 만들어져 튼튼한 갑옷입니다.", 0, 13, false, 2500, 2000));
            AddItem(new Equipment("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, false, 3500, 3000));
            if (player.Job == "전사")
            {
                AddItem(new Equipment("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 2, 0, true, 500, 300));
                AddItem(new Equipment("철제 검", "철로 만든 검입니다.", 5, 0, false, 1500, 300));
                AddItem(new Equipment("황금 검", "황금으로 만든 검입니다.", 8, 0, false, 2500, 300));
                AddItem(new Equipment("전설의 검", "전설의 전사가 사용했던 검입니다.", 11, 0, false, 3500, 300));
            }
            else if (player.Job == "궁수")
            {
                AddItem(new Equipment("낡은 활", "쉽게 볼 수 있는 낡은 활입니다.", 2, 0, true, 500, 300));
                AddItem(new Equipment("철제 활", "철로 만든 활입니다.", 5, 0, false, 1500, 1300));
                AddItem(new Equipment("황금 활", "황금으로 만든 활입니다.", 8, 0, false, 2500, 2300));
                AddItem(new Equipment("전설의 활", "전설의 궁수가 사용했던 활입니다.", 11, 0, false, 3500, 3300));
            }
            else if (player.Job == "마법사")
            {
                AddItem(new Equipment("낡은 지팡이", "쉽게 볼 수 있는 낡은 지팡이입니다.", 2, 0, true, 500, 300));
                AddItem(new Equipment("철제 지팡이", "철로 만든 지팡이입니다.", 5, 0, false, 1500, 1300));
                AddItem(new Equipment("황금 지팡이", "황금으로 만든 지팡이입니다.", 8, 0, false, 2500, 2300));
                AddItem(new Equipment("전설의 지팡이", "전설의 마법사가 사용했던 지팡이입니다.", 11, 0, false, 3500, 3300));
            }
            else if (player.Job == "도적")
            {
                AddItem(new Equipment("낡은 아대", "쉽게 볼 수 있는 낡은 아대입니다.", 2, 0, true, 500, 300));
                AddItem(new Equipment("철제 아대", "철로 만든 아대입니다.", 5, 0, false, 1500, 1300));
                AddItem(new Equipment("황금 아대", "황금으로 만든 아대입니다.", 8, 0, false, 2500, 2300));
                AddItem(new Equipment("전설의 아대", "전설의 도적이 사용했던 아대입니다.", 11, 0, false, 3500, 3300));
            }
            AddItem(new Consumable("HP 포션", "체력을 회복해주는 물약입니다.", player.HealHP, 30, 3, true, 200, 100));
            AddItem(new Consumable("MP 포션", "마나를 회복해주는 물약입니다.", player.HealMP, 30, 3, true, 200, 100));

            monsters = new Monster[]
            {
                new Monster("Lv.1 미니언", 1, 10, 3, 10, Items[0]),
                new Monster("Lv.2 미니언", 2, 15, 5, 20, Items[1]),
                new Monster("Lv.5 대포미니언", 5, 25, 8, 50, Items[2]),
                new Monster("Lv.3 공허충", 3, 10, 9, 30, Items[3])
            };
        
        }

        #region 아이템 관리

        public static void DropItem(List<Item> items)
        {
            foreach(var item in items)
            {
                AddItem(item);
            }
        }

        static void AddItem(Item item)
        {
            Items[ItemCount] = item;
            ++ItemCount;
            if (item.Type == ItemType.Equipment)
            {
                ++equipmentCount;
            }
            InvenSort();
        }

        static void EquipItem(Equipment item)
        {
            item.IsEquiped = true;
        }

        static void UnequipItem(Equipment item)
        {
            item.IsEquiped = false;
        }

        static void UseItem(Consumable item)
        {
            item.Consume();
            item.Count--;

            if (item.Count == 0)
            {
                int index = Array.IndexOf(Items, item);
                Items[index] = null;

                InvenSort(index);
                ItemCount--;
            }
        }

        private static void InvenSort(int sortIndex)
        {
            for (int i = sortIndex; i < ItemCount - 1; i++)
            {
                SwapItem(i, i + 1);
            }
        }

        private static void InvenSort()
        {
            // TODO : 장비, 회복 아이템 선택하는 창에서 장비와 회복 아이템을 분리해놓지 않으면 문제가 생김
            // 분리해서 정렬하는 로직이 필요
            for (int i = 0; i < ItemCount; i++)
            {
                if (i < equipmentCount)
                {
                    if (Items[i].Type == ItemType.Consumable)
                    {
                        SwapItem(i, ItemCount - 1);
                    }
                }
                else
                {
                    if (Items[i].Type == ItemType.Equipment)
                    {
                        SwapItem(i, equipmentCount - 1);
                    }
                }

            }
        }

        private static void SwapItem(int i, int j)
        {
            Item item = Items[i];
            Items[i] = Items[j];
            Items[j] = item;
        }

        public static int GetItemAtkAmount()
        {
            int itemAtk = 0;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null)
                    break;

                Item curItem = Items[i];
                if (curItem.Type == ItemType.Equipment)
                {
                    Equipment equipment = (Equipment)curItem;
                    if (equipment.IsEquiped)
                        itemAtk += equipment.Atk;
                }
            }

            return itemAtk;
        }

        static int GetItemDefAmount()
        {
            int itemDef = 0;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null)
                    break;

                Item curItem = Items[i];
                if (curItem.Type == ItemType.Equipment)
                {
                    Equipment equipment = (Equipment)curItem;
                    if (equipment.IsEquiped)
                    {
                        itemDef += equipment.Def;
                    }
                }
            }

            return itemDef;
        }
        #endregion


        #region 게임 화면 출력

        public static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 전투");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            BattleManager battle = new BattleManager(player, monsters, Items);

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;
                case 4:
                    battle.StartBattle(player);
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
            Console.WriteLine($"EXP: {player.CurrentExp}");
            Console.WriteLine();

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

            Console.WriteLine($"체력 : {player.CurrentHp}");
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

            // 초기 스킬이 하나일 경우
            // player = jobs[input - 1];
            // player.Skills.Add(characterList[input - 1].Skills[0]);
        }

        static void DisplayShop()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            for (int i = 0; i < Items.Length; i++)
            {
                Item curItem = Items[i];

                switch (curItem.Type)
                {
                    case ItemType.Equipment:
                        DisplayShopEquipments(curItem);
                        break;
                    case ItemType.Consumable:
                        Console.ResetColor();
                        Consumable consumable = (Consumable)curItem;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{curItem.Name} | ");
                        Console.Write($"개수 : {consumable.Count}");
                        Console.Write($" | {curItem.Description}");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"구매 금액 : {curItem.BuyGold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매 및 판매");
            Console.WriteLine("0. 나가기");
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
                    DisplayShopPurchaseOrSell();
                    break;
            }
        }

        //상점 구매 및 판매
        static void DisplayShopPurchaseOrSell()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            for (int i = 0; i < Items.Length ; i++)
            {
                Item curItem = Items[i];

                switch (curItem.Type)
                {
                    case ItemType.Equipment:
                        Console.Write($"{i + 1}. ");
                        DisplayShopEquipments(curItem);
                        break;
                    case ItemType.Consumable:
                        Console.ResetColor();
                        Consumable consumable = (Consumable)curItem;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{i + 1}. ");
                        Console.Write($"{curItem.Name} | ");
                        Console.Write($"개수 : {consumable.Count}");
                        Console.Write($" | {curItem.Description}");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"구매 금액 : {curItem.BuyGold}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                int input = CheckValidInput(0, Items.Length);
                if (input == 0)
                {
                    DisplayShop();
                    break;
                }
                else
                {
                    if (Items[input - 1].IsHave == true && Items[input - 1].Type == ItemType.Equipment)
                    {

                        player.Gold += Items[input - 1].SellGold;
                        Items[input - 1].IsHave = false;
                        Items[input - 1].IsEquiped = false;
                        DisplayShopPurchaseOrSell();
                    }
                    else if(Items[input - 1].IsHave == false || Items[input - 1].Type == ItemType.Consumable)
                    {
                        if (player.Gold < Items[input - 1].SellGold)
                        {
                            Console.WriteLine("Gold 가 부족합니다.");
                        }
                        else
                        {
                            player.Gold -= Items[input - 1].SellGold;
                            Items[input - 1].IsHave = true;
                            if (Items[input - 1].Type == ItemType.Consumable)
                            {
                                Consumable consumableItem = (Consumable)Items[input - 1];
                                consumableItem.Count++;
                            }
                            DisplayShopPurchaseOrSell();
                        }
                    }
                }
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

            for (int i = 0; i < Items.Length; i++)
            {
                if (!Items[i].IsHave)
                    continue;

                
                Item curItem = Items[i];

                switch (curItem.Type)
                {
                    case ItemType.Equipment:
                        DisplayEquipment(curItem);
                        break;
                    case ItemType.Consumable:
                        Console.ResetColor();
                        Consumable consumable = (Consumable)curItem;
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{curItem.Name} | ");
                        Console.Write($"개수 : {consumable.Count}");
                        Console.Write($" | {curItem.Description}");
                        Console.WriteLine();
                        break;
                }
            }
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 회복 아이템");
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 2);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    DisplayManageEquipment();
                    break;

                case 2:
                    DisplayConsumableItem();
                    break;
            }
        }

        private static void DisplayConsumableItem()
        {
            Console.Clear();

            DisplayTitle("회복");
            Console.WriteLine("포션을 사용하면 체력/마나를 회복할 수 있습니다.");
            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[아이템 목록]");

            int num = 1;
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null)
                    break;

                Item curItem = Items[i];

                if (curItem.Type == ItemType.Consumable)
                {
                    Console.Write($"{num} ");
                    dic.Add(num, i);
                    num++;
                    Consumable consumable = (Consumable)curItem;
                    Console.Write($"{curItem.Name} | ");
                    Console.Write($"개수 : {consumable.Count}");
                    Console.Write($" | {curItem.Description}");
                    Console.WriteLine();
                }
            }
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int consumableStartIdx = ItemCount == equipmentCount ? 0 : ItemCount - equipmentCount + 1;
            int consumableLength = ItemCount == equipmentCount ? 0 : ItemCount;
            int input = CheckValidInput(0, dic.Count);
            if (input == 0)
            {
                DisplayInventory();
            }
            else
            {
                Item curItem = Items[dic[input]];

                if (curItem.Type == ItemType.Consumable)
                {
                    Consumable consumable = (Consumable)curItem;
                    UseItem(consumable);
                }

                DisplayConsumableItem();
            }
        }

        private static void DisplayEquipment(Item curItem)
        {
            Equipment equipment = (Equipment)curItem;
            if (equipment.IsEquiped)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[E] ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write($"{curItem.Name} | ");
            if (equipment.Atk != 0) Console.Write($" 공격력 +{equipment.Atk} ");
            if (equipment.Def != 0) Console.Write($" 방어력 +{equipment.Def} ");
            Console.Write($" | {curItem.Description}");
            Console.WriteLine();
        }

        private static void DisplayShopEquipments(Item curItem)
        {
            Equipment equipment = (Equipment)curItem;
            
            Console.Write($"{curItem.Name} | ");
            if (equipment.Atk != 0) Console.Write($" 공격력 +{equipment.Atk} ");
            if (equipment.Def != 0) Console.Write($" 방어력 +{equipment.Def} ");
            Console.Write($" | {curItem.Description}"); 
            if (equipment.IsHave)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"판매 금액 : {equipment.SellGold}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"구매 금액 : {equipment.SellGold}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();
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

            int num = 1;
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null)
                    break;

                Item curItem = Items[i];

                if (curItem.Type == ItemType.Equipment && curItem.IsHave == true)
                {

                    Console.Write($"{num} ");
                    dic.Add(num, i);
                    num++;
                    Equipment equipment = (Equipment)curItem;

                    if (equipment.IsEquiped)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[E] ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write($"{curItem.Name} | ");
                    if (equipment.Atk != 0) Console.Write($" 공격력 +{equipment.Atk} ");
                    if (equipment.Def != 0) Console.Write($" 방어력 +{equipment.Def} ");
                    Console.Write($" | {curItem.Description}");
                    Console.WriteLine();
                }
            }
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, dic.Count);
            if (input == 0)
            {
                DisplayInventory();
            }
            else if (input > 0 && input <= equipmentCount)
            {
                Item curItem = Items[dic[input]];

                if (curItem.Type == ItemType.Equipment)
                {
                    Equipment equipment = (Equipment)curItem;
                    if (equipment.IsEquiped)
                        UnequipItem(equipment);
                    else
                        EquipItem(equipment);
                }

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
                    if ((ret >= min && ret <= max) || ret == 0)
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

        public static void DisplayError(string error)
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
    

