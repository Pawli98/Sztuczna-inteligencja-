using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2
{
    class C4State : State
    {
        private int[,] tab;
        private string id;
        private bool isMaxMove;

        public override string ID
        {
            get
			{
                return this.id;
            }
        }

        public int[,] Tab
        {
            get
            {
                return this.tab;
            }
        }

        private void computeId()
        {
            StringBuilder sb = new StringBuilder();
			for (int i = 0; i < this.tab.GetLength (0); ++i)
			{
				for (int j = 0; j < this.tab.GetLength (1); ++j) 
				{
					sb.Append (this.tab [i, j]);
				}
			}
            this.id = sb.ToString();
        }

        public C4State(int[,] tab, bool isMaxFirst)
        {
			this.isMaxMove = isMaxFirst;
			this.Depth = 0.5;//wjebac to do menu w mainie
            this.tab = tab;
            this.computeId();
            this.ComputeHeuristicGrade();
        }

        public C4State(int x, int y, bool isMaxFirst)
        {
            this.tab[x, y] = 2;
            this.isMaxMove = isMaxFirst;
            this.Depth = parent.Depth + 0.5;
            this.computeId();
            this.ComputeHeuristicGrade();
        }

       public C4State(C4State parent, int x, int y)
        {
            this.tab = new int[parent.tab.GetLength(0), parent.tab.GetLength(1)];
            Array.Copy(parent.tab, this.tab, this.tab.LongLength);
            this.isMaxMove = !parent.isMaxMove;
            this.tab[x, y] = this.isMaxMove?2:1;
            this.computeId();
            this.Parent = parent;
            parent.children.Add(this);
            this.Depth = parent.Depth + 0.5;
            if (parent.RootMove == null)
                this.RootMove = id;
            else
                this.RootMove = parent.RootMove;
            this.ComputeHeuristicGrade();
        }

		public double patternCompare(String [] expression, String [] pattern, double [] heuristics)
		{
			int confirmed = 0;
			for (int a = 0; a < expression.Length; ++a)
			{
				for (int b = 0; b < expression[a].Length; ++b)
				{
					for (int c = 0; c < pattern.Length; ++c)
					{
						++confirmed;
						for (int d = 0; d < pattern[c].Length; ++d)
						{
							if (b + d < expression[a].Length)
							if (expression[a][b + d] != pattern[c][d])
							{
								confirmed = 0;
							}
						}
						if (confirmed > 0 && heuristics[c] != System.Double.PositiveInfinity && heuristics[c] != System.Double.NegativeInfinity)
							this.h += heuristics[c];
						if (confirmed > 0 && (heuristics[c] == System.Double.PositiveInfinity || heuristics[c] == System.Double.NegativeInfinity))
							return heuristics[c];
					}
				}
			}
			return h;
		}

        public override double ComputeHeuristicGrade()
        {
            int[,] tmp = new int[this.Tab.GetLength(0), this.Tab.GetLength(1)];
			int length0 = this.Tab.GetLength (0);
			int length1 = this.Tab.GetLength (1);

			StringBuilder horizontally = new StringBuilder();
            for (int i = 0; i < length0; ++i)
            {
                for (int j = 0; j < length1; ++j)
                {
                    horizontally.Append(this.tab[i, j]);
                    tmp[this.Tab.GetLength(0) - 1 - i, j] = this.tab[i, j];
                }
                horizontally.Append(9);
            }

			StringBuilder vertically = new StringBuilder();
			for (int i = 0; i < length1; ++i)
            {
                for (int j = 0; j < length0; ++j)
                {
                    vertically.Append(this.tab[j, i]);
                }
                vertically.Append(9);
            }

			StringBuilder diagL = new StringBuilder();
			StringBuilder diagR = new StringBuilder();
			for (int i = 1 - length1; i < length0; ++i)
            {
                for (int j = 0; j < length1; ++j)
                {
					if (i + j >= 0 && i + j <= length0 - 1)
                    {
                        diagR.Append(this.tab[i + j, j]);
                        diagL.Append(tmp[i + j, j]);
                    }
                }
                diagL.Append(9);
                diagR.Append(9);
            }

            String [] tabResults = new String []
			{ 
			  horizontally.ToString(),
			  vertically.ToString(),
			  diagL.ToString(), 
			  diagR.ToString()
			};
            
            String[] patternsX = new String[] { "0110", "1100", "1010", "0101", "0011", "1110", "1011", "1101", "0111", "00110", "01100", "01010", "1111" };
			double[] heuristicsX = new double[] { 20,     20,     20,     20,     20,     80,    80,    80,    80,    40,    40,      40,     System.Double.PositiveInfinity };

			String[] patternsO = new String[] {  "0220", "2200", "2020", "0202", "0022", "2220", "2022", "2202", "0222", "00220", "02200", "02020", "2222"};
			double[] heuristicsO = new double[] { -20,    -20,    -20,    -20,    -20,    -80,   -80,   -80,   -80,   -40,    -40,    -40,     System.Double.NegativeInfinity };

            this.h = 0;

			if (this.h != System.Double.NegativeInfinity && this.h != System.Double.PositiveInfinity)
			{
				this.h += this.patternCompare (tabResults, patternsX, heuristicsX);
			}
				
			if (this.h != System.Double.NegativeInfinity && this.h != System.Double.PositiveInfinity)
			{
				this.h += this.patternCompare (tabResults, patternsO, heuristicsO);            
			}

            return this.h;
        }

        
    }
}
