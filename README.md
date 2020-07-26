# GreenVsRed
Console Application 
It represent game on a 2D matrix.

## IDE
```
Visual Studio v 16.6.5 or any other that allows you to start .Net Core App 3.1 
```

## Technology stack
Visual Studio v 16.6.5 or any other that allows you to start .Net Core App 3.1 <br />

## How to run
Open Visual Studio find "GreenVsRed.sln" and load it. Press F5 button (if you use Visual Studio v 16.6.5) from the keyboard or check how to start Console App .Net Core 3.1.</br>

## Game Description
1. In our game we assume that matrix has x (width) and y (height) where x<=y<1000, so max x,y is 999</br>
2. On a frist line you should write matrix dimensions width and height, integers separated by comma ",". If one or both are out range ( width[0,max see 1] or height [0, max see 1]) you will receive a error message. If one or both are not an integer you will receive error message. After each error message you can write it again correctly.
3. On the next line you will receive message "Please enter on each {height} lines, {width} digits({1} for green or {0} for red)" and on the next N line you have to write each row of the matrix. If you write less/more digits(1s or 0s) than the matrix width or row contains not correct digits you will receive error message and you can repeat row again.</br>
5. Ater height correct rows you have to write target Point and Rounds. Three integers separated by comma ",". Target Point is a cell in the matrix for which you will like to calculate how many times it becam green from the initial matrix till the last Round. Rounds is an integer and it is used as a property how many times you want to change the matrix.</br>   

## Enjoy the game
You can repeat the game as many times as you like.</br>
The game will finish when you answer "No" to the question "Do you want to proceed?".</br>