# Kata: Bowling Game

Create a program, which, given a valid sequence of rolls for
one line of American Ten-Pin Bowling, produces the total
score for the game. This is a summary of the rules of the game:
• Each game, or “line” of bowling, includes ten turns, or
“frames” for the bowler.
• In each frame, the bowler gets up to two tries to knock
down all the pins.
• If in two tries, he fails to knock them all down, his score
for that frame is the total number of pins knocked down
in his two tries.
• If in two tries he knocks them all down, this is called
a “spare” and his score for the frame is ten plus the
number of pins knocked down on his next throw (in
his next turn).
• If on his first try in the frame he knocks down all the
pins, this is called a “strike”. His turn is over, and his
score for the frame is ten plus the simple total of the
pins knocked down in his next two rolls.
• If he gets a spare or strike in the last (tenth) frame,
the bowler gets to throw one or two more bonus balls,
respectively. - These bonus throws are taken as part of
the same turn. If the bonus throws knock down all the
pins, the process does not repeat: the bonus throws are
only used to calculate the score of the final frame.
³¹With thanks to Robert C. Martin, who designed this Kata, for permission to include it
here.

## Kata: Bowling Game 120
• The game score is the total of all frame scores.
Here are some things that the program will not do:
• We will not check for valid rolls.
• We will not check for correct number of rolls and
frames.
• We will not provide scores for intermediate frames.
The input is a scorecard from a finished bowling game, where
“X” stands for a strike, “-” for no pins bowled, and “/” means
a spare. Otherwise figures 1-9 indicate how many pins were
knocked down in that throw.
Sample games:
12345123451234512345
always hitting pins without getting spares or strikes, a total
score of 60
XXXXXXXXXXXX
a perfect game, 12 strikes, giving a score of 300
9-9-9-9-9-9-9-9-9-9-
heartbreak - 9 pins down each round, giving a score of 90
Kata: Bowling Game 121
5/5/5/5/5/5/5/5/5/5/5
a spare every round, giving a score of 150

## Additional discussion points for the
Retrospective
• Did you do any design before you started coding? If so,
does your code have this design now?
• At what point did you realize you can’t simply loop
over frames, that you in fact need to refer to the
previous frame as well as the current one in order to
calculate the score? In an ideal world, when should you
have realized this?
• Look at the code you have ended up with, and compare
it with the above description of the rules of bowling. Is
there any similarity in the words used in each?
• Did you do enough refactoring? How would you know?
• How did you decide which tests to write, and in which
order? Did it matter what order you implemented them
in?

## Ideas for after the Dojo
Read this article “Engineer Notebook: An Extreme Program-
ming Episode”³² by Robert C. Martin, where he describes
solving this kata together with Robert S. Koss. Follow along in
your editor. Does he do the kata the same way as you would?
³²http://www.objectmentor.com/resources/articles/xpepisode.htm
Kata: Bowling Game 122


## Contexts to use this Kata
This kata is relatively easy for newcomers to TDD. It’s a
good one for creating a test list at the start, and choosing a
suitable order to implement them in that allows the algorithm
to develop organically.
As Robert Martin points out in his article, it is possible to
analyse the requirements for this problem and come up with
an over-designed solution. If you do this Kata with TDD
newcomers it could show them how TDD can help you
discover a good design for your software without too much
up-front work.