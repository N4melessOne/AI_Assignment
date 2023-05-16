﻿using AI_Core.StateRepresentations.ColoredDisksState;

#region Solution with random steps
Random rnd = new Random();
ColoredDisksState state = new ColoredDisksState(4);
Console.WriteLine("Starting state:");
PrintState(state);
do
{
    int index = rnd.Next(state.Size);
    Directions rowOrColumn = (Directions)rnd.Next(2);
    state.ApplyOperator(rowOrColumn, index);

    Console.WriteLine("Current state:");
    PrintState(state);
    //Thread.Sleep(1000);
} while (!state.IsGoalState());
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