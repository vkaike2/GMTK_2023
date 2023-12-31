using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class Player : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField]
    private Animator spriteRendererAnimation;
    private const string ANIMATION_RENDERER_IDLE = "Idle";
    private const string ANIMATION_RENDERER_SELECTED = "Selected";
    [SerializeField]
    private Animator entityAnimator;
    private const string ANIMATION_ENTITY_IDLE = "Entity_Idle";
    private const string ANIMATION_ENTITY_MOVE = "Entity_Move";
    private const string ANIMATION_ENTITY_DIE = "Entity_Die";
    [Space]
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [Header("AUDIO")]
    [SerializeField]
    private AudioMaster audioMaster;

    [Header("UI")]
    [SerializeField]
    private SelectedArrowUI selectedArrowUI;
    [SerializeField]
    private Chat chat;

    public bool IsSelected => _status.IsSelected;
    public InputModel<Vector2> MoveInput { get; private set; } = new InputModel<Vector2>();
    public Animator EntityAnimator => entityAnimator;
    public StageManager StageManager => _stageManager;

    private PlayerStatus _status;
    private List<PlayerBaseState> _finiteStates = new List<PlayerBaseState>()
    {
        new Idle(),
        new Move(),
        new Die()
    };
    private PlayerBaseState _currentState;
    private GameManager _gameManager;
    private StageManager _stageManager;
    private WeaponHolder _weaponHolder;
    private Rigidbody2D _rigidbody2D;
    private bool _isTakingDamage;
    private bool _isDead = false;

    private void Awake()
    {
        _status = GetComponent<PlayerStatus>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        ClearInputActions();

        //if (IsSelected) Select();
        //else Deselect();
    }

    private void Start()
    {
        StartFiniteStates();

        ChangeState(State.Idle);

        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _stageManager = GameObject.FindObjectOfType<StageManager>();

        if (_gameManager == null) return;
        _gameManager.AddPlayer(this);
    }

    private void Update()
    {
        if (_currentState == null) return;

        _currentState.Update();
    }

    public void Select()
    {
        _rigidbody2D.isKinematic = false;

        selectedArrowUI.Select();
        spriteRendererAnimation.Play(ANIMATION_RENDERER_SELECTED);
        _status.IsSelected = true;
    }

    public void Deselect()
    {
        selectedArrowUI.Deselect();
        spriteRendererAnimation.Play(ANIMATION_RENDERER_IDLE);
        _status.IsSelected = false;

        MoveInput.Canceled();

        _rigidbody2D.isKinematic = true;
    }

    public void ChangeState(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.OnExitState();
            ClearInputActions();
        }

        _currentState = _finiteStates.FirstOrDefault(e => e.State == nextState);

        _currentState.OnEnterState();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput.Value = context.ReadValue<Vector2>();
        switch (context.phase)
        {
            case InputActionPhase.Started:
                //Debug.Log("started");
                MoveInput.Started();
                break;
            case InputActionPhase.Performed:
                //Debug.Log("performed");
                MoveInput.Performed();
                break;
            case InputActionPhase.Canceled:
                //Debug.Log("canceled");
                MoveInput.Canceled();
                break;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        if (_weaponHolder.CurrentWeapon != null)
        {
            _weaponHolder.OnAttack();
            return;
        }

        if (_status.InteractableWeapon != null)
        {
            _weaponHolder.EquipWeapon(_status.InteractableWeapon);
            _status.CantInteract();
            return;
        }
    }

    public void OnDropWeapon(InputAction.CallbackContext context)
    {
        TryToDropWeapon();
    }

    internal void ReceiveDamage(Vector3 position, StageManager.WinCondition weapon)
    {
        if (_isDead) return;

        if (_isTakingDamage) return;

        audioMaster.Play(AudioMaster.AudioType.ReceiveDamage);

        if (!_status.ImMonster && ReceiveDamageOnHero(weapon))
        {
            //ReceiveDamageOnHero(weapon);
            return;
        }


        TryToDropWeapon();

        StartCoroutine(TakeDamageAnimation());
        StartCoroutine(ApplyKnockback(position));
    }

    private bool ReceiveDamageOnHero(StageManager.WinCondition weapon)
    {
        if (_stageManager.WinConditionWeapon == weapon)
        {
            _isDead = true;
            ChangeState(State.Die);
            return true;
        }
        else
        {
            chat.ShowUp(_stageManager.WinConditionWeapon.ToString());

            return false;
        }
    }

    private void TryToDropWeapon()
    {
        if (_weaponHolder.CurrentWeapon == null) return;
        _weaponHolder.DropWeapon();
    }

    private void ClearInputActions() => MoveInput.ClearActions();

    private void StartFiniteStates()
    {
        foreach (PlayerBaseState state in _finiteStates)
        {
            state.Start(this);
        }
    }

    private IEnumerator TakeDamageAnimation()
    {
        _isTakingDamage = true;

        Color color = spriteRenderer.color;

        int howManyTimesItWillBlink = 4;
        float blinkDuration = 0.5f;

        for (int i = 0; i < howManyTimesItWillBlink; i++)
        {
            color.a = 0;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(blinkDuration / (howManyTimesItWillBlink * 2));

            color.a = 1;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(blinkDuration / (howManyTimesItWillBlink * 2));

        }
        color.a = 1;
        spriteRenderer.color = color;

        _isTakingDamage = false;
    }

    private IEnumerator ApplyKnockback(Vector3 attackPosition)
    {
        _rigidbody2D.isKinematic = false;

        Vector2 direction = WeaponUtils.GetNormalizedDirection(this.transform, attackPosition);
        direction *= 2;
        _rigidbody2D.AddForce(-direction, ForceMode2D.Impulse);

        yield return new WaitForSeconds(.2f);

        _rigidbody2D.velocity = Vector2.zero;

        if (!IsSelected)
        {
            _rigidbody2D.isKinematic = true;
        }
    }
}
