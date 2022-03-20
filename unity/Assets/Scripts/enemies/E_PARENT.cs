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

    // should implement all the functions from interface
    void Fire();
    void Die(); 
    void Idle(); 
    void Track();
    void Patrol();
}
