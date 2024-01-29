EXTERNAL setFlag(variableName, value)
EXTERNAL loadScene(sceneName)
EXTERNAL playSound(soundName)
EXTERNAL playAnimation(animationName, persist)

VAR in_sitcom = false
VAR cleaned_table = false
VAR asked_for_coffee = false
VAR has_coffee = false
VAR coffee_correct = false
VAR coffee_done = false
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
            {coffee_done:
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
~ playSound("LaughTrack")
DALE: Uh, okay. You sure are chipper this morning.
DALE: Huh? What in the world? Who's laughing?
MARGARET: And we're starting with that model train set on the table!
~ playSound("LaughTrack")
MARGARET: Or should I say, you're starting with it. I'm going to go make some coffee.
DALE: What? You've never had a problem with my train set before.
DALE: And where is that laughing coming from?! # close
-> STATEMACHINE

=== PHASE1REPEAT ===
MARGARET: That dang train set is still on the table. How am I supposed to clean with that in the way?
~ playSound("LaughTrack")
DALE: It's never been in your way before!
DALE: Ugh, fine. I'll put it away. # close
-> STATEMACHINE

=== BLOCKED1 ===
MARGARET: I'm going to be busy cleaning.
MARGARET: I think Billy needed help with his homework. Can you go help him? #close
-> STATEMACHINE

=== PHASE2 ===
{has_coffee:
    -> GIVECOFFEE
- else:
    {talked_to_margaret_phase_2:
        -> PHASE2REPEAT
    - else:
        -> PHASE2START
    }
}

=== GIVECOFFEE ===
~ setFlag("has_coffee", false)
{coffee_correct:
    ~ setFlag("coffee_done", true)
    MARGARET: Just how I like it. Thank you dear. # close
    -> STATEMACHINE
- else:
    MARGARET: Come on Dale, we've been married for 12 years and you still don't know how I like my coffee?
    ~ playSound("LaughTrack")
    MARGARET: Milk and two spoonfuls of sugar. Now get to it, I can't be the only one staying busy! # close
    -> STATEMACHINE
}

=== PHASE2START ===
~ talked_to_margaret_phase_2 = true
~ setFlag("asked_for_coffee", true)
MARGARET: I need caffeine.
MARGARET: Dale, could you please make me a cup of coffee? # close
-> STATEMACHINE

=== PHASE2REPEAT ===
MARGARET: Make me coffee please. # close
-> STATEMACHINE

=== BLOCKED2 ===
MARGARET: Honey - I thought you were going to play with Billy? And talk to Al about something? We can talk later.# close
-> STATEMACHINE

=== PHASE3 ===
{talked_to_margaret_phase_3:
    -> PHASE3REPEAT
- else:
    -> PHASE3START
}

=== PHASE3START ===
~ talked_to_margaret_phase_3 = true
MARGARET: Dale.
~ playSound("LaughTrack")
DALE: Marg... I've had such a confusing day. It feels like I'm in a sitcom or something...
MARGARET: What do you mean?
DALE: Well, it's hard to explain. Doesn't matter now. Today was... alright, I think. Except for the disembodied laughing.
~ playSound("LaughSympathetic")
MARGARET: I didn't hear any such laughing. Do you need to see a doctor?
DALE: Just then! Right when you spoke!
MARGARET: Honey... I think you might be hearing things.
DALE: Okay, fine, it doesn't matter. But here's something that does matter to me: Did you have a good day?
MARGARET: I did.
~ playSound("PlayMargaretStinger")
MARGARET: Thank you for all the help today. Have you talked to Billy yet?  #close
-> STATEMACHINE

=== PHASE3REPEAT ===
MARGARET: I love you, dear. Let's take Billy to the park tomorrow.
MARGARET: ...
MARGARET: ... # close
DALE: What?
MARGARET: ...I think someone's outside again.
-> STATEMACHINE
