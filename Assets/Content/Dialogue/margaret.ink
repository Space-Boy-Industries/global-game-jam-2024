EXTERNAL setFlag(variableName, value)
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
MARGARET: Goodnight Honey.  
DALE: Goodnight sweetie. 
~ setFlag("in_sitcom", true)
~ loadScene("sitcom")
# close
-> STATEMACHINE

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

