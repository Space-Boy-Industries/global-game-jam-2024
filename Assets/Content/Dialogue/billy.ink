EXTERNAL setVariable(variableName, value)
EXTERNAL loadScene(sceneName)
EXTERNAL playSound(soundName)
EXTERNAL playAnimation(animationName, persist)

VAR in_sitcom = false
VAR cleaned_table = false
VAR made_coffee = false
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
DALE: Billy, it’s time for bed.
BILLY: But Daaaaaaaad…
DALE: Now, Billy. Tomorrow we can play together! 
BILLY: Okay!!!!!! Can we play catch tomorrow? Or watch some anime! 
DALE: Yes we can! Now off to bed with you. #close
-> STATEMACHINE

=== PHASE1 ===
Billy: Not Implemented #close
-> STATEMACHINE

=== BLOCKED ===
Billy: Not Implemented #close
-> STATEMACHINE

=== PHASE2 ===
Billy: Not Implemented #close
-> STATEMACHINE

=== ENDING ===
Billy: Not Implemented #close
-> STATEMACHINE

