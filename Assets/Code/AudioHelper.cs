using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    [SerializeField]
    private AudioMaster master;


    public void ANIMATOR_Play_Footsteps()
    {
        master.PlayRandomPitch(AudioMaster.AudioType.Footstep);
    }
   
}
