using System.Collections;
using System.Diagnostics;

// Boolean to determine whether to exit the program
bool exit = false;

// Array to save user's results
List<string> gameHistory = new List<string>();

// Difficulty
int difficulty = 10;

// Show menu to player
void ShowMenu()
{
    Console.WriteLine("Please enter a number to select the operation you want to practice:");
    Console.WriteLine("\t1. Summation");
    Console.WriteLine("\t2. Subtraction");
    Console.WriteLine("\t3. Multiplication");
    Console.WriteLine("\t4. Division");
    Console.WriteLine("\t5. Random");
    Console.WriteLine("\t6. Show history");
    Console.WriteLine("\t7. Change difficulty");
    Console.WriteLine("\t8. Exit");
}

// Get user's input and return an integer to choose operation
int GetUserInput()
{
    string input = Console.ReadLine() ?? "";
    input = input.Trim().ToLower();
    int choice;

    try
    {
        choice = int.Parse(input);
        if (0 < choice && choice < 9)
            return choice;
        else
        {
            Console.WriteLine("Please enter a valid option.");
            return GetUserInput();
        }
    }
    catch
    {
        Console.WriteLine("Please enter a valid option.");
        return GetUserInput();
    }
}

void ShowHistory()
{
    for(int i = 0; i < gameHistory.Count; i++)
    {
        Console.WriteLine($"Q{i+1}: {gameHistory[i]}");
    }
    Console.Write("\n");
}

string getDifficulty()
{
    // Get current difficulty
    string currentDifficulty = "";
    if(difficulty == 1)
        currentDifficulty = "Easy";
    else if(difficulty == 10)
        currentDifficulty = "Normal";
    else if(difficulty == 100)
        currentDifficulty = "Hard";
    else 
        currentDifficulty = "ASIAN!";
    return currentDifficulty;
}

void ChangeDifficulty()
{
    Console.WriteLine("Choose your difficulty:");
    Console.WriteLine("\t1. Easy");
    Console.WriteLine("\t2. Normal");
    Console.WriteLine("\t3. Hard");
    Console.WriteLine("\t4. ASIAN!");
    Console.WriteLine($"Current difficulty: {getDifficulty()}");

    string userChoice = Console.ReadLine() ?? "";
    userChoice = userChoice.Trim().ToLower();
    int newDifficulty;
    bool getUserChoice = int.TryParse(userChoice, out newDifficulty);

    // Return if cannot get valid input
    if(getUserChoice == false)
    {
        Console.WriteLine("Invalid input! Cannot change your difficulty!");
        return;
    }
    // Change difficulty
    switch (newDifficulty)
    {
        case 1:
            difficulty = 1;
            break;
        case 2:
            difficulty = 10;
            break;
        case 3:
            difficulty = 100;
            break;
        case 4:
            difficulty = 10000;
            break;
    }
    Console.WriteLine($"Your new difficulty is {getDifficulty()}\n");
}

int MathQuestion(int firstNumber, int secondNumber, char operation)
{
    int result = 0;
    
    switch (operation)
    {
        case '+':
            result = firstNumber + secondNumber;
            break;
        case '-':
            result = firstNumber - secondNumber;
            break;
        case '*':
            result = firstNumber * secondNumber;
            break;
        case '/':
            result = firstNumber / secondNumber;
            break;
    }
    gameHistory.Add($"{firstNumber} {operation} {secondNumber} = {result}");
    return result;
}

void startGame(char operation)
{
    // Variables to generate questions
    Random generateNumber = new Random();
    int first = 0, second = 0;
    bool isRandom = operation == 'r';

    // User variables
    int userPoint = 0;
    bool correctAnswer;

    // Start timer
    Stopwatch stopwatch = Stopwatch.StartNew();

    for(int i = 0; i < 5; i++)
    {
        if (isRandom)
        {
            // Generate random operation
            char[] operations = {'+', '-', '*', '/'};
            Random random = new Random();
            operation = operations[random.Next(0, 4)];
        }
        if(operation == '+')
        {
            first = generateNumber.Next(0, 10 * difficulty);
            second = generateNumber.Next(0, 10 * difficulty);
        }
        else if (operation == '-')
        {
            first = generateNumber.Next(1, 10 * difficulty);
            second = generateNumber.Next(0, first);
        }
        else if(operation == '*')
        {
            first = generateNumber.Next(1, 3 * difficulty);
            second = generateNumber.Next(1, 3 * difficulty);
        }
        else
        {
            second = generateNumber.Next(1, 3 * difficulty);
            int tmp = generateNumber.Next(1, 3 * difficulty);
            first = tmp * second;
        }
        Console.Write($"Question {i+1}:\t");
        int res = MathQuestion(first, second, operation);
        Console.Write($"{first} {operation} {second} = ");
        string? answer = Console.ReadLine();
        
        int userAnswer; 
        bool getUserAnswer = int.TryParse(answer, out userAnswer);
        correctAnswer = userAnswer.Equals(res);
        
        if (getUserAnswer == false)
        {
            Console.WriteLine("Invalid input.\nYou won't have any point for this answer!\n");
            correctAnswer = false;
        }
        
        if(correctAnswer == true)
        {
            userPoint += 1;
            Console.WriteLine($"Correct! The answer is {res}\n");
        }
        else
        {
            Console.WriteLine($"Wrong! The correct answer is {res}\n");
        }
    }
    // Get the total seconds as a double
    double exactSeconds = stopwatch.Elapsed.TotalSeconds;
    // Round up total seconds
    int roundedUpSeconds = (int)Math.Ceiling(exactSeconds);

    // Display game result
    Console.WriteLine($"You have successfully answered {userPoint}/5 questions!");
    Console.WriteLine($"Total time: {roundedUpSeconds} seconds.\n");
}

while(exit == false)
{
    ShowMenu();
    int userChoice = GetUserInput();
    char operation;
    
    switch (userChoice)
    {
        case 1:
            operation = '+';
            startGame(operation);
            break;  
        case 2:
            operation = '-';
            startGame(operation);
            break;  
        case 3:
            operation = '*';
            startGame(operation);
            break;  
        case 4:
            operation = '/';
            startGame(operation);
            break;  
        case 5:
            operation = 'r';
            startGame(operation);
            break;  
        case 6:
            ShowHistory();
            break;  
        case 7:
            ChangeDifficulty();
            break;   
        default:
            Console.WriteLine("Game closed!");
            exit = true;
            break;
    }
}
