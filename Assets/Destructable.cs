using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{

    //khai báo biến lựa chọn hit type: color , inflate, push, none; float DisableHitEffect, SpriteParent ( none(Transform)) ; ParticalSystem : custom hit effect, AudioClip CustomHit Source, float hide when dead, Material hit Material, int Health
    public enum HitType { Color, Inflate, Push, None }
    public HitType hitType;
    public bool disableHitEffect;
    public Transform spriteParent;
    public ParticleSystem customHitEffect;
    public AudioClip customHitSource;
    public bool hideWhenDead;
    public Material hitMaterial;
    public int health;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
