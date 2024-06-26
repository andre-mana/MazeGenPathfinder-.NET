namespace MazeGenerationAndPathFinding
{
    public static class MazeManager
    {
        public static List<List<Cell>> CellGrid { get; private set; } = new();
        public static int BoardLength { get; set; }
        public static int BoardHeight { get; set; }

        public static void Initialize(int boardLength, int boardHeight)
        {
            BoardLength = boardLength;
            BoardHeight = boardHeight;
            CellGrid = new List<List<Cell>>(boardLength);

            for (int i = 0; i < boardLength; i++)
            {
                var column = new List<Cell>(boardHeight);
                for (int j = 0; j < boardHeight; j++)
                {
                    column.Add(new Cell(i, j));
                }
                CellGrid.Add(column);
            }
        }
    }
}
