using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toeplitz
{
    class ToeplitzMatrix
    {
        private int[] coefficients;
        public int Size
        {
            get;
            private set;
        }
        public ToeplitzMatrix(int[] coefficients)
        {
            Size = (coefficients.Length + 1) / 2;
            this.coefficients = coefficients;
        }

        public int this[int index1, int index2]
        {
            get
            {
                return coefficients[(Size - 1 - index2) + index1];
            }
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    s += this[i, j] + " ";
                }
                if (i != Size - 1)
                    s += "\n";
            }
            return s;
        }

        public int[] ClassicMultiply(int[] v)
        {
            int[] w = new int[Size];
            int sum;
            for (int i = 0; i < Size; i++) // obliczamy i-ty element wektora, i-ty wiersz macierzy
            {
                sum = 0;
                for (int j = 0; j < Size; j++)
                {
                    sum += this[i, j] * v[j];
                }
                w[i] = sum;
            }

            return w;
        }
        public int[] FastMultiply(int[] v)
        {
            int[] w = new int[Size];
            return w;
        }
    }
}
