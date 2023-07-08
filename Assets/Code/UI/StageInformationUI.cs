using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class StageInformationUI : MonoBehaviour
{
    [Header("COMPONENTS")]
    [SerializeField]
    private List<Animator> detailsAnimator;
    private const string ANIMATION_DETAILS_START = "Stage Info Details";
    [Space]
    [SerializeField]
    private Image winConditionIcon;
    [SerializeField]
    private TextMeshProUGUI stageName;
    [Space]
    [SerializeField]
    private List<GameObject> backgrounds;
    [SerializeField]
    private SceneTransition sceneTransition;

    [Header("CONFIGURATIONS")]
    [SerializeField]
    private float lifeTime = 5f;

    private Animator _animator;
    private const string ANIMATION_MAIN_HIDE = "Stage Information_Hide";

    private GameManager _gameManager;
    private StageManager _stageManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    
        foreach (var background in backgrounds) 
        { 
            background.SetActive(true);
        }
    }

    private void Start()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _stageManager = GameObject.FindObjectOfType<StageManager>();

        sceneTransition.OnEndTransitionStart.AddListener(StartAnimations);

        winConditionIcon.sprite = _stageManager.WinConditionSprite;
        stageName.text = $"STAGE {_stageManager.StageNumber.ToString().PadLeft(2, '0')}";
    }

    private void StartAnimations()
    {
        StartCoroutine(AnimatingDetails());
        StartCoroutine(WaitThenHide());
    }

    private IEnumerator AnimatingDetails(int index = 0)
    {
        if (index == detailsAnimator.Count)
        {
            index = 0;
        }

        Animator detailAnimator = detailsAnimator[index];
        detailAnimator.Play(ANIMATION_DETAILS_START);

        yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 1f));

        index++;

        StartCoroutine(AnimatingDetails(index));
    }

    private IEnumerator WaitThenHide()
    {
        yield return new WaitForSeconds(lifeTime);
        StopAllCoroutines();
        _animator.Play(ANIMATION_MAIN_HIDE);

        _stageManager.OnStartGame.Invoke();
    }

    //private bool IsAnimationPlaying(Animator animator, string animationName)
    //{
    //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    //    return stateInfo.IsName(animationName);
    //}
}
