using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2
{
    class C4Search : AlphaBetaSearcher
    {
        public C4Search(IState state, bool isMaxFirst, double depth) : base(state, isMaxFirst, depth)
		{

        }
        protected override void buildChildren(IState parent)
        {
            C4State state = (C4State)parent;
            for (int j = 0; j < state.Tab.GetLength(1); ++j)
            {
                for (int i = state.Tab.GetLength(0)-1; i >= 0; --i)
                {
                    if (state.Tab[i, j] == 0)
                    {
                        C4State child = new C4State(state, i, j);
                        break;
                    }
                }
            }
        }
    }
}
