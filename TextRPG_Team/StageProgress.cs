namespace TextRPG_Team
{
    using System;
    using System.IO;

    namespace TextRPG_Team
    {
        public class StageProgress
        {
            public int CurrentStage { get; set; } = 1;

            public static StageProgress Load()
            {
                return JsonUtility.Load<StageProgress>("stage_progress");
            }

            public void Save()
            {
                JsonUtility.Save(this, "stage_progress");
            }
        }
    }

}
