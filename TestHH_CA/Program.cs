using AI_Core.Core.Node;
using AI_Core.Core.SearchingAlgorithms.Backtrack;
using AI_Core.StateRepresentations.HungryHorsemanNState;

Console.CursorVisible = false;

BacktrackHungryHorsemanN backtrack = new BacktrackHungryHorsemanN(30, true, 8);
HungryHorsemanNNode terminalNode = backtrack.Search();

//backtrack.PrintSolution(terminalNode);
int i = 0;
foreach (var node in backtrack.GetSolution(terminalNode))
{
    i++;
    Console.WriteLine($"{i}. iteration");
    //Console.WriteLine(node);
    PrintStateN(node.State);
    Console.WriteLine("\n\n");
    Thread.Sleep(200);
}

static void PrintStateN(HungryHorsemanN state)
{
    for (int i = 0; i < state.Size; i++)
    {
        for (int j = 0; j < state.Size; j++)
        {
            if (state.X == i && state.Y == j)
                Console.Write("H");
            else Console.Write("_");
        }
        Console.WriteLine();
    }
}