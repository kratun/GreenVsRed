# GreenVsRed
Console Application that represent 2D matrix game.

## IDE
```
Visual Studio v 16.6.5 or any other software that allows you to start .Net Core App 3.1 applications
```

## How to run
Find and load GreenVsRed.sln file from the directory where you downloaded it.</br>
When the project is loaded press F5 button (if you use Visual Studio v 16.6.5) from the keyboard or check how to start Console App .Net Core 3.1 with program that you use.</br>

## Game Description
1. In our game we assume that matrix has x (width) and y (height) where x<=y<1000, so max x,y is 999</br>
2. On the frist line you should write width and height of the matrix. They must be integers separated by comma ",". If one or both are out range ( width[0,max see 1.] or height [0, max see 1.]) you will receive an error message. If one or both are not an integer you will receive an error message also. After each error message you have to write the dimensions again.
3. On the next line you will receive a message "Please enter on each next {height} lines, {width} digits({1} for green or {0} for red)" and on the next Nth lines you have to write each row of the matrix. If you write less/more digits(1s or 0s) than the matrix width or line contains not correct digits you will receive an error message. After that each error message you have to write line again.</br>
5. Atfer height correct lines you have to write three integers separated by comma "," (target Point X and Y and Rounds). Target Point is a cell in the matrix for which you will like to calculate how many times it become green from the initial matrix till the matrix generate in the last round. Rounds is an integer, used as a property how many times you want to change the matrix.</br>   

Other functionality during the game:
- write command "End" if you do not want playing;
- write command "Restart" to restart game;
- write command "Repeat" to repeat inputs.

## Enjoy the game
You can repeat the game as many times as you like.</br>
The game will finish when you answer "No" to the question "Do you want to proceed?".</br>

## Need Help
If you have any questions about application or if you can not download video file, please contact me.</br>
