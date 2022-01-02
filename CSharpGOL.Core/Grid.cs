using CSharpGOL.Core.Config;
using CSharpGOL.Core.Extensions;
using CSharpGOL.Core.Helpers;

namespace CSharpGOL.Core;

public interface IGrid
{
    int Height { get; }
    int Width { get; }

    bool this[int rowIndex, int columnIndex] { get; }

    void SetInitialState();
    void MoveForward();
    void Reset();
    void Randomize(int density);
}

public class Grid : IGrid
{
    private readonly GridOptions _gridOptions;

    // TODO: Consider handling all random number related functionality
    // on the ISimulation level, rather than having this be handled internally by the Grid itself.
    // This would reduce the number of pass through methods in ISimulation.

    // TODO: Make the Grid as "dumb" as possible, and handle the business logic in the Simulation
    // class--treating the Grid only as a model that contains the individual cells.
    private readonly Random _random;

    public bool this[int rowIndex, int columnIndex]
    {
        get => _currentState[rowIndex, columnIndex];
    }

    // TODO: Improve how the initial nullable heigh/width values are handled.
    public int Height => _gridOptions.Height.Value!;
    public int Width => _gridOptions.Width.Value!;

    private bool[,] _initialState { get; set; }
    private bool[,] _currentState { get; set; }
    private bool[,] _previousState { get; set; }

    public Grid(Random random, GridOptions gridOptions)
    {
        _random = random;
        _gridOptions = gridOptions;

        _currentState = new bool[Height, Width];
        _initialState = new bool[Height, Width];
        _previousState = new bool[Height, Width];
    }

    public void MoveForward()
    {
        var stateSwap = _currentState;
        _currentState = _previousState;
        _previousState = stateSwap;

        Action<int, int> moveForwardGridAction = (int x, int y) =>
        {
            int livingNeighborCount = GetLivingNeighborCount(x, y);

            bool isCurrentlyAlive = _previousState[x, y];

            if (isCurrentlyAlive)
            {
                _currentState[x, y] = livingNeighborCount == 2
                    || livingNeighborCount == 3;
            }
            else
            {
                _currentState[x, y] = livingNeighborCount == 3;
            }
        };

        ApplyGridAction(moveForwardGridAction);
    }

    // TODO: If a new "Cell" class is used to replace the original 2D boolean array
    // method, reuse the logic below for setting up the "Neighbors" property (IEnumerable<Cell>)
    // for each of the cells in the grid.
    private int GetLivingNeighborCount(int rowIndex, int columnIndex)
    {
        int nextRowIndex = GridHelper.GetWrapAroundIndex(rowIndex, 1, Height);
        int previousRowIndex = GridHelper.GetWrapAroundIndex(rowIndex, -1, Height);

        int nextColumnIndex = GridHelper.GetWrapAroundIndex(columnIndex, 1, Width);
        int previousColumnIndex = GridHelper.GetWrapAroundIndex(columnIndex, -1, Width);

        var neighborCellCoordinates = new List<(int RowIndex, int ColumnIndex)>
        {
            // Top row of neighbors
            (previousRowIndex, previousColumnIndex),
            (previousRowIndex, columnIndex),
            (previousRowIndex, nextColumnIndex),
                
            // Side neighbors
            (rowIndex, previousColumnIndex),
            (rowIndex, nextColumnIndex),

            // Bottom row of neighbors
            (nextRowIndex, previousColumnIndex),
            (nextRowIndex, columnIndex),
            (nextRowIndex, nextColumnIndex)
        };

        int livingNeighborCount = neighborCellCoordinates.Select(x => _previousState[x.RowIndex, x.ColumnIndex]).Count(x => x);

        return livingNeighborCount;
    }

    public void Randomize(int density)
    {
        Action<int, int> randomizationGridAction = (int x, int y) =>
        {
            bool isAlive = _random.Next(11) > density;
            _currentState[x, y] = isAlive;
        };

        ApplyGridAction(randomizationGridAction);

        _initialState = _currentState.DeepCopy()!;
        _previousState = _currentState.DeepCopy()!;
    }

    // TODO: Would this benefit from parallelization (assuming no side effects)?.
    private void ApplyGridAction(Action<int, int> gridAction)
    {
        for (int x = 0; x < Height; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                gridAction(x, y);
            }
        }

        // TODO: If any performance benefit is gained by it, add a
        // configuration option to enable parallelization.

        //Parallel.For(0, Height, x =>
        //{
        //    for (int y = 0; y < Width; y++)
        //    {
        //        gridAction(x, y);
        //    }
        //});
    }

    public void SetInitialState()
    {
        _initialState = _currentState.DeepCopy()!;
    }

    public void Reset()
    {
        _currentState = _initialState.DeepCopy()!;
    }
}
