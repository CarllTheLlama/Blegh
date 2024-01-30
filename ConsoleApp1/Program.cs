
using System;
using System.ComponentModel.Design;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Console position of the player
int playerX = 0;
int playerY = 0;

// Console position of the food
int foodX = 0;
int foodY = 0;


// Available player and food strings
string[] states = { "('-')", "(^-^)", "(X_X)" };
string[] foods = { "@@@@@", "$$$$$", "#####" };

// Current player string displayed in the Console
string player = states[0];

// Index of the current food
int food = 0;


InitializeGame();
while (!shouldExit)
{
    bool change = TerminalResized();
    if (change)
    {
        Console.Clear();
        Console.WriteLine("Console was resized. Program exiting.");
        break;
    }
    Move();
    FoodEaten();
}

//Checks ammount of food player ate and changes apperance if enough was eaten
void StatusChanger()
{
    int currentFood = ShowFood();
    player = states[currentFood];
}


//Checks if player stuffed his bell'ey
void FoodEaten()
{
    // if ((playerX >= foodX && playerX < foodX + foods[food].Length) && playerY == foodY)
    // {
    //     StatusChanger();
    // }

    if (playerX == foodX && playerY == foodY)
    {
        StatusChanger();
    }
}

// Returns true if the Terminal was resized 
bool TerminalResized()
{
    return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;

}

// Displays random food at a random location, and respawns food if player ate existing one.
int ShowFood()
{
    do
    {
        // Update food to a random index
        food = random.Next(0, foods.Length);
        
        // Update food position to a random location
        foodX = random.Next(0, width - player.Length);
        foodY = random.Next(0, height - 1);
        // Display the food at the location
        Console.SetCursorPosition(foodX, foodY);
        Console.Write(foods[food]);

    } while (playerX == foodX && playerY == foodY);
    return food;
}

void PlayerStat()
{

}
// Temporarily stops the player from moving
void FreezePlayer()
{
    System.Threading.Thread.Sleep(1000);
    player = states[0];
}

// Reads directional input from the Console and moves the player
void Move()
{
    int lastX = playerX;
    int lastY = playerY;

    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow:
            playerY--;
            break;
        case ConsoleKey.DownArrow:
            playerY++;
            break;
        case ConsoleKey.LeftArrow:
            playerX--;
            break;
        case ConsoleKey.RightArrow:
            playerX++;
            break;
        case ConsoleKey.Escape:
            shouldExit = true;
            break;    
         default:
            Console.Clear();
            Console.WriteLine("You pressed key other than arrows, you fucking donkey.");
            shouldExit = true;
            return;
    }


    // Clear the characters at the previous position
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < player.Length; i++)
    {
        Console.Write(" ");
    }

    // Set the food position and draw
    Console.SetCursorPosition(foodX < 0 ? 0 : foodX, foodY < 0 ? 0 : foodY);
    Console.Write(foods[food]);

    FoodEaten();

    // Keep player position within the bounds of the Terminal window
    playerX = (playerX < 0) ? 0 : (playerX >= width ? width : playerX);
    playerY = (playerY < 0) ? 0 : (playerY >= height ? height : playerY);

    // Draw the player at the new location
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Clears the console, displays the food and player
void InitializeGame()
{
    Console.Clear();
    FoodEaten();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);

}