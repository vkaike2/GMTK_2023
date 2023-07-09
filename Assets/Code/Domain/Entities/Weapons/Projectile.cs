using System.Collections;
using UnityEngine;


public abstract class Projectile : MonoBehaviour
{
    [Header("AUDIO")]
    [SerializeField]
    protected AudioMaster audioMaster;

    public abstract void KillIt();
}
