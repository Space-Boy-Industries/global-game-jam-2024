EXTERNAL setFlag(variableName, value)
EXTERNAL loadScene(sceneName)
EXTERNAL playSound(soundName)
EXTERNAL playAnimation(animationName, persist)

VAR in_sitcom = false
VAR cleaned_table = false
VAR asked_for_coffee = false
VAR has_coffee = false
VAR coffee_correct = false
VAR did_homework = false
VAR built_gundam = false
VAR gave_life_advice = false
VAR talked_about_life = false

VAR talked_to_margaret_phase_1 = false
VAR talked_to_margaret_phase_2 = false
VAR talked_to_margaret_phase_3 = false

-> STATEMACHINE
=== STATEMACHINE ===
{in_sitcom:
    {cleaned_table:
        {did_homework:
            {coffee_correct:
                {built_gundam && gave_life_advice:
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
{talked_to_margaret_phase_1:
    -> PHASE1REPEAT
- else:
    -> PHASE1START
}

=== PHASE1START ===
~ talked_to_margaret_phase_1 = true
MARGARET: Rise and shine, Dale! Today, we're cleaning the house. I've got plans!
playSound("LaughTrack")
DALE: Uh, okay. You sure are chipper this morning.
DALE: Huh? What in the world? Who's laughing?
MARGARET: And we're starting with that model train set on the table!
playSound("LaughTrack")
MARGARET: Or should I say, you're starting with it. I'm going to go make some coffee.
DALE: What? You've never had a problem with my train set before.
DALE: And where is that laughing coming from?! # close
-> STATEMACHINE

=== PHASE1REPEAT ===
MARGARET: 
-> STATEMACHINE

=== PHASE2 ===
{has_coffee:
    -> PHASE2REPEAT
- else:
    {talked_to_margaret_phase_2:
        -> PHASE2REPEAT
    - else:
        -> PHASE2START
    }
}

=== BLOCKED1 ===
MARGARET: Not Implemented #close
-> STATEMACHINE

=== PHASE2 ===
{talked_to_margaret_phase_2:
    -> PHASE1REPEAT
- else:
    -> PHASE1START
}

=== PHASE2START ===
~ talked_to_margaret_phase_2 = true
MARGARET: Not Implemented # close
-> STATEMACHINE

=== PHASE2REPEAT ===
MARGARET: Not Implemented # close
-> STATEMACHINE

=== BLOCKED2 ===
MARGARET: Not Implemented # close
-> STATEMACHINE

=== PHASE3 ===
{talked_to_margaret_phase_3:
    -> PHASE1REPEAT
- else:
    -> PHASE1START
}

=== PHASE3START ===
~ talked_to_margaret_phase_3 = true
MARGARET: Not Implemented # close
-> STATEMACHINE

=== PHASE3REPEAT ===
MARGARET: Not Implemented # close
-> STATEMACHINE
