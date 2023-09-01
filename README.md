# TextRPG_Team
## 팀 노션
https://www.notion.so/05-B05-53b435b6a2bb4701bb2d6a123cf94242

## 기능 - 주찬
### 스킬 기능
CharacterSkills에서 단일 타겟 스킬과 다중 타겟 스킬을 구현하였습니다.
https://github.com/JeongJuChan/TextRPG_Team/blob/14ac4f0c9784ba9bdbc5d8e73d39341bd23702c6/TextRPG_Team/CharacterSkills.cs#L3
Skill이라는 클래스를 만들어 타입 별 스킬을 등록할 수 있게 구현하였습니다.
https://github.com/JeongJuChan/TextRPG_Team/blob/14ac4f0c9784ba9bdbc5d8e73d39341bd23702c6/TextRPG_Team/Skill.cs#L3
배틀 로직과 합쳐 스킬을 적용하였습니다.
https://github.com/JeongJuChan/TextRPG_Team/blob/14ac4f0c9784ba9bdbc5d8e73d39341bd23702c6/TextRPG_Team/BattleManager.cs#L152
### 치명타 기능
전투 중 일정 확률로 치명타가 적용 되게 구현하였습니다.
https://github.com/JeongJuChan/TextRPG_Team/blob/14ac4f0c9784ba9bdbc5d8e73d39341bd23702c6/TextRPG_Team/BattleManager.cs#L398
### 회피 기능
전투 중 일정 확률로 회피가 적용 되게 구현하였습니다.
https://github.com/JeongJuChan/TextRPG_Team/blob/14ac4f0c9784ba9bdbc5d8e73d39341bd23702c6/TextRPG_Team/BattleManager.cs#L416
### 아이템 구현
아이템을 Equipment 타입과 Consumable 타입으로 구분해 값을 저장할 수 있도록 구현하였습니다.
https://github.com/JeongJuChan/TextRPG_Team/blob/14ac4f0c9784ba9bdbc5d8e73d39341bd23702c6/TextRPG_Team/Item.cs#L3
### 데이터 저장
Json으로 데이터를 저장하고 불러올 수 있게끔 하였습니다.
https://github.com/JeongJuChan/TextRPG_Team/blob/14ac4f0c9784ba9bdbc5d8e73d39341bd23702c6/TextRPG_Team/JsonUtility.cs#L6
유저 데이터를 저장, 로드하는 부분입니다.
https://github.com/JeongJuChan/TextRPG_Team/blob/14ac4f0c9784ba9bdbc5d8e73d39341bd23702c6/TextRPG_Team/Program.cs#L277
### 인벤토리 정렬, 확장
https://github.com/JeongJuChan/TextRPG_Team/blob/14ac4f0c9784ba9bdbc5d8e73d39341bd23702c6/TextRPG_Team/Program.cs#L211
