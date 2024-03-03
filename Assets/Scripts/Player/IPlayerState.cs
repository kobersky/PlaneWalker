/* interface for various states of the player (character) */
public interface IPlayerState 
{
    public void OnEnter(PlayerController controller);
    public void OnFixedUpdate(PlayerController controller);
    public void OnExit(PlayerController controller);
}
