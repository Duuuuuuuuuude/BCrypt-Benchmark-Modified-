using System.Diagnostics;
using BCryptNet = BCrypt.Net.BCrypt;

#region How long to hash. Taken from https://auth0.com/blog/hashing-in-action-understanding-bcrypt/
Console.WriteLine("Running speed tests:\n");
string plainTextPassword = "DFGh5546*%^__90";

for (int saltRounds = 10; saltRounds < 21; saltRounds++)
{
    Stopwatch sw = Stopwatch.StartNew();
    BCryptNet.HashPassword(plainTextPassword, saltRounds);
    Console.WriteLine($"{saltRounds - 9}. test | cost = {saltRounds}, time to hash = {sw.ElapsedMilliseconds}ms");
}
Console.WriteLine();
#endregion



#region Modified benchmark from https://github.com/BcryptNet/bcrypt.net
var cost = 16;
var timeTarget = 400; // Milliseconds
long timeTaken;
int iterationsSoFar = 1;
int iterationsToDo = 10;

Console.WriteLine("Running Appropriate cost tests:\n");
do
{
    do
    {
        var sw = Stopwatch.StartNew();

        try
        {
            BCryptNet.HashPassword("RwiKnN>9xg3*C)1AZl.)y8f_:GCz,vt3T]PI", workFactor: cost);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("BCrypt doesn't allow cost to go below 4");
            Console.WriteLine("\nDone");
            return;
        }

        sw.Stop();
        timeTaken = sw.ElapsedMilliseconds;

        cost -= 1;

    } while ((timeTaken) >= timeTarget);

    Console.WriteLine($"iteration: {iterationsSoFar++} ");
    Console.WriteLine($" Appropriate Cost Found: {cost + 1} Time taken: {timeTaken}ms Target time = {timeTarget}ms\n");

    cost = 16;
    timeTarget -= 50;
} while (iterationsSoFar < iterationsToDo);

Console.WriteLine("done");
Console.ReadLine();
#endregion


#region Original benchmark from https://github.com/BcryptNet/bcrypt.net
//var cost = 16;
//var timeTarget = 100; // Milliseconds
//long timeTaken;
//do
//{
//    var sw = Stopwatch.StartNew();

//    BC.HashPassword("RwiKnN>9xg3*C)1AZl.)y8f_:GCz,vt3T]PI", workFactor: cost);

//    sw.Stop();
//    timeTaken = sw.ElapsedMilliseconds;

//    cost -= 1;

//} while ((timeTaken) >= timeTarget);

//Console.WriteLine("Appropriate Cost Found: " + (cost + 1));
//Console.ReadLine(); 
#endregion


