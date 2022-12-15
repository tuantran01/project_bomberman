using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSpriteRenderer : MonoBehaviour
{
   private SpriteRenderer sr;

    //an array to store anim
    public Sprite idleSprite;
    public Sprite[] spriteArray;

    public float animationTime = 0.25f;
    private int animationFrame = 0;

    public bool idle = true;
    public bool loop = true;

   private void Awake() {
        sr = GetComponent<SpriteRenderer>();
   }

   private void Start() {
        // invoke the method NextFrame in animationTime second, then repeat every
        // animationTimie second
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
   }

   private void OnEnable()
   {
        sr.enabled = true;
   }

   private void OnDisable()
   {
        sr.enabled = false;
   }

    // method that create the animation
   private void NextFrame()
   {
        animationFrame++;

        // restart the animation
        if(loop & animationFrame >= spriteArray.Length) animationFrame = 0;
        
        // if idle render the idle sprite
        if(idle) 
        {
            sr.sprite = idleSprite;
        }
        else if(animationFrame >= 0 && animationFrame < spriteArray.Length)
        {
            sr.sprite = spriteArray[animationFrame];
        }
   }
}
