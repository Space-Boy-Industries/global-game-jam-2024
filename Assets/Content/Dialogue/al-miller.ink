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
    {gave_life_advice:
        {has_coffee && built_gundam: 
            -> PHASE2
        - else:
            -> BLOCKED
        }
    - else:
        -> PHASE1
    }
- else:
    -> PROLOGUE
}

=== PROLOGUE ===
DALE: Hey Al, how's it going?
AL: Not bad Dale, how's your night been?
DALE: Long day at work. I was just about to head to bed.
AL: Alright man. Hey, we should do something this weekend. Tomorrow I'll be one week sober... Longest I've gone without a drink in a long time!
DALE: Hey, that's great Al! I'm proud of you!
AL: Thanks Dale. We should celebrate! You want to head to the casino this weekend?
DALE: Al, you know I don't gamble. And I don't think a trip to the casino is gonna be good for your sobriety either.
AL: Heh... yeah, I guess that's the alcoholism talking. Well, maybe we can go to the jazz club or something.
DALE: Yeah, I'll see if I can make it. Have a good night Al. # close
-> PROLOGUEREPEAT

=== PROLOGUEREPEAT ===
AL: Think you'll have time to go to the jazz club this weekend?
DALE: Yeah, I'll see if I can make it. Have a good night Al. # close
-> PROLOGUEREPEAT

=== PHASE1 ===
AL: Im Drunk
+ Life Advice
    ~ setFlag("gave_life_advice", true)
    AL: Thanks #close

-> STATEMACHINE
=== BLOCKED ===
Al: zzz # close

-> STATEMACHINE
=== PHASE2 ===
Al: not implemented #close
-> STATEMACHINE

