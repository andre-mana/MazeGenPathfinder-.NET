namespace MazeGenerationAndPathFinding
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Cell? PreviousCell { get; set; }
        public int DistanceToSourceNode { get; set; } = int.MaxValue;
        public bool Visited { get; set; }
        public bool IsObstacle { get; set; }
        public List<Cell> ConnectedCells { get; set; } = new();
        private string? cellType = "██";
        public string? CellType
        {
            get => cellType;
            set => cellType = value ?? "██";
        }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
