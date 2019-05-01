using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    class PuzzleSearch : AStarSearch
    {
		public PuzzleSearch(PuzzleState aState) : base(aState, true, true)
        {
            
        }
        protected override void buildChildren(IState aParent)
        {
            PuzzleState state = (PuzzleState)aParent; // rzutujemy podany stan na PuzzleState
			int row = 0;
			int column = 0;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (state.Board[i, j] == 0) // cala ta petla w petli ma na celu znalezienie gdzie jest puste pole
                    {
                        row = i;
                        column = j;
						goto outside;
                    }
                }
            }
			outside:
			if (column < 2) // tworzymy potencjalne cele potomne; jesli column < 2 to mozliwy jest ruch w lewo
            {
                state.Board[row, column] = state.Board[row, column + 1]; // chwilowo zmieniamy tablice na taka, jaka powinien miec stan potomny
                state.Board[row, column + 1] = 0;
				new PuzzleState(state); // podanie new PuzzleState i state jako argument powoduje ze ten nowy stan podpina sie jako dziecko
                state.Board[row, column + 1] = state.Board[row, column]; // przywracamy domyslny stan tablicy
                state.Board[row, column] = 0;
            }
			// ruchy w pozostale kierunki analogicznie
            if (column > 0)
            {
                state.Board[row, column] = state.Board[row, column - 1];
                state.Board[row, column - 1] = 0;
                new PuzzleState(state); 
                state.Board[row, column - 1] = state.Board[row, column];
                state.Board[row, column] = 0;
            }
			if (row < 2)
			{
				state.Board[row, column] = state.Board[row + 1, column];
				state.Board[row + 1, column] = 0;
				new PuzzleState(state);
				state.Board[row + 1, column] = state.Board[row, column];
				state.Board[row, column] = 0;
			}
			if (row > 0)
			{
				state.Board[row, column] = state.Board[row - 1, column];
				state.Board[row - 1, column] = 0;
				new PuzzleState(state);
				state.Board[row - 1, column] = state.Board[row, column];
				state.Board[row, column] = 0;
			}
        }
        protected override bool isSolution(IState state)
        {
			return (state.H == 0);
        }
    }
}
