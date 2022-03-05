public interface IEnemyBehavior 
{
    void Fire();
    void Die(); 
    void Idle(); 
    void Track();
}
public class E_PARENT : IEnemyBehavior
{ 
    public enum enemyState
    {
        Idle,
        Track, // track player
        Fire, // shoot player
        Die, // when player jumps over enemy
    }

    // should implement all the functions from interface
    void IEnemyBehavior.Idle() {
        
    }
    void IEnemyBehavior.Die() {
        
    }
    void IEnemyBehavior.Fire() {
        
    }
    void IEnemyBehavior.Track() {
        
    }
}
