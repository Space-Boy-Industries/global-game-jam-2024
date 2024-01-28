using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CharacterAnimator
{
    public string characterName;
    public Animator animator;
}

public class CharacterAnimationSystem : MonoBehaviour
{
    public static CharacterAnimationSystem Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    
    public List<CharacterAnimator> characterAnimators;

    private void Start()
    {
        string[] characterNames = {"Margaret", "Billy", "Al"};
        foreach (var characterName in characterNames)
        {
            FindCharacter(characterName.ToLower(), characterName);
        }
        
        FindCharacter("dale", "Player");
    }
    
    private void FindCharacter(string characterName, string objName)
    {
        if (characterAnimators.Any(characterAnimator => string.Equals(characterAnimator.characterName, characterName, StringComparison.CurrentCultureIgnoreCase)))
        {
            return;
        }
        
        var obj = GameObject.Find(objName);
        
        if (obj == null)
        {
            Debug.LogError($"Could not find character with name {objName}");
            return;
        }
        
        var animator = obj.GetComponentInChildren<Animator>();
        
        if (animator == null)
        {
            Debug.LogError($"Could not find animator on character with name {objName}");
            return;
        }
        
        characterAnimators.Add(new CharacterAnimator()
        {
            characterName = characterName,
            animator = animator
        });
    }

    public void PlayAnimation(string characterName, string animationName)
    {
        Debug.Log("Playing animation");
        foreach (var characterAnimator in characterAnimators)
        {
            if (!string.Equals(characterAnimator.characterName, characterName,
                    StringComparison.CurrentCultureIgnoreCase)) continue;
            Debug.Log("Found character");
            characterAnimator.animator.Play(animationName);
            return;
        }
        
        Debug.LogError($"Could not find character with name {characterName}");
    }

    public void ResetAnimations()
    {
        foreach (var animator in characterAnimators)
        {
            animator.animator.Play("Idle");
        }
    }
}
