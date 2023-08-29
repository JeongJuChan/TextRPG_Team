namespace TextRPG_Team
{
    public class Dungeon
    {
        public int dungeonLevel; // 현재 던전 층 수
        public int monsterCount; // 몬스터 수
        private Monster[] monsters; // 몬스터 배열
        private BattleManager battleManager;

        public Dungeon(int initialLevel, Monster[] monsterArray)
        {
            dungeonLevel = initialLevel;
            monsters = monsterArray;
            monsterCount = monsters.Length;
        }

        public void EnterDungeon()
        {

            BattleManager battle = new BattleManager(Program.player, monsters, this); // 던전 객체 전달
            battle.StartBattle(Program.player,this);
        }

        public void ClearDungeon()
        {
            dungeonLevel++;
            monsterCount++; // 더 많은 몬스터가 등장하도록 추가
            Console.WriteLine($"던전 {dungeonLevel-1}층을 클리어했습니다!\n");
            Console.WriteLine("계속하시겠습니까?");
            Console.WriteLine("1. 네");
            Console.WriteLine("2. 아니오");

            int input = Program.CheckValidInput(1, 2);
            switch (input)
            {
            case 1:
              
                EnterDungeon();
                break;

            case 2:
                Program.DisplayGameIntro();
                break;
            }
        }
    }
}

