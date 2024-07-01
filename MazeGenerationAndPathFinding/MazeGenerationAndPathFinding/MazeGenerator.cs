namespace MazeGenerationAndPathFinding
{
    internal class MazeGenerator
    {
        public static void GenerateMaze()
        {
            MazeManager.Initialize(26, 26);
            DFSMazeGeneration();
        }

        private static void DFSMazeGeneration()
        {
            Stack<Cell> stack = new Stack<Cell>();
            List<Cell> neighbours;
            Cell currentCell;

            stack.Push(MazeManager.CellGrid[0][0]);

            while (stack.Count > 0)
            {
                currentCell = stack.Peek();
                currentCell.Visited = true;
                currentCell.CellType = "  ";
                neighbours = GetUnvisitedNeighbours(currentCell);

                if (neighbours.Count > 0)
                {
                    var random = new Random();
                    int rInt = random.Next(0, neighbours.Count);
                    var neighbour = neighbours[rInt];

                    // Remove wall between cells. Neighbours are separated two apart
                    MazeManager.CellGrid[(neighbour.X + currentCell.X) / 2][(neighbour.Y + currentCell.Y) / 2].CellType = "  ";

                    stack.Push(neighbour);
                }
                else
                {
                    stack.Pop();
                }
            }

            MazeManager.CellGrid[0][0].CellType = "  "; // Beginning cell
            MazeManager.CellGrid[MazeManager.BoardLength - 1][MazeManager.BoardHeight - 2].CellType = "  "; // End cell
        }

        private static List<Cell> GetUnvisitedNeighbours(Cell cell)
        {
            var neighbours = new List<Cell>();
            int x = cell.X;
            int y = cell.Y;

            // Check left neighbor
            if (x > 0 && !MazeManager.CellGrid[x - 2][y].Visited)
            {
                neighbours.Add(MazeManager.CellGrid[x - 2][y]);
            }

            // Check right neighbor
            if (x < MazeManager.BoardLength - 2 && !MazeManager.CellGrid[x + 2][y].Visited)
            {
                neighbours.Add(MazeManager.CellGrid[x + 2][y]);
            }

            // Check up neighbor
            if (y > 0 && !MazeManager.CellGrid[x][y - 2].Visited)
            {
                neighbours.Add(MazeManager.CellGrid[x][y - 2]);
            }

            // Check down neighbor
            if (y < MazeManager.BoardHeight - 2 && !MazeManager.CellGrid[x][y + 2].Visited)
            {
                neighbours.Add(MazeManager.CellGrid[x][y + 2]);
            }
            return neighbours;
        }
    }
}
