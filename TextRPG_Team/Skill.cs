using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team
{
    public class Skill
    {
        // 정보 MP 추가
        // 캐릭터에서 가져오기
        // 2. 스킬 항목 추가
        
        // TODO :
        // 직업에 맞는 스킬 추가
        // - 메서드 늘리기
        // Program에 있는 배틀 로직 배틀 쪽에 붙이기

        // GameData Settings 쪽에 Switch 문으로 캐릭터 선택과 생성이 완료 되면 Skill을 자동으로 추가

        public string Name { get; }
        public int Cost { get; }
        public string Description { get; }

        // 단일 타겟 스킬과 다중 타겟 스킬을 생성자로 나눴다.
        Action<string, float> skill;
        Action<List<string>, float> mutipleSkill;

        public Skill(string name, int cost, string description, Action<string, float> skillAction)
        {
            Name = name;
            Cost = cost;
            Description = description;
            skill = skillAction;
        }

        public Skill(string name, int cost, string description, Action<List<string>, float> mutipleSkill)
        {
            Name = name;
            Cost = cost;
            Description = description;
            this.mutipleSkill = mutipleSkill;
        }

    }
}
