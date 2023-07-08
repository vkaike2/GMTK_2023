using UnityEngine;


public abstract class PlayerBaseState
{
    public abstract Player.State State { get; }

    protected Player _player;
    protected PlayerStatus _status;
    protected Rigidbody2D _rigidbody2D;

    public virtual void Start(Player player)
    {
        _player = player;
        _status = player.GetComponent<PlayerStatus>();
        _rigidbody2D = player.GetComponent<Rigidbody2D>();
    }

    public abstract void Update();

    public abstract void OnEnterState();
    public abstract void OnExitState();
}
