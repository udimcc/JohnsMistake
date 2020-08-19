using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAPI : MonoBehaviour
{
    public float damage;
    public float attackRate;
    public AudioClip audioClip;

    protected AudioSource audioSource;
    protected Animator animator;

    public abstract void Attack();

    protected virtual void Awake()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.animator = this.GetComponent<Animator>();
    }
}
