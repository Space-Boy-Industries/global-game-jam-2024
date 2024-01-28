EXTERNAL setFlag(variableName, value)
EXTERNAL loadScene(sceneName)
EXTERNAL playSound(soundName)
EXTERNAL playAnimation(animationName, persist)

VAR in_sitcom = false
VAR cleaned_table = false
VAR has_coffee = false
VAR coffee_correct = false
VAR did_homework = false
VAR built_gundam = false
VAR gave_life_advice = false
VAR talked_about_life = false

-> STATEMACHINE
=== STATEMACHINE ===
{in_sitcom:
    -> PHASE1
- else:
    -> PROLOGUE
}

=== PROLOGUE ====
DALE: Hey Marg, how was your day? Sorry for getting home so late.
MARGARET: No worries dear. I had a pretty good day. How was work?
DALE: Long day... Power shut off for the whole building and my team had to stay late to make sure our samples didn't spoil.
MARGARET: Oh no! That sounds awful.
MARGARET: Well, I'm glad you're home now. Do you want to get ready for bed?
+ [Yeah]
    -> GO2BED
+ [Not yet]
    -> NOBEDYET

=== PROLOGUEREPEAT ===
MARGARET: Ready for bed dear?
+ [Yeah]
    -> GO2BED
+ [Not yet]
    -> NOBEDYET

=== GO2BED ===
MARGARET: Goodnight honey, love you.
DALE: Goodnight sweetie, love you too.
~ setFlag("in_sitcom", true)
~ loadScene("sitcom")
-> STATEMACHINE

=== NOBEDYET ===
DALE: Not yet. I'm going to go say goodnight to Billy.
MARGARET: Okay, let me know when you're ready. # close
-> PROLOGUEREPEAT

=== PHASE1 ===
MARGARET: Not Implemented #close
-> STATEMACHINE

=== BLOCKED ===
MARGARET: Not Implemented #close
-> STATEMACHINE

=== PHASE2 ===
MARGARET: Not Implemented #close
-> STATEMACHINE

=== ENDING ===
MARGARET: Not Implemented #close
-> STATEMACHINE

