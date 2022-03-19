// Enemy logic flow 
// 1. 플레이어가 일정 거리(50f) 바깥이면 패트롤링
// 2. 일정 거리 안으로 들어오면 추적 시작(updateState => updateBehavior)
// 3. 난이도 별로 논리 상이하게 적용(easy : 충돌 시 플레이어 죽음, intermediate & difficult : 사격)
// 4. 플레이어가 일정 거리 밖으로 나가면 추적/사격 중단 후 다시 패트롤링
// 5. 플레이어가 점프해서 누를 경우 enemy 죽음

public interface IEnemyBehavior 
{
    public enum enemyState
    {
        Idle,
        Track, // track player
        Fire, // shoot player
        Die, // when player jumps over enemy
    }

    public enum playerDistanceState { 
        TooFar, // can't detect player
        Within // detect player
    }

    public enum enemyType 
    {
        Easy, // (쥐, 개구리)
        Intermediate, // (뱀, 거미)
        Difficult, // (벌, 외계인<2종>)
        Object // (공)
    }

    // should implement all the functions from interface
    void Fire();
    void Die(); 
    void Idle(); 
    void Track();
    void Patrol();
}
