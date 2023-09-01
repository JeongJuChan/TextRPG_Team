# TextRPG_Team
> **팀 B05** <br/> **개발기간: 2023.08.28 ~ 2023.09.01**

## 프로젝트 소개
내배캠 프로그래밍 심화 팀과제 TextRPG_Team은 C# 콘솔 기반 텍스트 RPG 게임입니다.

## 팀 노션
https://www.notion.so/05-B05-53b435b6a2bb4701bb2d6a123cf94242

## 주요 기능🎮
---
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
---
---
## 기능 - 김나운
### 기본 전투 시스템
#### 몬스터 기본 구조
https://github.com/JeongJuChan/TextRPG_Team/blob/f4522378a36daeabf08f8c5a1e19aa0ac0efaa06/TextRPG_Team/Monster.cs#L3

#### 몬스터 랜덤 생성
https://github.com/JeongJuChan/TextRPG_Team/blob/ed9422af751cf961fb95e95dce5d14d5ab0af9d6/TextRPG_Team/BattleManager.cs#L68

#### 기본 공격 계산
https://github.com/JeongJuChan/TextRPG_Team/blob/ed9422af751cf961fb95e95dce5d14d5ab0af9d6/TextRPG_Team/BattleManager.cs#L274

#### 턴 전투
https://github.com/JeongJuChan/TextRPG_Team/blob/ed9422af751cf961fb95e95dce5d14d5ab0af9d6/TextRPG_Team/BattleManager.cs#L152
https://github.com/JeongJuChan/TextRPG_Team/blob/ed9422af751cf961fb95e95dce5d14d5ab0af9d6/TextRPG_Team/BattleManager.cs#L242

#### 스테이지 구성
https://github.com/JeongJuChan/TextRPG_Team/blob/ed9422af751cf961fb95e95dce5d14d5ab0af9d6/TextRPG_Team/BattleManager.cs#L68
---

---
## 게임 화면
 ![image](https://github.com/JeongJuChan/TextRPG_Team/assets/111439484/fac6cf32-f7b6-47b8-a466-bca6a69820fd)


 ![image](https://github.com/JeongJuChan/TextRPG_Team/assets/111439484/551ff808-1263-49c1-b252-f2aba1b40726)
---

## 개발 스택
### Environment
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-007ACC?style=for-the-badge&logo=Visual%20Studio&logoColor=white)
![Git](https://img.shields.io/badge/Git-F05032?style=for-the-badge&logo=Git&logoColor=white)
![Github](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=GitHub&logoColor=white)             
   

### Development
![CSharp](https://img.shields.io/badge/CSharp-000000?style=for-the-badge&logo=CSharp&logoColor=white)

## 아키텍쳐

### 디렉토리 구조
```bash

│  .gitignore
│  README.md
│  TextRPG_Team.sln
│  
├─.vs
│  ├─ProjectEvaluation
│  │      textrpg_team.metadata.v7.bin
│  │      textrpg_team.projects.v7.bin
│  │      
│  └─TextRPG_Team
│      ├─DesignTimeBuild
│      │      .dtbcache.v2
│      │      
│      ├─FileContentIndex
│      │  │  4f3297ba-dc56-480a-bd36-591381932bb7.vsidx
│      │  │  6c85da91-0afb-4768-a499-860d8aa811cd.vsidx
│      │  │  84b4ce97-0fb3-4548-b74f-de65090d8faa.vsidx
│      │  │  b8a7e04d-5e7a-4f89-943c-7961ef8c7379.vsidx
│      │  │  read.lock
│      │  │  
│      │  └─merges
│      └─v17
│              .futdcache.v2
│              
└─TextRPG_Team
    │  BattleManager.cs
    │  Character.cs
    │  CharacterSkills.cs
    │  Item.cs
    │  JsonUtility.cs
    │  Monster.cs
    │  Program.cs
    │  Skill.cs
    │  Table.cs
    │  TextRPG_Team.csproj
    │  Utility.cs
    │  
    ├─bin
    │  └─Debug
    │      │  characterList.json
    │      │  ExpTable.json
    │      │  player.json
    │      │  stage_progress.json
    │      │  
    │      └─net6.0
    │              Newtonsoft.Json.dll
    │              TextRPG_Team.deps.json
    │              TextRPG_Team.dll
    │              TextRPG_Team.exe
    │              TextRPG_Team.pdb
    │              TextRPG_Team.runtimeconfig.json
    │                 
```
