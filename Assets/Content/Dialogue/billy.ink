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

VAR talked_to_billy_phase_1 = false
VAR talked_to_billy_phase_2 = false
VAR talked_to_billy_phase_3 = false

-> STATEMACHINE
=== STATEMACHINE ===
{in_sitcom:
    {did_homework:
        {cleaned_table:
            {built_gundam:
                {coffee_correct && gave_life_advice:
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
BILLY: Dad! I need help with my homework.
DALE: Okay Billy. What's the homework?
~ playSound("LaughSympathetic")
BILLY: Math! It's a multiple choice sheet. Please help. I want to play with my toys.
DALE: Okay Billy. Let's do this.
-> HOMEWORK1

=== PHASE1REPEAT ===
DALE: Alright, let's try again.
-> HOMEWORK1

=== WRONGANSWER ===
~ playSound("LaughTrack")
BILLY: Uhh, I don't think that's right... # close
-> STATEMACHINE

=== HOMEWORK1 ===
BILLY: Okay, first problem: What is 2+2?
+ [2]
    -> WRONGANSWER
+ [4]
    -> HOMEWORK2
+ [22]
    -> WRONGANSWER

=== HOMEWORK2 ===
BILLY: Next one: What is 3×5?
+ [32.951]
    -> WRONGANSWER
+ [19]
    -> WRONGANSWER
+ [15]
    -> HOMEWORK3

=== HOMEWORK3 ===
BILLY: Next problem: What is 72÷9?
+ [8]
    -> HOMEWORK4
+ [19]
    -> WRONGANSWER
+ [9]
    -> WRONGANSWER

=== HOMEWORK4 ===
BILLY: Alright, next one: What is 4+2(3+5)?
+ [16]
    -> WRONGANSWER
+ [20]
    -> HOMEWORK5
+ [48]
    -> WRONGANSWER

=== HOMEWORK5 ===
BILLY:  Last one! This one is just extra credit. What are the roots of the quadratic equation x^2-5x+6?
+ [1, -1]
    BILLY: Well... if you say so.
    -> HOMEWORKDONE
+ [-5, 6]
    BILLY: Well... if you say so.
    -> HOMEWORKDONE
+ [2, 3]
    BILLY: Great! I really needed that extra credit.
    -> HOMEWORKDONE

=== HOMEWORKDONE ===
~ setFlag("did_homework", true)
~ playSound("LaughTrack")
BILLY: Thanks Dad!! I just really don't understand all these numbers. I'm so silly.
DALE: No problem, son. We can play with toys soon. #close
-> STATEMACHINE

=== BLOCKED1 ===
BILLY: Dad...
BILLY: Can we talk later? You're supposed to help Mom clean off the table...   # close
-> STATEMACHINE

=== PHASE2 ===
{talked_to_billy_phase_2:
    -> PHASE2REPEAT
- else:
    -> PHASE2START
}

=== PHASE2START ===
~ talked_to_billy_phase_2 = true
DALE: Okay Billy! Are you ready to play? I got you something the other day. It's a GUNDAM!
~ playSound("LaughTrack")
BILLY: A Gundam?  No way! Let's build it together!
DALE: Okay Billy!
~ setFlag("built_gundam", true)
(Gundam building cutscene happen) # close
-> STATEMACHINE

=== PHASE2REPEAT ===
BILLY: Dad... when can we build the gundam? You promised. # close
-> STATEMACHINE

=== BLOCKED2 ===
BILLY: Dad!
BILLY: The neighbor's being loud outside again! # close
-> STATEMACHINE

=== PHASE3 ===
{talked_to_billy_phase_3:
    -> PHASE3REPEAT
- else:
    -> PHASE3START
}

=== PHASE3START ===
~ talked_to_billy_phase_3 = true
BILLY: Hey Dad.
DALE: Hey Billy. Let's talk.
~ playSound("LaughSympathetic")
BILLY: Okay dad. What's up?
DALE: What happened? I thought you got an A on your math test?
~ playSound("LaughTrack")
BILLY: I don't know where you heard that! You know I'm not good at that stuff.
DALE: But... I see. Okay Billy. I don't really know what is going on here but yesterday the whole world was different.
DALE: Yesterday, you liked sports and you were good at math. Today, neither of those things are true.
DALE: But that's okay. We'll figure it out together.
~ playSound("LaughSympathetic")
DALE: You're young! Everything will be just fine.
BILLY: Thanks Dad. For the homework. And for playing with me too. # close
-> STATEMACHINE

=== PHASE3REPEAT ===
BILLY: Thanks for everything, Dad. Can you take me to school tomorrow? I want to show everyone my Gundam!
DALE: Of course, Billy. # close
-> STATEMACHINE
