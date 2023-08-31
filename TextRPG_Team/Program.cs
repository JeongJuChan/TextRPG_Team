using System;
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

        // 1. Item을 상속받는 Equipment와 Consumable이 있고 이미 Item배열인 Items가 있는 현재 구조에서
        //    Equipment 배열과 Consumable 배열을 만들어 json으로 따로 관리하려 했으나 낭비라고 생각이 듬
        // 2. 그래서 둘의 데이터는 같이 관리하되 장착했는지 여부와 몇 개인지는 Item에 통합하여 관리하도록 함 (사용자 데이터 관련한 부분들만)


        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            //초기 캐릭터 정보 세팅
            player = new Character("초기값", "초기값", 1, 10, 5, 100, 50, 1500);

            #region 캐릭터 저장 및 로드

            var characterList = JsonUtility.Load<List<Character>>("characterList");

            //직업별 스킬 json 테이블이 있을 때 Load / 없으면 Save 처리
            if (characterList == null)
            {
                // 직업 정보 세팅
                jobs = new Character[4];
                jobs[0] = new Character("전사", "전사", 1, 5, 10, 100, 50, 1500);
                jobs[1] = new Character("궁수", "궁수", 1, 6, 9, 80, 70, 1500);
                jobs[2] = new Character("마법사", "마법사", 1, 9, 6, 80, 100, 1500);
                jobs[3] = new Character("도적", "도적", 1, 10, 5, 80, 70, 1500);

                // 스킬 정보 세팅
                List<Character> charList = new List<Character>(jobs);
                charList[0].Skills.Add(new Skill("알파 스트라이크", $"공격력 * 2 로 하나의 적을 공격합니다.", SkillType.SigleTarget, 10, 2));
                charList[0].Skills.Add(new Skill("더블 스트라이크", "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.", SkillType.MultipleTarget, 15, 1.5f, 2));

                charList[1].Skills.Add(new Skill("라이징 샷", "공격력 * 2.25 로 하나의 적을 공격합니다.", SkillType.SigleTarget, 15, 2.25f));
                charList[1].Skills.Add(new Skill("한 발에 두 놈", "공격력 * 1.75 로 2명의 적을 랜덤으로 공격합니다.", SkillType.MultipleTarget, 20, 1.75f, 2));

                charList[2].Skills.Add(new Skill("파이어 볼", "공격력 * 2 로 하나의 적을 공격합니다.", SkillType.SigleTarget, 20, 2.5f));
                charList[2].Skills.Add(new Skill("메테오", "공격력 * 1.75로 모든 적을 공격합니다.", SkillType.MultipleTarget, 25, 2f));

                charList[3].Skills.Add(new Skill("급소 베기", "공격력 * 2로 하나의 적을 공격합니다.", SkillType.SigleTarget, 15, 2f));
                charList[3].Skills.Add(new Skill("암기 던지기", "공격력 * 2로 두 명의 적을 공격합니다.", SkillType.MultipleTarget, 20, 2f, 2));

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

            // 아이템 로드
            Item[] itemData = JsonUtility.Load<Item[]>("ItemData");

            

            LoadUserData(itemData);

            



            monsters = new Monster[]
            {
                new Monster("Lv.1 미니언", 1, 10, 3, 10,  Items[8]),
                new Monster("Lv.2 미니언", 2, 15, 5, 20, Items[9]),
                new Monster("Lv.5 대포미니언", 5, 25, 8, 50, Items[8]),
                new Monster("Lv.3 공허충", 3, 10, 9, 30, Items[9])
            };

        }

        private static void LoadUserData(Item[] itemData)
        {
            // 유저 데이터
            string[] userData = JsonUtility.Load<string[]>("UserData");

            if (userData == null)
            {
                // 기본 아머 아이템 : 1001~1004
                // 직업 아이템
                // 전사 1005~1008
                // 궁수 1009~1012
                // 법사 1013~1016
                // 도적 1017~1020
                // 소비 아이템 1021~1022

                Items = itemData;
                ItemCount = Items.Length;

                List<Item> defulatItems = new List<Item>();
                EarnDefaultItemWithID(1001);
                EarnDefaultItemWithID(1021);
                EarnDefaultItemWithID(1022);

                switch (player.Job)
                {
                    case "전사":
                        EarnDefaultItemWithID(1005);
                        break;
                    case "궁수":
                        EarnDefaultItemWithID(1009);
                        break;
                    case "마법사":
                        EarnDefaultItemWithID(1013);
                        break;
                    case "도적":
                        EarnDefaultItemWithID(1017);
                        break;
                }

                return;
            }

            for (int i = 0; i < itemData.Length; i++)
            {
                for (int j = 0; j < userData.Length; j++)
                {
                    string[] userDataArr = userData[j].Split('_');
                    int userID = int.Parse(userDataArr[0]);
                    if (itemData[i].Id == userID)
                    {
                        // 아이템 데이터에서 기본 세팅 이후 유저 데이터 세팅
                        Item item = itemData[i];
                        item.Id = userID;
                        item.IsHave = bool.Parse(userDataArr[1]);
                        switch (item.Type)
                        {
                            case ItemType.Equipment:
                                // Id_IsHave_Atk_Def_IsEquiped 
                                item.EquipmentData = new Item.Equipment(int.Parse(userDataArr[2]), int.Parse(userDataArr[3]), bool.Parse(userDataArr[4]));
                                break;
                            case ItemType.Consumable:
                                // Id_IsHave_Count_Amount
                                item.ConsumableData = new Item.Consumable(int.Parse(userDataArr[2]), int.Parse(userDataArr[3]));
                                break;
                        }
                        
                        AddItem(item);
                    }
                }
            }
        }

        private static void EarnDefaultItemWithID(int id)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i].Id == id)
                {
                    Items[i].IsHave = true;
                }
            }
        }

        #region 아이템 관리

        public static void DropItem(List<Item> items)
        {
            foreach(var item in items)
            {
                AddItem(item);
            }

            SaveUserData();
        }

        //원래 코드
        static void AddItem(Item item)
        {
            if (Items.Length / 2 < ItemCount)
            {
                ExtendInventory();
            }

            if (Items.Contains(item) && item.Type == ItemType.Consumable)
            {
                int index = Array.IndexOf(Items, item);
                item.ConsumableData = Items[index].ConsumableData;
                Items[index] = item;
            }
            else
            {
                Items[ItemCount] = item;
                ++ItemCount;
                
                InvenSort();
            }

        }

        private static void SaveUserData()
        {
            string[] userData = new string[Items.Length];
            for (int i = 0; i < userData.Length; i++)
            {
                Item item = Items[i];
                userData[i] = $"{item.Id}_{item.IsHave}_";
                switch (item.Type)
                {
                    case ItemType.Equipment:
                        // Id_IsHave_Atk_Def_IsEquiped 
                        userData[i] += $"{item.EquipmentData.Atk}_{item.EquipmentData.Def}_{item.EquipmentData.IsEquiped}"; 
                        break;
                    case ItemType.Consumable:
                        // Id_IsHave_Count_Amount
                        userData[i] += $"{item.ConsumableData.Count}_{item.ConsumableData.Amount}"; 
                        break;
                }
            }

            JsonUtility.Save(userData, "UserData");
        }

        private static void ExtendInventory()
        {
            var newInven = new Item[Items.Length * 2];
            for (int i = 0; i < Items.Length; i++)
            {
                newInven[i] = Items[i];
            }

            Items = newInven;
        }

        static void EquipItem(Item.Equipment itemData)
        {
            itemData.IsEquiped = true;
        }

        static void UnequipItem(Item.Equipment itemData)
        {
            itemData.IsEquiped = false;
        }

        static void UseItem(Item.Consumable itemData)
        {
            player.HealHP(itemData.Amount);
            itemData.Count--;
            Console.WriteLine("회복을 완료했습니다.");

            if (itemData.Count == 0)
            {
                int index = Array.IndexOf(Items, itemData);
                Items[index] = null;

                InvenSort(index);
                ItemCount--;
            }


            SaveUserData();
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
            int equipmentCount = GetEquipmentCount();
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
                    Item.Equipment equipment = curItem.EquipmentData;
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
                    Item.Equipment equipment = curItem.EquipmentData;
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
            Console.WriteLine($"마나 : {player.CurrentMp}");
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
            for (int i = 0; i < ItemCount; i++)
            {
                Item curItem = Items[i];

                switch (curItem.Type)
                {
                    case ItemType.Equipment:
                        DisplayShopEquipments(curItem);
                        break;
                    case ItemType.Consumable:
                        Console.ResetColor();
                        Item.Consumable consumable = curItem.ConsumableData;
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
            for (int i = 0; i < ItemCount; i++)
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
                        Item.Consumable consumable = curItem.ConsumableData;
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
                        Items[input - 1].EquipmentData.IsEquiped = false;
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
                                Item.Consumable consumableItem = Items[input - 1].ConsumableData;
                                consumableItem.Count++;
                            }
                            DisplayShopPurchaseOrSell();
                        }
                    }

                    SaveUserData();
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

            for (int i = 0; i < ItemCount; i++)
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
                        Item.Consumable consumable = curItem.ConsumableData;
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

        public static void DisplayConsumableItem(bool isBattleState = false)
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
                    Item.Consumable consumable = curItem.ConsumableData;
                    Console.Write($"{curItem.Name} | ");
                    Console.Write($"개수 : {consumable.Count}");
                    Console.Write($" | {curItem.Description} | 회복 : {consumable.Amount}");
                    Console.WriteLine();
                }
            }
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, dic.Count);

            if (input == 0)
            {
                if (isBattleState)
                {
                    return;
                }

                DisplayInventory();
            }
            else
            {
                Item curItem = Items[dic[input]];

                if (curItem.Type == ItemType.Consumable)
                {
                    Item.Consumable consumable = curItem.ConsumableData;
                    UseItem(consumable);
                }

                if (isBattleState)
                {
                    return;
                }

                DisplayConsumableItem();
            }
        }

        private static void DisplayEquipment(Item curItem)
        {
            Item.Equipment equipment = curItem.EquipmentData;
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
            Item.Equipment equipment = curItem.EquipmentData;
            
            Console.Write($"{curItem.Name} | ");
            if (equipment != null)
            {
                if (equipment.Atk != 0) Console.Write($" 공격력 +{equipment.Atk} ");
                if (equipment.Def != 0) Console.Write($" 방어력 +{equipment.Def} ");
            }
            Console.Write($" | {curItem.Description}"); 
            if (curItem.IsHave)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"판매 금액 : {curItem.SellGold}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"구매 금액 : {curItem.SellGold}");
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
                    Item.Equipment equipment = curItem.EquipmentData;

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
            else if (input > 0 && input <= GetEquipmentCount())
            {
                Item curItem = Items[dic[input]];

                if (curItem.Type == ItemType.Equipment)
                {
                    Item.Equipment equipment = curItem.EquipmentData;
                    if (equipment.IsEquiped)
                        UnequipItem(equipment);
                    else
                        EquipItem(equipment);
                }

                SaveUserData();

                DisplayManageEquipment();
            }

        }

        private static int GetEquipmentCount()
        {
            int count = 0;
            foreach (var item in Items)
            {
                if (item.Type == ItemType.Equipment)
                {
                    count++;
                }
            }
            
            return count;
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


    }
}
    

