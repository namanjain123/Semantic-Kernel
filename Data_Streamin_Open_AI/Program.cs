// See https://aka.ms/new-console-template for more information
using Data_Streamin_Open_AI;

Console.WriteLine("write you promnpt \n");
string input = Console.ReadLine()??"No prompt was their";
if (input == "No prompt was their")
{
    Console.WriteLine(input);
}
else
{
    await Streaming.RunAsync(input);
}