//using CSharpGOL.Core.Extensions;
//using CSharpGOL.Core.Helpers;

//namespace CSharpGOL.Core
//{
//    public interface IGrid
//    {
//        int RowLength { get; }
//        int ColumnLength { get; }

//        bool this[int rowIndex, int columnIndex] { get; }

//        void MoveForward();
//        void Reset();
//        void Randomize(int density);
//    }

//    public class Grid : IGrid
//    {
//        private bool[,] _initialState { get; set; }
//        private bool[,] _currentState { get; set; }
//        private bool[,] _previousState { get; set; }

//        //public int RowLength => _currentState.GetLength(0);
//        //public int ColumnLength => _currentState.GetLength(1);

//        public int RowLength { get; set; }
//        public int ColumnLength { get; set; }

//        public IList<Cell> Cells { get; set; } = new List<Cell>();

//        public Dictionary<(int, int), Cell> CellMap = new Dictionary<(int, int), Cell>();

//        public Cell[,] CellGrid { get; set; }



//        public Grid(int height, int width, int density = 5)
//        {
//            RowLength = height;
//            ColumnLength = width;

//            //_currentState = new bool[height, width];

//            //var rand = new Random();
//            //for (int x = 0; x < RowLength; x++)
//            //{
//            //    for (int y = 0; y < ColumnLength; y++)
//            //    {
//            //        bool isAlive = rand.Next(11) > density;
//            //        _currentState[x, y] = isAlive;
//            //    }
//            //}

//            //_initialState = _currentState.DeepCopy()!;
//            //_previousState = _currentState.DeepCopy()!;

//            CellGrid = new Cell[height, width];

//            var rand = new Random();
//            for (int x = 0; x < RowLength; x++)
//            {
//                for (int y = 0; y < ColumnLength; y++)
//                {
//                    bool isAlive = rand.Next(11) > density;

//                    var cell = new Cell(x, y);
//                    cell.State = isAlive;

//                    CellMap.Add((x, y), cell);
//                    Cells.Add(cell);
//                    CellGrid[x, y] = cell;
//                }
//            }

//            SetupNeighbors();
//        }

//        private void SetupNeighbors()
//        {
//            foreach (Cell cell in Cells)
//            {
//                int rowIndex = cell.RowIndex;
//                int columnIndex = cell.ColumnIndex; 

//                int nextRowIndex = GridHelper.GetWrapAroundIndex(rowIndex, 1, RowLength);
//                int previousRowIndex = GridHelper.GetWrapAroundIndex(rowIndex, -1, RowLength);

//                int nextColumnIndex = GridHelper.GetWrapAroundIndex(columnIndex, 1, ColumnLength);
//                int previousColumnIndex = GridHelper.GetWrapAroundIndex(columnIndex, -1, ColumnLength);

//                var neighborCellCoordinates = new List<(int RowIndex, int ColumnIndex)>
//                {
//                    // Top row of neighbors
//                    (previousRowIndex, previousColumnIndex),
//                    (previousRowIndex, columnIndex),
//                    (previousRowIndex, nextColumnIndex),
                
//                    // Side neighbors
//                    (rowIndex, previousColumnIndex),
//                    (rowIndex, nextColumnIndex),

//                    // Bottom row of neighbors
//                    (nextRowIndex, previousColumnIndex),
//                    (nextRowIndex, columnIndex),
//                    (nextRowIndex, nextColumnIndex)
//                };

//                cell.Neighbors = neighborCellCoordinates.Select(x => CellMap[x]).ToList();
//            }
//        }

//        public bool this[int rowIndex, int columnIndex]
//        {
//            //get => _currentState[rowIndex, columnIndex];
//            get => CellGrid[rowIndex, columnIndex].State;
//        }

//        public void MoveForward()
//        {
//            //_previousState = _currentState.DeepCopy()!;

//            //var stateSwap = _currentState;
//            //_currentState = _previousState;
//            //_previousState = stateSwap;

//            //for (int x = 0; x < RowLength; x++)
//            //{
//            //    for (int y = 0; y < ColumnLength; y++)
//            //    {
//            //        int livingNeighborCount = GetLivingNeighborCount(x, y);
//            //        bool currentlyAlive = _previousState[x, y];

//            //        if (currentlyAlive)
//            //        {
//            //            _currentState[x, y] = livingNeighborCount == 2
//            //                || livingNeighborCount == 3;
//            //        }
//            //        else
//            //        {
//            //            _currentState[x, y] = livingNeighborCount == 3;
//            //        }
//            //    }
//            //}

//            foreach (Cell cell in Cells)
//            {
//                cell.SavePrevious();
//            }

//            foreach (Cell cell in Cells)
//            {
//                cell.MoveForward();
//            }
//        }

//        private int GetLivingNeighborCount(int rowIndex, int columnIndex)
//        {
//            int nextRowIndex = GridHelper.GetWrapAroundIndex(rowIndex, 1, RowLength);
//            int previousRowIndex = GridHelper.GetWrapAroundIndex(rowIndex, -1, RowLength);

//            int nextColumnIndex = GridHelper.GetWrapAroundIndex(columnIndex, 1, ColumnLength);
//            int previousColumnIndex = GridHelper.GetWrapAroundIndex(columnIndex, -1, ColumnLength);

//            var neighborCellCoordinates = new List<(int RowIndex, int ColumnIndex)>
//            {
//                // Top row of neighbors
//                (previousRowIndex, previousColumnIndex),
//                (previousRowIndex, columnIndex),
//                (previousRowIndex, nextColumnIndex),
                
//                // Side neighbors
//                (rowIndex, previousColumnIndex),
//                (rowIndex, nextColumnIndex),

//                // Bottom row of neighbors
//                (nextRowIndex, previousColumnIndex),
//                (nextRowIndex, columnIndex),
//                (nextRowIndex, nextColumnIndex)
//            };

//            int livingNeighborCount = neighborCellCoordinates.Select(x => _previousState[x.RowIndex, x.ColumnIndex]).Count(x => x);

//            return livingNeighborCount;
//        }

//        public void Reset()
//        {
//            _currentState = _initialState.DeepCopy()!;
//        }

//        public void Randomize(int density = 5)
//        {
//            var rand = new Random();
//            for (int x = 0; x < RowLength; x++)
//            {
//                for (int y = 0; y < ColumnLength; y++)
//                {
//                    bool isAlive = rand.Next(11) > density;
//                    _currentState[x, y] = isAlive;
//                }
//            }

//            _initialState = _currentState.DeepCopy()!;
//            _previousState = _currentState.DeepCopy()!;
//        }

//        public class Cell
//        {
//            public int RowIndex { get; }
//            public int ColumnIndex { get; }
//            public bool State { get; set; }
//            public bool PreviousState { get; set; }

//            public IEnumerable<Cell> Neighbors { get; set; } = new List<Cell>();

//            public Cell(int rowIndex, int columnIndex)
//            {
//                RowIndex = rowIndex;
//                ColumnIndex = columnIndex;
//            }

//            public void SavePrevious()
//            {
//                PreviousState = State;
//            }

//            public void MoveForward()
//            {
//                int livingNeighbors = Neighbors.Count(n => n.PreviousState);

//                if (PreviousState)
//                {
//                    State = livingNeighbors == 2 || livingNeighbors == 3;
//                }
//                else
//                {
//                    State = livingNeighbors == 3;
//                }
//            }
//        }
//    }
//}
