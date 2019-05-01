using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    class SudokuSearch : AStarSearch
    {
        public SudokuSearch(SudokuState aState) : base(aState, true, true)
        {
            
        }
        protected override void buildChildren(IState aParent)
        {
            SudokuState state = (SudokuState)aParent; 
			int x = 0;
			int y = 0;
			int [] tab;
			int [] tab2 = new int[9];
			for (int i = 0; i < 9; ++i) tab2[i] = 0;
            int spr, tmp=0;
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    tab = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    spr = 0;
                    if (state.Board[i, j] == 0)
                    {
                        for (int k = 0; k < 9; ++k)
                        {
							if (state.Board [i, k] != 0 && k != j)
							{
								tab[state.Board[i, k] - 1] = 0;
							}
							if (state.Board [k, j] != 0 && k != i) 
							{
								tab[state.Board[k, j] - 1] = 0;
							}
                                
                            for (int l = 0; l < 9; ++l)
                            {
                                if (k == i && l == j)
                                    continue;

                                if (i <= 2 && k > 2)
                                    continue;
                                if (j <= 2 && l > 2)
                                    continue;

                                if (i >2 && i<=5 && (k<=2 || k>5))
                                    continue;
                                if (j > 2 && j <= 5 && (l <= 2 || l > 5))
                                    continue;

                                if (i > 5 && k <= 5)
                                    continue;
                                if (j > 5 && l <= 5)
                                    continue;

                                if (state.Board[k, l] != 0)
                                    tab[state.Board[k, l] - 1] = 0;
                            }
                        }
						for (int k = 0; k < 9; ++k)
                        {
                            if (tab[k] == 0)
                                ++spr;
                        }
                        if (spr > tmp)
                        {
                            tmp = spr;
                            x = i; 
							y = j;
                            tab2 = tab;
                        }
                    }
                }
            }
            for (int i = 0; i < 9; ++i)
            {
                if (tab2[i] != 0)
                {
                    new SudokuState(state, x, y, tab2[i]);
                }
            }
        }
        protected override bool isSolution(IState aState)
        {
			return (aState.H == 0);
        }
    }
}
