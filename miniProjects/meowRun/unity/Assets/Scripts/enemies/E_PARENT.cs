public interface IEnemyBehavior 
{
    public enum enemyState
    {
        Idle,
        Track, // track player
        Fire, // shoot player
        Die, // when player jumps over enemy
    }

    // should implement all the functions from interface
    void Fire();
    void Die(); 
    void Idle(); 
    void Track();
}
