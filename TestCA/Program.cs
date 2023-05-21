using AI_Core.Core.Node;
using AI_Core.Core.SearchingAlgorithms.Backtrack;
using AI_Core.Core.SearchingAlgorithms.DepthFirst;
using AI_Core.StateRepresentations.ColoredDisksState;

#region Solution with random steps

/*
Random rnd = new Random();
ColoredDisksState state = new ColoredDisksState(4);
Console.WriteLine("Starting state:");
PrintState(state);
int i = 0;
do
{
    i++;
    Console.WriteLine($"{i}. iteration");
    int index = rnd.Next(state.Size);
    Directions rowOrColumn = (Directions)rnd.Next(2);
    state.ApplyOperator(rowOrColumn, index);

    Console.WriteLine("Current state:");
    PrintState(state);
    //Thread.Sleep(1000);
} while (!state.IsGoalState());*/
#endregion

#region SolutionWithBacktrack

/*
BacktrackColoredDisks backtrackColoredDisks = new BacktrackColoredDisks(1000, true, 4);
Console.WriteLine("Starting search...");
ColoredDisksNode terminalNode = backtrackColoredDisks.Search(); //null-t ad vissza, gyakorlatilag nincs megoldása.
Console.WriteLine("Search has finished.");
int i = 0;
foreach (var node in backtrackColoredDisks.GetSolution(terminalNode))
{
    i++;
    Console.WriteLine($"{i}. iteration");
    PrintState(node.State);
    Console.WriteLine("\n\n");
}*/

#endregion

#region MyRegion

DepthFirstColoredDisks depthFirstColoredDisks = new DepthFirstColoredDisks(true);
Console.WriteLine("Starting search...");
ColoredDisksNode terminalNode = depthFirstColoredDisks.Search(); //null-t ad vissza, gyakorlatilag nincs megoldása.
Console.WriteLine("Search has finished.");
int i = 0;
foreach (var node in depthFirstColoredDisks.GetSolution(terminalNode))
{
    i++;
    Console.WriteLine($"{i}. iteration");
    PrintState(node.State);
    Console.WriteLine("\n\n");
}

#endregion


static void PrintState(ColoredDisksState state)
{
    bool[,] diskMatrix = state.Disks;
    
    for (int i = 0; i < diskMatrix.GetLength(0); i++)
    {
        for (int j = 0; j < diskMatrix.GetLength(1); j++)
        {
            if (diskMatrix[i, j])
                Console.ForegroundColor = ConsoleColor.Blue;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('O');
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}