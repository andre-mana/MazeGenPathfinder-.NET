using System;

namespace MazeGenerationAndPathFinding
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                MazeManager.Initialize(26, 26);
                MazeGenerator.GenerateMaze();
                PathFinder.FindPath();
            }
        }
    }
}
