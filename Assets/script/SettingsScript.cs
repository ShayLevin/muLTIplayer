using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetInteger("settings", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
