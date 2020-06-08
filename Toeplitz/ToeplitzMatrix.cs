using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace Toeplitz
{
    class ToeplitzMatrix
    {
        private double[] coefficients;
        public int Size
        {
            get;
            private set;
        }
        public ToeplitzMatrix(double[] coefficients)
        {
            Size = (coefficients.Length + 1) / 2;
            this.coefficients = coefficients;
        }

        public double this[int index1, int index2]
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

        public double[] ClassicMultiply(double[] v)
        {
            double[] w = new double[Size];
            double sum;
            for (int i = 0; i < Size; i++) // obliczamy i-ty element wektora, i-ty wiersz macierzy
            {
                sum = 0;
                for (int j = 0; j < Size; j++)
                {
                    sum += this[i, j] * v[j];
                }
                w[i] = sum;
            }
            
            return w.Select(d => Math.Round(d, 4)).ToArray();
        }
        public double[] FastMultiply(double[] v)
        {
            //resize to power of 2 & double
            coefficients = ResizeToDoublePowerOf2(coefficients);
            int n = coefficients.Length;

            // make v the same length
            v = ResizeToN(v, n);

            Complex[] y_a = FFT(coefficients);
            Complex[] y_v = FFT(v);
            Complex[] y_w = PointwiseMultiply(y_a, y_v);

            Complex[] w = InverseFFT(y_w);

            double[] result = new double[Size];
            for (long i = 0; i < result.Length; i++)
                result[i] = w[i + Size - 1].Real / n;

            return result.Select(d => Math.Round(d, 4)).ToArray();
        }

        private double[] ResizeToDoublePowerOf2(double[] origin)
        {
            int power = 1;
            while (power < origin.Length)
                power *= 2;
            power *= 2;

            double[] resized = new double[power];
            for (long i = 0; i < origin.Length; i++)
                resized[i] = origin[i];
            return resized;
        }
        private double[] ResizeToN(double[] origin, int n)
        {
            double[] resized = new double[n];
            for (long i = 0; i < origin.Length; i++)
                resized[i] = origin[i];
            return resized;
        }

        private Complex[] FFT(double[] a)
        {
            long n = a.Length;
            Complex[] y = new Complex[n];
            if (n == 1)
            {
                y[0] = new Complex(a[0], 0);
                return y;
            }
            Complex w_n = new Complex(Math.Cos(2 * Math.PI / n), Math.Sin(2 * Math.PI / n));
            Complex w = new Complex(1, 0);

            double[] a_0 = GetHalfOfCoefficients(a, even: true);
            double[] a_1 = GetHalfOfCoefficients(a, even: false);

            Complex[] y_0 = FFT(a_0);
            Complex[] y_1 = FFT(a_1);

            for(long k = 0; k < n/2; k++)
            {
                y[k] = y_0[k] + w * y_1[k];
                y[k + n / 2] = y_0[k] - w * y_1[k];
                w *= w_n;
            }

            return y;
        }

        private Complex[] PointwiseMultiply(Complex[] v1, Complex[] v2)
        {
            if (v1.Length != v2.Length)
                throw new Exception();
            Complex[] w = new Complex[v1.Length];
            for (long i = 0; i < w.Length; i++)
                w[i] = v1[i] * v2[i];
            return w;
        }

        private Complex[] InverseFFT(Complex[] y)
        {
            int n = y.Length;
            Complex[] a = new Complex[n];
            if (n == 1)
            {
                a[0] = y[0];
                return a;
            }
            Complex w_n = new Complex(Math.Cos(-2 * Math.PI / n), Math.Sin(-2 * Math.PI / n));
            Complex w = new Complex(1, 0);

            Complex[] y_0 = GetHalfOfCoefficients(y, even: true);
            Complex[] y_1 = GetHalfOfCoefficients(y, even: false);

            Complex[] a_0 = InverseFFT(y_0);
            Complex[] a_1 = InverseFFT(y_1);

            for (int k = 0; k < n / 2; k++)
            {
                a[k] = ( a_0[k] + w * a_1[k] );
                a[k + n / 2] = ( a_0[k] - w * a_1[k] );
                w *= w_n;
            }

            return a;
        }

        private T[] GetHalfOfCoefficients<T>(T[] coefficients, bool even)
        {
            long start = even ? 0 : 1;
            T[] halfOfCoefficients = new T[coefficients.Length / 2];
            for(long i = 0; i < halfOfCoefficients.Length; i++)
            {
                halfOfCoefficients[i] = coefficients[start];
                start += 2;
            }
            return halfOfCoefficients;
        }
    }
}
