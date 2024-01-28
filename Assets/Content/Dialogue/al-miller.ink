EXTERNAL setVariable(variableName, value)

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
    {gave_life_advice:
        {made_coffee && built_gundam: 
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
AL: Hey Dale! How’s your night been?
DALE: It’s going. I was just about to head to bed.
AL: Ah man, alright. You know, I’m one week sober. Longest I’ve gone without a drink in a long time. We should celebrate! You want to head to the casino this weekend?
DALE: Al, you know I don’t gamble. I don’t think going to the casino is gonna be good for your sobriety either.
AL: Yeah.. yeah, I don’t drink. At least not anymore. Well — have a good night. Maybe over the weekend. The jazz club or something. 
DALE: Maybe Al. Have a good night. # close
-> STATEMACHINE

=== PHASE1 ===
AL: Im Drunk
+ Life Advice
    ~ setVariable("gave_life_advice", true)
    AL: Thanks #close

-> STATEMACHINE
=== BLOCKED ===
Al: zzz # close

-> STATEMACHINE
=== PHASE2 ===
Al: not implemented #close
-> STATEMACHINE

