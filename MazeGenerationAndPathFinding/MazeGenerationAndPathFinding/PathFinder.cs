namespace MazeGenerationAndPathFinding
{
    internal static class PathFinder
    {
        public static void FindPath()
        {
            InitializePathFinder();
            RunDijkstra();
            InsertFinalPath();
            DrawGrid();
        }

        private static void InitializePathFinder()
        {
            for (int j = 0; j < MazeManager.BoardLength; j++)
            {
                for (int i = 0; i < MazeManager.BoardHeight; i++)
                {
                    var cell = MazeManager.CellGrid[j][i];

                    if (j > 0 && MazeManager.CellGrid[j - 1][i].CellType == "  ")
                        cell.ConnectedCells.Add(MazeManager.CellGrid[j - 1][i]); // left
                    if (j < MazeManager.BoardLength - 1 && MazeManager.CellGrid[j + 1][i].CellType == "  ")
                        cell.ConnectedCells.Add(MazeManager.CellGrid[j + 1][i]); // right
                    if (i > 0 && MazeManager.CellGrid[j][i - 1].CellType == "  ")
                        cell.ConnectedCells.Add(MazeManager.CellGrid[j][i - 1]); // up
                    if (i < MazeManager.BoardHeight - 1 && MazeManager.CellGrid[j][i + 1].CellType == "  ")
                        cell.ConnectedCells.Add(MazeManager.CellGrid[j][i + 1]); // down
                }
            }
        }

        private static void RunDijkstra()
        {
            var unvisited = new List<Cell>();
            var path = new List<Cell>();

            foreach (var line in MazeManager.CellGrid)
                foreach (var cell in line)
                    unvisited.Add(cell);

            var actualCell = MazeManager.CellGrid[0][0];
            actualCell.PreviousCell = actualCell;
            actualCell.DistanceToSourceNode = 0;

            while (!path.Contains(MazeManager.CellGrid[MazeManager.BoardLength - 1][MazeManager.BoardHeight - 2]))
            {
                int lowestDistanceToSource = int.MaxValue;
                int indexLowestDistanceToSource = 0;

                foreach (var neighbour in actualCell.ConnectedCells)
                {
                    int distance = 1 + actualCell.DistanceToSourceNode;
                    if (neighbour.DistanceToSourceNode > distance)
                    {
                        neighbour.DistanceToSourceNode = distance;
                        neighbour.PreviousCell = actualCell;
                    }
                }

                actualCell.Visited = true;
                unvisited.Remove(actualCell);

                for (int i = 0; i < unvisited.Count; i++)
                {
                    if (unvisited[i].DistanceToSourceNode < lowestDistanceToSource)
                    {
                        lowestDistanceToSource = unvisited[i].DistanceToSourceNode;
                        indexLowestDistanceToSource = i;
                    }
                }

                actualCell = unvisited[indexLowestDistanceToSource];
                path.Add(actualCell);
            }
        }

        private static void InsertFinalPath()
        {
            var cellFinalPath = MazeManager.CellGrid[MazeManager.BoardLength - 1][MazeManager.BoardHeight - 2];

            while (cellFinalPath.PreviousCell != null && cellFinalPath.PreviousCell != MazeManager.CellGrid[0][0])
            {
                cellFinalPath = cellFinalPath.PreviousCell;
                MazeManager.CellGrid[cellFinalPath.X][cellFinalPath.Y].CellType = "XX";
            }

            if (cellFinalPath.PreviousCell != null)
            {
                MazeManager.CellGrid[cellFinalPath.X][cellFinalPath.Y].CellType = "XX";
            }

            MazeManager.CellGrid[MazeManager.BoardLength - 1][MazeManager.BoardHeight - 2].CellType = "XX";
            MazeManager.CellGrid[0][0].CellType = "XX";
        }

        private static void DrawGrid()
        {
            for (int j = 0; j < MazeManager.BoardLength; j++)
            {
                for (int i = 0; i < MazeManager.BoardHeight; i++)
                {
                    if (MazeManager.CellGrid[j][i].CellType == "██")
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    if (MazeManager.CellGrid[j][i].CellType == "XX")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        MazeManager.CellGrid[j][i].CellType = "██";
                    }

                    // Ensure CellType is not null by providing a default value
                    Console.Write(MazeManager.CellGrid[j][i].CellType ?? "██");
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPress ENTER to generate a new maze and solution");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
