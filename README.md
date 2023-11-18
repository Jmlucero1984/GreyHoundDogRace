# GreyHoundDogRace 
Just an starting point for a later bets system implementation, that's we call "a reason for..."
 *IN PROGRESS...(sincerely, it's taking me quite more than i'd expected)* 

## Game Description
There are seven grey hounds on a 2 lap race. Takes in account points and the general winner. Originally started (endings of 2021) as a mean to produce data to work with bets and statistics.
At the moment this lacks of a general artistical style or, in design terms, "a graphical language".
 
## Game Mechanics
Blender modeled dog prototipes, free to change their skins and some skeleton metrics and behaviors. Basic IK transitions porvided by Unity Animator.
Step trails are simulated with a pool of decals
I took advantage of the Unity's pathfinder algorithm, even though some key events must be fixed by scripting, like finish line triggering (no box collision o whatever, even dynamically...)
To populate the steps of the building i had been using animated billboards, but it requires more detailed work.
https://github.com/Jmlucero1984/GreyHoundDogRace/assets/91501518/59604ab9-8b8c-4f48-bbd6-0de06de45e89

