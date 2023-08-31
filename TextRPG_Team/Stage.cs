namespace TextRPG_Team
{
    public class Stage
    {
        public Monster[] Monsters { get; private set; }

        public Stage(Monster[] monsters)
        {
            Monsters = monsters;
        }
    }

}
