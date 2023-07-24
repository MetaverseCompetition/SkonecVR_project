
먼저 해야할것

1)스크립트 모두 다운받아서 유니티폴더에넣기 (Assets든 스크립트모아두는폴더든)
2)쉐이더 에셋부터 다운받기


플레이어에 XRPlayercontroller 넣기 


몬스터 쉐이더 적용방법

shader을 다운받는다.

1)https://assetstore.unity.com/packages/p/urp-dissolve-2020-191256
2)https://assetstore.unity.com/packages/vfx/shaders/ultimate-10-shaders-168611

몬스터의 하위 자식 중 Bone_Pelvis_1 아래의 LowPolySkeleton의 Material에 
1에서 다운받은 폴더로 들어가 URP - Materials-shaer graphs_dissolve_dissolve_direction..(초록색 메테리얼)
을 부모로 할당해준다 (메테리얼에 끌어넣으면 material의 parent 탭에 들어간다)

그리고 사진보고 그대로 할당해주면 된다.



---------------------------------------------------------------
몬스터 순찰 방법

1)하이어아키창에 Destinations라는 빈오브젝트를 생성해준다.
2) Destinations라느 빈 오브젝트 밑에 몬스터가 순찰할 위치를 랜덤으로 여러개 만들어준다(스폰위치생성하는거랑
똑같이)

3)몬스터 프리팹을 다운받는다 (올려준거 AI 어쩌고 프리팹. 메타 파일 둘다 경로에넣어주기)
4)montser.png사진 그대로 할당한다

5)몬스터를 마음대로 배치한다. 각 몬스터 마다 destinations 배열의 순서를 다르게 하자.

6)충돌 문제 생기면 말해주기 






