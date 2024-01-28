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

VAR talked_to_al_phase_1 = false
VAR talked_to_al_phase_2 = false

-> STATEMACHINE
=== STATEMACHINE ===
{in_sitcom: 
    {gave_life_advice:
        {coffee_done && built_gundam: 
            -> PHASE2
        - else:
            -> BLOCKED1
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
{talked_to_al_phase_1:
    -> PHASE1REPEAT
- else:
    -> PHASE1START
}

=== PHASE1START ===
~ playAnimation("Al","Drunk")
~ talked_to_al_phase_1 = true
AL: Well hey there ol' buddy ol' pal, Dale my guy, how's it going? Wanna have a beer with me?
~ playSound("LaughTrack")
~ playAnimation("Al","Drunk")
~ playAnimation("Dale","No")
DALE: Al, are you drunk again? What happened to going sober?
~ playAnimation("Al","Pondering")
AL: Sober?
~ playAnimation("Al","Pondering")
~ playSound("LaughTrack")
AL: Who's that?
~ playAnimation("Al","Drunk")
~ playAnimation("Dale","Dissapointed")
DALE: Al...
+ [You need to stop drinking.]
    -> GOOD2
+ [I think you need another beer.]
    -> BAD2

=== GOOD2 ===
~ playAnimation("Al","Why")
AL: Huh? STOP drinking?!
~ playAnimation("Al","No")
~ playAnimation("Dale","Dissapointed")
~ playSound("LaughTrack")
AL: But I have to stay hydrated!
DALE: Al...
+ [Lose the booze, .]
    -> GOOD3
+ [I think you need another beer.]
    -> BAD2


=== PHASE1REPEAT ===
AL: Not Implemented # close
-> STATEMACHINE

=== BLOCKED1 ===
AL: Not Implemented # close
-> STATEMACHINE

=== PHASE2 ===
{talked_to_al_phase_2:
    -> PHASE1REPEAT
- else:
    -> PHASE1START
}

=== PHASE2START ===
~ talked_to_al_phase_2 = true
AL: Not Implemented # close
-> STATEMACHINE

=== PHASE2REPEAT ===
AL: Not Implemented # close
-> STATEMACHINE