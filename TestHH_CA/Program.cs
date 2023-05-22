using AI_Core.Core.Node;
using AI_Core.Core.SearchingAlgorithms.Backtrack;
using AI_Core.Core.SearchingAlgorithms.BreadthFirst;
using AI_Core.Core.SearchingAlgorithms.DepthFirst;
using AI_Core.Core.SearchingAlgorithms.TrialAndError;
using AI_Core.StateRepresentations.HungryHorsemanNState;

Console.CursorVisible = false;

#region Trial and Error
/*
TrialAndErrorHungryHorsemanN trialAndError = new TrialAndErrorHungryHorsemanN(6);
HungryHorsemanNNode terminalNode = trialAndError.Search();
int i = 0;
foreach (var node in trialAndError.GetSolution(terminalNode))
{
    i++;
    Console.WriteLine($"{i}. iteration");
    PrintStateN(node.State);
    Console.WriteLine("\n\n");
    //Thread.Sleep(200);
}
*/
#endregion

#region Backtrack
/*
BacktrackHungryHorsemanN backtrack = new BacktrackHungryHorsemanN(30, true, 8);
HungryHorsemanNNode terminalNode = backtrack.Search();
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
*/
#endregion

#region Depth-First
/*
DepthFirstHungryHorsemanN depthFirst = new DepthFirstHungryHorsemanN(true, 8);
HungryHorsemanNNode terminalNode = depthFirst.Search();
int i = 0;
foreach (var node in depthFirst.GetSolution(terminalNode))
{
    i++;
    Console.WriteLine($"{i}. iteration");
    //Console.WriteLine(node);
    PrintStateN(node.State);
    Console.WriteLine("\n\n");
    Thread.Sleep(200);
}
*/
#endregion

#region Breadth-First
/*
BreadthFirstHungryHorsemanN breadthFirst = new BreadthFirstHungryHorsemanN(true, 8);
HungryHorsemanNNode terminalNode = breadthFirst.Search();
int i = 0;
foreach (var node in breadthFirst.GetSolution(terminalNode))
{
    i++;
    Console.WriteLine($"{i}. iteration");
    //Console.WriteLine(node);
    PrintStateN(node.State);
    Console.WriteLine("\n\n");
    Thread.Sleep(200);
}
*/
#endregion

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