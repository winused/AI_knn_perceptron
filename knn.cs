using System;
using System.Collections.Generic;

namespace DSProje_1
{

    class Program
    {
        private static double[,] CreateMatrix(double width, double height, int n)
        {


            double[,] matrix = new double[n, 2];

            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                matrix[i, 0] = (random.NextDouble()) * width;
                matrix[i, 1] = (random.NextDouble()) * height;
            }

            Console.WriteLine($"\n\n{width}x{height} Öklid uzayındaki rastgele noktalardan oluşan {n}x2 matris :\n");

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 2; j++)
                    Console.Write(String.Format("{0:0.0}", matrix[i, j]) + "\t");
                Console.WriteLine("\n");
            }

            return matrix;
        }

        private static void YazdırCreateDistanceMatrix(double[,] distance_matrix)
        {

            Console.WriteLine($"\n\n{distance_matrix.GetLength(0)}x{distance_matrix.GetLength(1)} uzaklık matrisi :\n");

            for (int i = 0; i < distance_matrix.GetLength(0); i++)
            {
                for (int j = 0; j < distance_matrix.GetLength(1); j++)
                    Console.Write(String.Format("{0:0.0}", distance_matrix[i, j]).Replace(",", ".") + "\t");

                Console.WriteLine("\n");
            }
        }
        private static double[,] CreateDistanceMatrix(double[,] matrix)
        {
            double[,] distance_matrix = new double[matrix.GetLength(0), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    distance_matrix[i, j] = Math.Sqrt(Math.Pow(matrix[i, 0] - matrix[j, 0], 2) + Math.Pow(matrix[i, 1] - matrix[j, 1], 2));
                }
            }

            YazdırCreateDistanceMatrix(distance_matrix);

            return distance_matrix;
        }

        static void Main(string[] args)
        {
            Console.Write("Lütfen Öklid uzayının genişliğini giriniz : ");
            double width = double.Parse(Console.ReadLine());

            Console.Write("Lütfen Öklid uzayının yüksekliğini giriniz : ");
            double height = double.Parse(Console.ReadLine());

            Console.Write("Lütfen kaç adet nokta oluşturulacağını belirtiniz : ");
            int number_of_dots = int.Parse(Console.ReadLine());
            double[,] matrix = CreateMatrix(width, height, number_of_dots);
            double[,] distance_matrix = CreateDistanceMatrix(matrix);
            knn(width, height, number_of_dots, distance_matrix);
            Console.ReadKey();
        }

        static void knn(double width, double height, int n, double[,] distance_matrix)
        {
            Random random = new Random();
            Console.Write("Kaç tur döndürüleceğini belirtiniz : ");
            int tur_count = int.Parse(Console.ReadLine());
            List<int> ilk_noktalar = new List<int>();
            int nokta;
            YazdırCreateDistanceMatrix(distance_matrix);
            for (int s = 0; s < tur_count; s++)
            {
                do
                {
                    nokta = random.Next(0, tur_count);

                } while (ilk_noktalar.Contains(nokta));

                List<int> kullanilmayacaklar = new List<int>();
               
                ilk_noktalar.Add(nokta);
 

                double min_uzaklık;
                int min_uzak_nok;
                double toplam_uzaklık = 0;

                for (int j = 0; j < n; j++)
                {

                    kullanilmayacaklar.Add(nokta);
                    min_uzaklık = width * height;
                    min_uzak_nok = -1;

                    for (int i = 0; i < n; i++)
                    {
                        
                        if (!kullanilmayacaklar.Contains(i))
                        {
                            double arasi_uzaklik;
                            arasi_uzaklik = distance_matrix[nokta, i];
                            if (arasi_uzaklik < min_uzaklık)
                            {
                                min_uzaklık = arasi_uzaklik;
                                min_uzak_nok = i;
                            }

                        }
                    }
                    //Console.WriteLine("toplama katılan min uzaklık" + min_uzaklık+" toplanan nokta="+min_uzak_nok);
                    
                    if (min_uzak_nok != -1)
                    {
                        toplam_uzaklık += min_uzaklık;
                        nokta = min_uzak_nok;
                    }
                }

                Console.WriteLine("\nTur numarası: " + (s + 1));
                for (int i = 0; i < kullanilmayacaklar.Count - 1; i++)
                {
                    Console.Write(kullanilmayacaklar[i] + "-");

                }
                Console.Write(kullanilmayacaklar[kullanilmayacaklar.Count - 1]);


                Console.WriteLine("\nTurun toplam uzunluğu: " + toplam_uzaklık);
                kullanilmayacaklar.Clear();

            }
        }
    }
}
