/*
TODO LIST
...

AR Mode
	1. setup AR mode
	2. test simple AR on mobile (android/ios)
	3.test 3D model AR on real mobile device (android/ios)
	4.find Line game unity source code
	5. implement on dev environment
	6. integrate LINE game with AR
	7. test playing LINE AR game on real device (android/ios)
...

UI Setup
	1. find assets															x
		- 6 balls															x
		- board																x
		- header		
		- dialog
	2. tilemap																x
	3. board: 9x9 cells(grey color)											x
	4.header bar:
		-score texts: best core, time, new score(white color)				
		- black background													
	5.setup GUI programmatically											x
...

Game Play
	1. New game start
	2. GameOver (win/lose)
	3.init balls first time
	4. Mouse detect
	5. New position move ball
	6. explode ball when match
	7. add new score
	8.random 3 balls, display them in header bar
	9. display 3 new balls after finish moving ball (optional)
	10.timing(optional)
	11.initialize GUI programmatically
...

Addition effect
	1. decline effect -> stuck moving ball
	2. highlight effect -> selecting ball
	3. add new score effect
	4. new mouse position pointing effect
	5. explosion effect to match 5 balls
	6. pathfiding line (optional)
	
More Optional Feature (if has time)
	1.Menu Home
	2.Skin switching
	3.Level up
	4.map journey
......

LINE game rule
1. use mouse to locate new position of ball
2.explode balls if 5 balls or over are same color in a line (continously in horizontal/vertical/diagonal)
3.earn new score for the explosion
4.ball cannot move overlap others
5.ball can only move 4 directions: up, down, left, right
6.ends game when player cannot make the ball move anymore
......

 */