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
~ playAnimation("Al","Drunk")
~ playAnimation("Dale","No")
DALE: Al...
+ [Lose the booze, man. You know you should.]
    -> GOOD3
+ [You're right. Get anotha' one!]
    -> BAD2

=== BAD2 ===
~ playAnimation("AL","Yes")
~ playSound("LaughTrack")
AL: One step ahead of you buddy!
~ playAnimation("AL","Drunk")
~ playAnimation("DALE","Yes")
DALE: Heck yeah! # close
-> STATEMACHINE

=== GOOD3 ===
~ playAnimation("AL","Crossing Arms")
AL: Man, you're such a party pooper, dawg!
~ playAnimation("AL","Yes")
~ setFlag("gave_life_advice", true)
AL: But...you're right. I really should, huh? # close
-> STATEMACHINE

=== PHASE1REPEAT ===
~ playAnimation("Al","Drunk")
AL: Huh, Dale? Back so soon?
-> PHASE1START

=== BLOCKED1 ===
~ playAnimation("Al","Dissapointed")
AL: I need to think about this, Dale. 
~ playAnimation("Al","Dissapointed")
~ playAnimation("Dale","Yes")
DALE: Take your time... # close
-> STATEMACHINE

=== PHASE2 ===
{talked_to_al_phase_2:
    -> PHASE1REPEAT
- else:
    -> PHASE1START
}

=== PHASE2START ===
~ talked_to_al_phase_2 = true
~ playAnimation("Al","Pondering")
AL: Say Dale, what's up with you today?
~ playAnimation("Al","Dissapointed")
~ playAnimation("DALE","Why")
DALE: I've been hearing laugh tracks all day!
~ playAnimation("Al","No")
AL: Really? I don't hear anything.
~ playAnimation("Al","IDLE 1")
~ playAnimation("DALE","Converse1")
DALE: Say something funny!
~ playAnimation("Al","Yes")
AL: Ok.
~ playAnimation("Al","Converse 1")
AL: What do people say when they're from Kansas?
~ playAnimation("Al","IDLE 1")
DALE: What?
~ playAnimation("Al","Knee Slap")
~ playAnimation("DALE","Dissapointed")
~ playSound("LaughTrack")
AL: They say, 'Hi! I'm from Kansas!'
~ playAnimation("DALE","No")
DALE: ...
DALE: I just don't know what to do. Everyone is acting so weird.
~ playAnimation("DALE","Pondering")
~ playAnimation("Al","Pondering")
AL: Have you ever thought you're just projecting your own delusions?
~ playAnimation("Al","Pondering")
~ playAnimation("DALE","Crossing Arms")
DALE: What do you mean?
~ playAnimation("Al","Converse 1")
AL: Maybe you've been so stressed with work that you can no longer percieve the dimension of your own family.
~ playAnimation("DALE","Pondering")
DALE: Huh...
+ [I think you're right.]
    -> GREAT2
+ [Nah, that's horse raddish!]
    -> EVIL2

=== GREAT2 ===
~ playSound("LaughTrack")
~ playAnimation("Al","Converse")
AL: I know I'm right, bucko! You really haven't been yourself today.
~ playAnimation("DALE","Yes")
DALE: Thanks, Al.
+ [I should be more considerate of my family.]
    -> GREAT3
+ [Let's go get drunk at the Casino!]
    -> EVIL2

== GREAT3 ==
~ setFlag("talked_about_life", true)
~ playAnimation("Al","Yes")
AL: That you should, buddy... That you should. # close
-> STATEMACHINE

=== EVIL2 ===
~ playSound("LaughTrack")
~ playAnimation("Al","Dissapointed")
AL: Oh, Dale!
~ playAnimation("Al","Converse 1")
AL: Maybe you should go think for a moment, don't you think?
DALE: Whatever, AL. # close
-> STATEMACHINE

=== PHASE2REPEAT ===
AL: Not Implemented # close
-> STATEMACHINE