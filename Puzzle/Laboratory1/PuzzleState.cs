using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    class PuzzleState : State
    {
        private int[,] board;
        public int[,] Board
		{
            get 
			{ 
				return this.board;
			}
            set 
			{ 
				this.Board = value; 
			}
        }
        public override string ID
		{
            get 
            { 
                StringBuilder sb = new StringBuilder();
				for (int i = 0; i < this.board.GetLength (0); ++i)
				{
					for (int j = 0; j < this.board.GetLength (1); ++j) 
					{
						sb.Append(this.board[i, j]);
					}
				}
                return sb.ToString();
            }
        }
        public override double ComputeHeuristicGrade()
        {
            double hV = 0;
            for (int i = 0; i < this.board.GetLength(0); ++i)
                for (int j = 0; j < this.board.GetLength(1); ++j)
                {
					if (this.board[i, j] != 0)
                    {
						
						// wartosc heurystyki = abs(koordynat x gdzie puzel powinien byc - koordynat gdzie faktycznie jest)
						// + abs(koordynat y gdzie puzel powinien byc - koordynat gdzie faktycznie jest)
						// aby prawidlowo obliczalo gdzie faktycznie jest dla x trza zrobic dzielenie przez 3 i wykorzystac obcinanie czesci po przecinku
						// a dla y modulo 3. trza jeszcze zrobic - 1 na wartosci, bo liczymy od 0 a wzor zaklada od 1
                        hV += Math.Abs(i - ((this.board[i, j] - 1) / 3)) + Math.Abs(j - (this.board[i, j] - 1) % 3);
                    }
                }
            return hV;
        }
        public PuzzleState(string aString)
        {
            this.board = new int[3, 3];
			for (int i = 0; i < 3; ++i) 
			{
				for (int j = 0; j < 3; ++j) 
				{
					this.board[i, j] = int.Parse (aString.ElementAt(i * 3 + j).ToString());
				}
			}
            this.h = ComputeHeuristicGrade();
        }
        public PuzzleState(PuzzleState aState)
        {
            this.board = new int[3, 3];
			for (int i = 0; i < 3; ++i)
			{
				for (int j = 0; j < 3; ++j) 
				{
					this.board [i, j] = aState.board [i, j]; // kopiujemy tablice stanu
				}
			}
            this.Parent = aState;
            aState.Children.Add(this); // dodajemy ten stan jako dziecko stanu przekazanego w argumencie
            this.h = ComputeHeuristicGrade();
            this.g = parent.G + 1;
        }
        public void Write() // wypis na konsoli
		{
            

                Console.Write("Latwe te puzle \n");
                Console.Write("|");
                Console.Write(this.board[0, 0]);
                Console.Write("||");
                Console.Write(this.board[0, 1]);
                Console.Write("||");
                Console.Write(this.board[0, 2]);
                Console.Write("|");
                Console.WriteLine();
                Console.Write("|");
                Console.Write(this.board[1, 0]);
                Console.Write("||");
                Console.Write(this.board[1, 1]);
                Console.Write("||");
                Console.Write(this.board[1, 2]);
                Console.Write("|");
                Console.WriteLine();
                Console.Write("|");
                Console.Write(this.board[2, 0]);
                Console.Write("||");
                Console.Write(this.board[2, 1]);
                Console.Write("||");
                Console.Write(this.board[2, 2]);
                Console.Write("|");
                Console.WriteLine();
            
        }
        public void Shuffle() // funkcja do mieszania puzli
        {
			int row = 2;
			int column = 2; // poczatkowo ustawiamy wolne pole w prawym dolnym rogu
            Random randomizer = new Random(); // obiekt generujacy losowe liczby
            int direction;
            for (int i = 0; i < 1000; ++i) // wykonujemy 100 mieszan
            {
                while (true)
                {
					direction = randomizer.Next(1, 5); // losujemy liczbe 1,2,3,
					if (direction == 1 && row < 2) // kierunek w dol + sprawdzenie czy mozemy isc w dol
                    {
                        this.board[row, column] = this.board[row + 1, column]; // przestawiamy puste pole
						++row; // zmieniamy info gdzie jest puste pole
                        this.board[row, column] = 0;
						break;
                    }
					// pozostale analogicznie
					if (direction == 2 && row > 0)
                    {
                        this.board[row, column] = this.board[row - 1, column];
						--row;
                        this.board[row, column] = 0;
						break;
                    }
					if (direction == 3 && column < 2)
                    {
						this.board[row, column] = this.board[row, column + 1];
						++column;
                        this.board[row, column] = 0;
						break;
                    }
					if (direction == 4 && column > 0)
                    {
						this.board[row, column] = this.board[row, column - 1];
						--column;
                        this.board[row, column] = 0;
						break;
                    }
                }
            }
            this.h = this.ComputeHeuristicGrade();
        }
    }
}
