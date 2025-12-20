# Deepfine 사전 과제 제출 - 민지석
Edit. 2025-12-20

## 1. 프로젝트 구조(상위)

### LaserSystem
- LaserConfig
  - Immutable configuration data for the laser system
  - Laser 시스템에 사용되는 변경 불가능한 설정 데이터
- LaserEmitter
  - Orchestrates laser lifecycle, tracing, rendering, and interactions
  - Laser의 생명주기, 추적, 렌더링 및 상호작용을 총괄하는 역할
- LaserTracer
  - Computes laser raycasts and reflection logic
  - Laser의 레이캐스트 계산과 반사 로직을 담당
- TraceResult
  - Data container describing a traced laser path (defined alongside LaserTracer)
  - Laser 추적 결과를 표현하는 데이터 컨테이너 (LaserTracer와 함께 정의됨)
- LaserRenderer
  - Renders the laser path using a LineRenderer
  - LineRenderer를 사용하여 Laser 경로를 렌더링

### Interfaces
- ILaserInteractable
  - Interface for objects that respond to laser enter/exit
  - Laser의 진입 및 이탈에 반응하는 객체를 위한 인터페이스
- ITransformInteractable
  - Interface for runtime manipulation of object transforms (e.g., position and rotation) independent of specific input methods
  - 특정 입력 방식과 무관하게 런타임 중 객체의 Transform(위치, 회전)을 조작하기 위한 인터페이스

### Interactables
- Mirror
  - Reflective surface implementation and orchestrates interactive events
  - Laser를 반사하는 표면 구현체이며, 관련 상호작용 이벤트를 처리
- Receiver
  - Interactable laser target that changes state when hit
  - Laser에 맞으면 상태가 변경되는 상호작용 대상 객체

### Helpers
- ObjectSpawner
  - Handles runtime mirror placement via mouse input
  - 마우스 입력을 통해 런타임 중 Mirror 배치를 처리
- TransformManipulator
  - Handles runtime mirror position and rotation manipulation
  - 런타임 중 Mirror의 위치 및 회전 조작을 처리

### Debugging / Validation
- Targeted debug logging and visual checks were used during development to validate laser tracing, reflection behavior, and interaction state changes.
- 개발 과정에서 Laser 추적, 반사 동작, 상호작용 상태 변화를 검증하기 위해 제한적인 디버그 로그 및 시각적 검증을 사용

## 2. 조작 방법
### Scene
- 제출물 검증을 위해 **04_PreassignmentSubmission** scene을 사용해 주세요.
- 해당 씬은 Build Settings의 Scene List에 등록되어 있으며 "**Assets > Scenes > Prod > 04_PreassignmentSubmission**" 경로에서도 확인할 수 있습니다.
- Scene을 실행(Play)하면 Laser는 자동으로 발사됩니다.

### Mirror 생성
- Mirror를 생성하려면 마우스 커서를 맵의 지면 위에 위치시킨 후 Spacebar를 누르세요.
- Mirror는 유효한 지면 위에서만 생성할 수 있으며, 벽이나 맵 영역 밖에는 배치할 수 없습니다.

### Mirror 조작
- Mirror위에서 마우스 왼쪽 버튼을 클릭한 채로 유지하면 조작을 시작할 수 있습니다.
- 클릭한 상태에서:
  - 마우스를 드래그하여 Mirror의 위치를 변경합니다.
  - A / D 키를 사용하여 Mirror를 좌우로 회전합니다.
  - W / S 키를 사용하여 Mirror를 상하로 회전합니다.
- 마우스 왼쪽 버튼을 놓으면 조작이 종료됩니다.
- Mirror를 제거(디스폰)하려면, Mirror위에 마우스 커서를 올린 상태에서 마우스 오른쪽 버튼을 클릭하세요.

### Receiver 조작
- Scene이 시작되면 Receiver는 기본 상태인 파란색으로 표시됩니다.
- Laser가 Receiver에 닿으면 색상이 빨간색으로 변경됩니다.
- Laser가 더 이상 Receiver에 닿지 않으면 자동으로 파란색(기본 상태)으로 복귀합니다.

### 부가 동작
- Laser는 Mirror가 아닌 객체(벽, 지면, 장애물, Receiver)와 충돌하는 지점에서 정지합니다.
- 최대 반사 횟수(10회)에 도달하면 경고 메시지가 로그로 출력되며, 이후 추가 반사는 발생하지 않습니다.

## 3. Operating Instructions
### Scene
- Please use "**04_PreassignmentSubmission**" scene to validate my submission.
- This scene is setup in the **Scene List** of the **Build Settings**, or you can find it via "**Assets > Scenes > Prod > 04_PreassignmentSubmission**".
- When you hit Play and run the scene, the laser will fire automatically.

### Mirror Instantiation
- To instantiate a mirror: please place the mouse cursor on the map ground and press Spacebar.
- Mirrors can only be spawned on valid ground surfaces and cannot be placed on walls or outside the map bounds.

### Mirror Interaction
- Click and hold Left Mouse Button on a mirror to begin interaction.
- While holding:
  - Drag the mouse to update the mirror’s position.
  - Use A / D to rotate the mirror left and right.
  - Use W / S to rotate the mirror up and down.
- Release the left mouse button to end interaction.
- To remove (de-spawn) a mirror, hover the cursor over it and click Right Mouse Button.

### Receiver Interaction
- When the scene starts, the Receiver is in its default blue state.
- When the laser hits the Receiver, it changes to red.
- When the laser no longer hits the Receiver, it automatically reverts to blue.

### Secondary Interactions
- The laser stops at the point of collision with non-mirror objects, including Walls, Ground, Obstacles, and the Receiver.
- A warning message is logged when the maximum reflection count (10) is reached. No additional reflections occur beyond this limit.
