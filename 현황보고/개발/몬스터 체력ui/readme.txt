올려준 프리팹이랑 meta파일 폴더에옮기고
lowpolyskeleton _AI 하나 씬에 배치하고 
monsterui프리팹을 lowopltyskeleton _Ai 에 자식으로 놔서 살짝위에 나오도록하게 함


그리고 새로 올려준 monstercontroller cs 다운받아서 사진대로 할당해주면댐 

문제점 : triggerenter할때마다 ++하게했느데 이 칼 collider가 아예손잡이부분까지 있다보니까 
충돌이 드르륵되면서 여러번 enter되는거가틈..

아 그리고 만약 ui안보이면 monsterCanvas들어가서 canvas 컴포넌트의 rendermode를 world space로
바꿔저야대요 
