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
BILLY: Dad, you're home!
DALE: Hey champ, how was school today?
BILLY: It was fun! We played soccer at recess and I got an A on my math test!
DALE: Awesome! Hey, it's pretty late, so start getting ready for bed soon alright?
BILLY: Okay! Can we play catch tomorrow? Or watch some anime?
DALE: Sure thing buddy!
BILLY: Yay! I love you dad! Goodnight!
DALE: Goodnight Billy. # close
-> PROLOGUEREPEAT

=== PROLOGUEREPEAT ===
BILLY: Goodnight dad!
DALE: Goodnight Billy. # close
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

