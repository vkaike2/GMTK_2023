using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("CONFIGURATIONS")]
    [SerializeField]
    private bool isEquiped;


    [Header("COMPONENTS")]
    [SerializeField]
    protected CircleCollider2D pickupCollider;
    [SerializeField]
    private Animator genericAnimator;
    protected const string ANIMATION_GENERIC_IDLE = "Weapon_Idle";
    protected const string ANIMATION_GENERIC_SELECTED = "Weapon_Floating";
    [SerializeField]
    protected Animator specificAnimator;
    protected const string ANIMATION_SPECIFIC_IDLE = "Idle";
    protected const string ANIMATION_SPECIFIC_SELECTED = "Selected";
    [Space]
    [SerializeField]
    private GameObject _shadowGameObjbect;
    [Space]
    [SerializeField]
    protected Transform spawnPosition;

    [Header("AUDIO")]
    [SerializeField]
    protected AudioMaster audioMaster;

    protected GameManager _gameManager;

    public bool IsEquiped { get => isEquiped; private set => isEquiped = value; }
    public bool CanFlip { get; protected set; } = true;

    private void Awake()
    {
        Drop();
    }

    protected void Start()
    {
        _gameManager = GameManager.FindObjectOfType<GameManager>();
    }

    public abstract void Attack();

    public virtual void Equip(bool playAudio = true)
    {
        this.transform.localScale = Vector3.one;

        if (playAudio)
            audioMaster.Play(AudioMaster.AudioType.PickUPItem);

        genericAnimator.Play(ANIMATION_GENERIC_IDLE);
        pickupCollider.enabled = false;
        _shadowGameObjbect.SetActive(false);
    }

    public virtual void Drop()
    {
        //Debug.Log("DROPED");
        this.transform.parent = null;
        this.transform.localScale = Vector3.one;
        this.transform.rotation = new Quaternion();

        genericAnimator.Play(ANIMATION_GENERIC_SELECTED);
        pickupCollider.enabled = true;
        _shadowGameObjbect.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStatus playerStatus = collision.GetComponent<PlayerStatus>();
        if (playerStatus == null) return;
        playerStatus.CanInteractWith(this);
        specificAnimator.Play(ANIMATION_SPECIFIC_SELECTED);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerStatus playerStatus = collision.GetComponent<PlayerStatus>();
        if (playerStatus == null) return;
        playerStatus.CanInteractWith(this);
        specificAnimator.Play(ANIMATION_SPECIFIC_SELECTED);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerStatus playerStatus = collision.GetComponent<PlayerStatus>();
        if (playerStatus == null) return;

        playerStatus.CantInteract();

        specificAnimator.Play(ANIMATION_SPECIFIC_IDLE);
    }
}
