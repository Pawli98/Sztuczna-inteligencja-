using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    class SudokuState : State
    {
        private int [,] board; // tablica sudoku
        public int [,] Board // getter i setter, odwolanie do nich nastepuje poprzez slowo Board
		{
            get 
			{ 
				return this.board; 
			}
            set
			{ 
				this.board = value; 
			}
        }
        public override string ID // metoda do konwersji sudoku na string
		{
			get 
			{ 
				StringBuilder sb = new StringBuilder (); // sklejacz stringow
				for (int i = 0; i < this.board.GetLength (0); ++i)
					for (int j = 0; j < this.board.GetLength (1); ++j)
						sb.Append (this.board [i, j]); // doklejamy do budowanego przez sb stringu kolejne wartosci pol sudoku
				return sb.ToString (); // zwracamy string wyprodukowany przez sb
			}
		}
        public override double ComputeHeuristicGrade() // obliczamy heurystyke
        {
            double hV = 0;
			for (int i = 0; i < this.board.GetLength (0); ++i) 
			{
				for (int j = 0; j < this.board.GetLength (1); ++j) 
				{
					if (this.board [i, j] == 0)
					{
						++hV; // heurystyka to zasadniczo ilosc wszystkich pol, ktore nie sa wypelnione
					}
				}
			}
            return hV;
            
        }
        public SudokuState(string aString) // konstruktor do tworzenia sudoku ze stringu
        {
            this.board = new int[9, 9];
			for (int i = 0; i < 9; ++i) 
			{
				for (int j = 0; j < 9; ++j)
				{
					this.board [i, j] = int.Parse (aString.ElementAt(i * 9 + j).ToString()); // kolejne znaczki stringu przerabiamy na kolejne liczby tablicy
				}
			}  
			this.h = ComputeHeuristicGrade(); // obliczamy od razu heurystyke (zmienna h w klasie bazowej tej klasy) za pomoca metody z tej klasy
        }
		public SudokuState(SudokuState aParent, int aRows, int aColumns, int aNumber) // tworzymy stan jako dziecko stanu rodzicielskiego (analogicznie jak np. w drzewie BST)
		{ // stan taki bedzie identyczny jak jego rodzic z tym, ze 1 komorka bedzie inna (liczba ktora dodalismy na plansze w jakiejs kratce)
            this.board = new int[9, 9];
			for (int i = 0; i < 9; ++i) 
			{
				for (int j = 0; j < 9; ++j) 
				{
					this.board [i, j] = aParent.board [i, j]; // kopiujemy plansze od rodzica
				}
			}
			this.Parent = aParent; // ustawiamy, ze rodzic tego stanu to podany aParent
            this.board[aRows, aColumns] = aNumber; // ustawiamy dana komorke planszy na inna wartosc, okresla ja aNumber
            aParent.Children.Add(this); // ustawiamy w rodzicu, ze ten stan bedzie jednym z jego dzieci
            this.h = ComputeHeuristicGrade(); // obliczamy heurystyke tego stanu
        }
        public void Write() // metoda do wypisania na terminalu planszy tego stanu
		{
            
			Console.WriteLine("----------------------");
			for (int j = 0; j < 3; ++j) 
			{
              

                Console.Write("|");
				for (int i = 0; i < 3; ++i) Console.Write (this.board [j, i] + " ");
				Console.Write("|");
				for (int i = 0; i < 3; ++i) Console.Write (this.board [j, i+3] + " ");
				Console.Write("|");
				for (int i = 0; i < 3; ++i) Console.Write (this.board [j, i+6] + " ");
				Console.Write("|");
				Console.WriteLine ();
			}
			Console.WriteLine("----------------------");
			for (int j = 0; j < 3; ++j) 
			{
				Console.Write("|");
				for (int i = 0; i < 3; ++i) Console.Write (this.board [j+3, i] + " ");
				Console.Write("|");
				for (int i = 0; i < 3; ++i) Console.Write (this.board [j+3, i+3] + " ");
				Console.Write("|");
				for (int i = 0; i < 3; ++i) Console.Write (this.board [j+3, i+6] + " ");
				Console.Write("|");
				Console.WriteLine ();
			}
			Console.WriteLine("----------------------");
			for (int j = 0; j < 3; ++j) 
			{
				Console.Write("|");
				for (int i = 0; i < 3; ++i) Console.Write (this.board [j+6, i] + " ");
				Console.Write("|");
				for (int i = 0; i < 3; ++i) Console.Write (this.board [j+6, i+3] + " ");
				Console.Write("|");
				for (int i = 0; i < 3; ++i) Console.Write (this.board [j+6, i+6] + " ");
				Console.Write("|");
				Console.WriteLine ();
			}
			Console.WriteLine("----------------------");
        }
    }
}
