// Brett Barnes, February 12, 2025, Lab 3: Mastermind
Console.WriteLine("Welcome to Code Breaker!");
Console.WriteLine("Can you guess the secret code?");

char minLetter = 'a';
char maxLetter = 'g'; 

int wordLength = GetWordLength(); 

string secret = GenerateSecretWord(wordLength, minLetter, maxLetter); 
int guessCount = 0;
string guess; 

Console.WriteLine($"A Secret word has been made, with {wordLength} letters from '{minLetter}' to '{maxLetter}'. Break the code!");

do
{
    guess = GetValidGuess(wordLength, minLetter, maxLetter);
    guessCount++; 

    int correctPositions = 0;
    int correctLetterWrongPosition = 0;

    bool[] secretUsed = new bool[secret.Length]; 
    bool[] guessUsed = new bool[guess.Length]; 

    for (int i = 0; i < secret.Length; i++) 
    {
        if (guess[i] == secret[i])
        {
            correctPositions++; 
            secretUsed[i] = true;
            guessUsed[i] = true;
        }
    }

    for (int i = 0; i < guess.Length; i++) 
    {
        if(!guessUsed[i]) 
        {
            for (int j = 0; j < secret.Length; j++)
            {
                if (!secretUsed[j] && guess[i] == secret[j])
                {
                    correctLetterWrongPosition++; 
                    secretUsed[j] = true;
                    break;

                }
            }
        }
    }       
    Console.WriteLine($"Correct Positions: {correctPositions}");
    Console.WriteLine($"Correct letters in wrong position: {correctLetterWrongPosition}"); 
    Console.WriteLine($"Guesses so far: {guessCount}\n");

} while (guess != secret); 

Console.WriteLine($"Code Broken! You broke the code {secret} in {guessCount} attempts."); 

static int GetWordLength() 
{
    int length;
    do
    {
        Console.WriteLine("Enter the code's length (between 3 and 7): ");
    } while (!int.TryParse(Console.ReadLine(), out length) || length < 3 || length > 7);
    return length;
}

static string GenerateSecretWord(int length, char min, char max)
{
    Random random = new Random();
    string availableLetters = "abcdefg"; 

    return new string(availableLetters.OrderBy(_ => random.Next()).Take(length).ToArray());
}

static string GetValidGuess(int length, char min, char max)
{
    string guess;
    do
    {
        Console.WriteLine("Enter your guess: ");
        guess = Console.ReadLine()?.ToLower(); 

        if (guess.Length != length)
        {
            Console.WriteLine($"Error, your guess must be exactly {length} letters long.");
            continue;
        }

        if (guess.Distinct().Count() != guess.Length)
        {
            Console.WriteLine("Error, your guess must not contain duplicate letters.");
            continue;
        }
        if (!guess.All(c => c >= min && c <= max))
        {
            Console.WriteLine($"Error, your guess can only contain letters between '{min}' and '{max}'");
            continue;
        }

        break; 
    } while (true);

    return guess;
}


// Magically, after breaking this about seven times. It works!
