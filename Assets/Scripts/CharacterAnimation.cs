using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    private IMove mover;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        mover = GetComponent<IMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var speed = mover.Speed;
        _animator.SetFloat("Speed", Mathf.Abs(speed));

        if (speed != 0)
            spriteRenderer.flipX = speed > 0;
    }
}
