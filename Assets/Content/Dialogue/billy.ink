EXTERNAL setFlag(variableName, value)
EXTERNAL loadScene(sceneName)
EXTERNAL playSound(soundName)
EXTERNAL playAnimation(animationName, persist)

VAR in_sitcom = false
VAR cleaned_table = false
VAR coffee_done = false
VAR did_homework = false
VAR built_gundam = false
VAR gave_life_advice = false
VAR talked_about_life = false

VAR talked_to_billy_phase_1 = false
VAR talked_to_billy_phase_2 = false
VAR talked_to_billy_phase_3 = false

-> STATEMACHINE
=== STATEMACHINE ===
{in_sitcom:
    {did_homework:
        {cleaned_table:
            {built_gundam:
                {coffee_done && gave_life_advice:
                    -> PHASE3
                - else:
                    -> BLOCKED2
                }
            - else:
                -> PHASE2
            }
        - else:
            -> BLOCKED1
        }
    - else:
        -> PHASE1
    }
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
{talked_to_billy_phase_1:
    -> PHASE1REPEAT
- else:
    -> PHASE1START
}

=== PHASE1START ===
~ talked_to_billy_phase_1 = true
BILLY: Not Implemented # close
-> STATEMACHINE

=== PHASE1REPEAT ===
BILLY: Not Implemented # close
-> STATEMACHINE

=== BLOCKED1 ===
BILLY: Not Implemented # close
-> STATEMACHINE

=== PHASE2 ===
{talked_to_billy_phase_2:
    -> PHASE1REPEAT
- else:
    -> PHASE1START
}

=== PHASE2START ===
~ talked_to_billy_phase_2 = true
BILLY: Not Implemented # close
-> STATEMACHINE

=== PHASE2REPEAT ===
BILLY: Not Implemented # close
-> STATEMACHINE

=== BLOCKED2 ===
BILLY: Not Implemented # close
-> STATEMACHINE

=== PHASE3 ===
{talked_to_billy_phase_3:
    -> PHASE1REPEAT
- else:
    -> PHASE1START
}

=== PHASE3START ===
~ talked_to_billy_phase_3 = true
BILLY: Not Implemented # close
-> STATEMACHINE

=== PHASE3REPEAT ===
BILLY: Not Implemented # close
-> STATEMACHINE
