using UnityEngine;
using UnityEngine.Events;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private bool cameFromOtherScene = true;


    public UnityEvent OnEndTransitionStart { get; set; } = new UnityEvent();
    public UnityEvent OnEndTransitionEnd { get; set; } = new UnityEvent();

    private StageManager _stageManager;
    private Animator _animator;
    private const string ANIMATION_RIGHT_TO_LEFT_OUT = "Transition_Right to Left";
    private const string ANIMATION_LEFT_TO_RIGHT_IN = "Transition_Left to Right";

    private bool _isStating = false;
    private bool _isEnding = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _stageManager = GameObject.FindObjectOfType<StageManager>();

        if (cameFromOtherScene)
        {
            StartStage();
        }

        if (_stageManager == null) return;
        _stageManager.OnFinishState.AddListener(OnFinishStage);
    }

    public void Animator_EndTransition()
    {
        if (_isStating)
        {
            OnEndTransitionStart.Invoke();
        }

        if(_isEnding)
        {
            OnEndTransitionEnd.Invoke();
        }
    }

    public void StartStage()
    {
        _isStating = true;
        _animator.Play(ANIMATION_LEFT_TO_RIGHT_IN);
    }

    public void OnFinishStage()
    {
        _isEnding = true;
        _animator.Play(ANIMATION_RIGHT_TO_LEFT_OUT);
    }
}
