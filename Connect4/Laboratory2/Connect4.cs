using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Laboratory2
{
	class Connect4
    {
		private bool isPlayer = true;
		private int[,] board;
		private int state;
        public int State
        {
            get
			{ 
				return this.state;
			}
            set 
			{ 
				this.state = value; 
			}
        }

		public Connect4()
		{
            this.state = 0;
        }
        public const int column = 6;
        public const int row = 7;

        public void writeMenu()
        {
            Console.Clear();
			Console.WriteLine ("Wybierz tryb");
            Console.WriteLine("1: Gracz z komputerem");
            Console.WriteLine("2: Dwaj gracze komputerowi");
			Console.WriteLine("Inny znak: Wyjście");
            Console.WriteLine();
            this.state = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
             
			if (this.state == 1 || this.state == 2)
			{
				this.board = new int[column, row];
				Console.Clear();
				for (int i = 0; i < column; i++)
				{
					for (int j = 0; j < row; j++)
					{
						this.board[i, j] = 0;
					}
				}
				this.printBoard(false);
			}
			else
				Environment.Exit (0);
        }

		private void printBoard(bool last)
        {
                    
			String divider = "-----------------------------";

			Console.WriteLine(divider);

            for (int i = 0; i < this.board.GetLength(0); ++i)
            {
                Console.Write('│');
                for (int j = 0; j < this.board.GetLength(1); ++j)
                {
                    Console.Write(' ');
					if (this.board [i, j] == 1)
						Console.Write ('X');
					if (this.board [i, j] == 2)
						Console.Write ('O');
					if (this.board [i, j] != 1 && this.board [i, j] != 2)
						Console.Write (' ');
					Console.Write(' ');
                    Console.Write('│');
                }
                Console.WriteLine();
                if (i < this.board.GetLength(0)-1)
                    Console.WriteLine(divider);
            }
            Console.WriteLine(divider);
			Console.WriteLine();
			if (last == false)
			this.move();
        }

        private void move()
        {
	
            if (this.isPlayer)
            {
                if (this.state == 1)
                {

                    int column;


                    int correct;
                    do
                    {
						correct = 0;

                        do
                        {
                            Console.WriteLine("Jaka kolumna?");
                            column = Convert.ToInt32(Console.ReadLine());
                        }
						while (column > this.board.GetLength(1));
                        if (this.board[0, column - 1] != 0)
                            correct = 1;
                        else
							Console.Clear();
                        this.isPlayer = false;


                        for (int i = this.board.GetLength(0) - 1; i >= 0; --i)
                        {
                            if (this.board[i, column - 1] == 0)
                            {
                                this.board[i, column - 1] = 1;
                                break;
                            }
                        }
                    } 
					while (correct != 0);


					this.printBoard(false);
                }


                if (this.state == 2)
                {

                    C4State state = new C4State(board, this.isPlayer);

					if (state.H == System.Double.PositiveInfinity)
						this.result (1);

                    C4Search searcher = new C4Search(state, this.isPlayer, 2);
                    searcher.DoSearch();


                    C4State move = (C4State)searcher.Move;


                    if (move == null)
						this.result(3);

                    this.board = move.Tab;

                    if (move.H == System.Double.NegativeInfinity)
						this.result(2);

                    Console.Clear();
                    Console.WriteLine("Heurystyka: " + move.H);
                    this.isPlayer = false;
                }
            }
            else
            {
                C4State state = new C4State(board, this.isPlayer);
                if (state.H == System.Double.PositiveInfinity)
					this.result(1);
                C4Search searcher = new C4Search(state, this.isPlayer, 2);
                searcher.DoSearch();
                C4State move = (C4State)searcher.Move;
				if (move == null)
					this.result (3);
                this.board = move.Tab;
				if (move.H == System.Double.NegativeInfinity)
					this.result (2);
                Console.Clear();
                Console.WriteLine("Heurystyka: " + move.H);
                this.isPlayer = true;
            }

			this.printBoard (false);
        }

		public void result(int who)
		{
			if (who == 1) 
			{
				this.isPlayer = true;
				Console.WriteLine ("----------");
				Console.WriteLine ("Wygrał gracz nr 1");
				Console.WriteLine ("----------");
				this.printBoard (true);
				Console.ReadKey ();
				Environment.Exit (0);
			}
			if (who == 2) 
			{
				this.isPlayer = true;
				Console.WriteLine ("----------");
				Console.WriteLine ("Wygrał gracz nr 2");
				Console.WriteLine ("----------");
				this.printBoard (true);
				Console.ReadKey ();
				Environment.Exit (0);
			}
			if (who == 3)
			{
				this.isPlayer = true;
				Console.WriteLine ("----------");
				Console.WriteLine ("REMIS");
				Console.WriteLine ("----------");
				this.printBoard (true);
				Console.ReadKey ();
				Environment.Exit (0);
			}
		}
    }
}
