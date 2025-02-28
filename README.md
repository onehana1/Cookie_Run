
# 🏃🏻‍♂️ 유생의 질주 (Scholar Run)

![image](https://github.com/user-attachments/assets/57b6ae81-1812-4525-8001-9cff4c5d910d)


👉 **장원 급제를 목표로 과거 시험을 보러 가는 "유생"이 되어 시험장까지 달려보세요!**

🛤 젤리를 먹고, 퀴즈를 풀며 점수를 얻어 장원급제를 해보세요.

🕹 **무한 모드**와 **스토리 모드** 두 가지 모드가 준비되어 있습니다!

✨ 제작자들이 녹음한 재밌는 사운드가 잔뜩! 꼭 플레이 해보세요!

### **🎮 게임 플레이 링크**

[👉 플레이하기 (Unity WebGL)](https://play.unity.com/en/games/22fbbd25-6547-47d1-b2e0-f5f10620e5b1/web)

---

## 🤴🏻강태현과 Idle
![image](https://github.com/user-attachments/assets/dbe4d295-2481-461a-bfeb-65ed8107d973)

### 🛠️ 팀원: 강태현(팀장), 정하나, 박한나, 윤병철, 최한빈
### 📅 개발 기간: 2025.02.21 ~ 2025.02.28

---

## **🔧 기술 스택**

| 기술 | 사용 버전 |
| --- | --- |
| 🎮 엔진 | Unity 2022.3.17f1 |
| 💻 언어 | C# |
| 🔧 프레임워크 | .NET 8.0 |
| 🛠 개발 환경 | Visual Studio 2022 |

---

## **📜 게임 개요**

### **🏃 달려!**

![image](https://github.com/user-attachments/assets/df0b9871-5568-4975-8c98-4ba91bc07359)


- **Run 버튼 & Slide 버튼**으로 장애물을 피해 **과거시험장**까지 도착해야 합니다.
- 점수를 모아 장원급제에 도전하세요!

### 📜 무한 모드 & 스토리 모드

![image](https://github.com/user-attachments/assets/9e690a95-a0a2-4a24-963b-80b6bc208002)


- **무한 모드**: 제한 없이 계속 달릴 수 있습니다!
- **스토리 모드**: **3분 동안 15,000점 이상**을 획득해야 합니다.

### ❓ OX 퀴즈

![image](https://github.com/user-attachments/assets/77290e26-3e45-458f-a6d5-df5e1a661fd5)

- **달리면서 OX 퀴즈를 풀어 추가 점수를 획득하세요!**
- **맞출수록 보너스 점수 UP!**

---

# 🎮 캐릭터 컨트롤러

![플레이어](https://github.com/user-attachments/assets/244313e1-c288-496b-bb3e-066ad033c2d6)

![image](https://github.com/user-attachments/assets/d8f2b102-6ca6-4ada-964c-9df7685d1cb7)


- **점프 (Space)**, **슬라이드 (Shift)**
- **버튼 조작 지원**

### **🎯 캐릭터 기본 동작**

![image](https://github.com/user-attachments/assets/e2eb9bbe-1bde-438b-9391-353c2eff9741)


- 캐릭터는 **자동으로 이동**하며, 플레이어는 **점프 & 슬라이드**로 장애물을 회피
- `Raycast`를 활용하여 **지면 감지 및 착지 상태 자동 업데이트**

<details> <summary>🔎 자세히 보기</summary>
 
### **🛡 피격 시스템**

- `이벤트 (OnTakeDamage)`를 활용하여 **체력 UI 자동 업데이트**
- 
![체력바](https://github.com/user-attachments/assets/d1f85e0a-be74-4e06-9c8d-e0f8b34fb9e6)


hit될 때마다 hp에 따라 체력바 자동 업데이트

### **🛡 무적 효과**

![무적 효과](https://github.com/user-attachments/assets/b2c129a0-be75-4825-a138-6250ab12a570)


- `StartInvincibility()`를 적용하여 일정 시간 **무적 + 깜빡이는 애니메이션 연동**

### 🚁 낙사 방지 시스템

![낙사방지](https://github.com/user-attachments/assets/b6550880-8a92-4c5f-b5a5-fc1edd25094d)

특정 높이 아래로 떨어지면 **구조 애니메이션 실행 & 자동 복귀**

### 🧠 스킬 - OX 퀴즈

![퀴즈](https://github.com/user-attachments/assets/ebfec94b-a4ca-412b-b340-baab027b2024)

- `이벤트 (OnQuizUsed)`를 활용하여 **스킬 쿨타임 UI 자동 적용**
- **OX 퀴즈 도중 무적 & 스킬 애니메이션 실행**

</details>

---

# 🎁 아이템 시스템

### **🏃 질주 아이템**

![질주](https://github.com/user-attachments/assets/60479650-cb77-4ce4-8a4d-b37dfc79c407)

- **빠른 속도로 이동하며 장애물을 부술 수 있음!**
- `무적 기능`을 잠시 활성화하여 모든 장애물 파괴

### **🧲 자석 아이템**

![자석](https://github.com/user-attachments/assets/f534576e-9fbf-487e-bc79-9c993edd0f2c)


- 일정 범위 내 **아이템 자동 흡수**

### **🔍 거대화 아이템**
![거대화](https://github.com/user-attachments/assets/4ec4abcd-dd63-40a7-bb5d-367d0050b09a)

- **캐릭터가 커지면서 일정 시간 무적 상태**

### **❤️ 체력 회복 아이템**

![image](https://github.com/user-attachments/assets/d0ca23b8-5262-40cf-8162-483def5fd1b4)

![image](https://github.com/user-attachments/assets/bdc53c5e-db58-4063-8bb0-919018146a25)

- **체력을 회복할 수 있는 아이템**

<details> <summary>🔎 자세히 보기</summary>

![image](https://github.com/user-attachments/assets/c4b932a4-26e6-4ee3-befc-d9dfda6de118)

- baseState 클래스에 있는 "무적 기능"을 잠초하여 PlayerGalloping 클래스(질주 아이템)에 
적용시킴
- background = FindObjectOfType<BackGroundController>();
백그라운드컨트롤에서 moveSpeed 컴포넌트를 참조해서 작성
- 아이템을 사용하면 일정 시간동안 백그라운드가 이동하는 속도가 x2배가 됨

![image](https://github.com/user-attachments/assets/ce77ffad-871c-4c3e-a070-f1e2f7dc959c)


- 질주 기능의 활성화 및 종료, 무적 상태 적용, 장애물 파괴

![image](https://github.com/user-attachments/assets/ec1fcce1-fd8b-40a0-8721-cc7081446ed7)


- 무적인 상태에서 spriteRenderer 컬러의 투명도를 0.5 변경후 new Color(1, 1, 1, 1) 원래 색상으로 재 변경

</details>

---

# **🌍 맵 & 젤리 생성**

## 🛤 맵 생성 시스템

![맵생성2](https://github.com/user-attachments/assets/cab91672-83ff-4d6b-93a8-b02d3605d09c)

- 맵을 **자동 생성 & 삭제**하여 최적화
- 배경과 장애물 **동적 생성 시스템 구현**

![image](https://github.com/user-attachments/assets/2a2862cb-3c52-4a3b-9f16-a49b67d4273d)


<details>
<summary>🔎 자세히 보기</summary>

![image](https://github.com/user-attachments/assets/e772d8ac-2b73-4084-9577-6b342eec3261)


- 퍼스트엔드앵커는 큐에 앞에 저장된 맵의 엔드앵커이다.
- 퍼스트엔드앵커의 x좌표가 -10보다 작아지면 맵을 삭제한다.
- 라스트엔드앵커는 가장최근에 생성된 맵의 엔드앵커이다.(큐에 맨뒤에 위치)
- 라스트엔드앵커의 x좌표가 15보다 작아지면 맵을 생성한다.
- 라스트엔드앵커의 위치에 새로운 맵을 생성한다.

</details>


## 🍬 젤리 생성 시스템
![젤리](https://github.com/user-attachments/assets/42472ea0-bab8-417f-aa25-2cc59520ccf2)

- **자동으로 맵에 젤리 배치!**
- 플레이어가 먹으면 **점수 추가**
![image](https://github.com/user-attachments/assets/971d2221-dc3b-479a-a9da-0f8549ec63bf)


---

## 🍬 컷신

플레이 결과에 따라 다른 엔딩 컷씬이 제공됩니다! 🎬

![컷씬배드](https://github.com/user-attachments/assets/7534c1ac-2337-4119-9024-4cf2791c49c6)
🎭 배드 컷씬: 점수가 부족하면 과거 시험에 실패하고 훈장님의 훈화 말씀을 듣게 됩니다.

![컷씬굿](https://github.com/user-attachments/assets/d9dcf3c4-a0f6-48ac-9677-8d944482761a)
🏆 굿 컷씬: 목표 점수를 달성하면 장원급제! 많은 사람들에게 환호 받습니다.


---

## **📌 마무리**

✨ 1주일 동안 Unity WebGL로 개발한 러닝 게임입니다.

✨ 플레이하시고 피드백 남겨주시면 감사하겠습니다! 😊

 [**👉 플레이하기 (Unity WebGL)**](https://play.unity.com/en/games/22fbbd25-6547-47d1-b2e0-f5f10620e5b1/web))
