using System;

namespace Perceptron
{
    public class Neuron
    {

        private static readonly Random random = new Random();

        private static double RandomNumberBetween(double minimum, double maximum)
        {

            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;

        }


        // -1 ve 1 arası random ağırlıklar
        public double[] weights = { RandomNumberBetween(-1,1),RandomNumberBetween(-1,1) };

        // Training data
        public double[,] inputs = { { 6, 5 }, { 2, 4 }, { -3, -5 }, { -1, -1 }, { 1, 1 }, { -2, 7 }, { -4, -2 }, { -6, 3 } };
        
        //Target
        public int[] outputs = new int[] { 1, 1, -1, -1, 1, 1, -1, -1 }; 

    }

    public class Eğitim

    {

        static void Main()
        {


            Neuron neuron = new Neuron();

            double delta = 0.05;

            int epok = 1;

            int yanlis ;
            int Count = neuron.inputs.GetUpperBound(0) + 1;


            Console.Write("Lütfen kaç epok dönüleceğini giriniz : ");
            int epok_sayisi = int.Parse(Console.ReadLine());

             for (int s = 0; s < epok_sayisi; s++)
             {
 
                yanlis = 0;
                for (int j = 0; j < Count; j++)
                {
                    // Toplama işlemi yapılır ve pozitif veya negatifliğine göre 1 veya -1 döndürülür.
                    int output = Output(neuron.weights, neuron.inputs[j, 0], neuron.inputs[j, 1]);
                    // Hata yok ise t-o = 0 çıkar. neuron.outputs[j] bize t'yi verir.
                    double hata_payi = neuron.outputs[j] - output;

                    if (hata_payi != 0)
                    {
                        yanlis++;
                        // x1 ve x2 ağırlıkları değişir
                        //Projede x1/10, x2/10 değeri ile ağırlıkların değişmesi istendiği için 10a bölünür.
                        neuron.weights[0] += delta * hata_payi * neuron.inputs[j, 0] / 10;
                        neuron.weights[1] += delta * hata_payi * neuron.inputs[j, 1] / 10;
                     
                    }
         
                }
                int dogru = Count - yanlis;
                double dogruluk = (double)dogru / (double)Count;
                Console.WriteLine("Epok {0}\tDoğru{1}\tAccuracy %{2}", epok, dogru, dogruluk * 100);
                epok++;

             }

            //////Eğitimimin sonuna gelindi. w1 ve w2 değerlerimiz eğitildi.


            Console.Write("Konsola girilecek test veri çifti sayısı giriniz:");
            int veri_say = int.Parse(Console.ReadLine());

            int dogru_cikti = 0;
            int[,] veriler = new int[veri_say, 2];
            int[] test_target = new int[veri_say];
            int[] ciktilar = new int[veri_say];
            for (int m = 0; m < veri_say; m++)
            {
                //Kolsoldan her veri çifti için x1 ve x2 değerleri alınır ve 2 boyutlu arrayde saklanır.
                Console.WriteLine("{0}. veri için", m + 1);
                Console.Write("X1 = ");
                veriler[m,0] = int.Parse(Console.ReadLine());
                Console.Write("X2 = ");
                veriler[m, 1] = int.Parse(Console.ReadLine());
                test_target[m] = ((veriler[m,0]+ veriler[m, 1]) >= 0) ? 1 : -1;
                ciktilar[m] = Output(neuron.weights, veriler[m, 0], veriler[m, 1]);

                if(test_target[m] == ciktilar[m])
                {
                    dogru_cikti++;
                }
            }


            double test_dogruluk = (double)dogru_cikti / (double)veri_say;
            Console.WriteLine("Doğru{0}\tAccuracy %{1}", dogru_cikti, test_dogruluk * 100);


            Console.ReadKey();
        }

        private static int Output(double[] weights, double x1, double x2)
        {
            //  Output = x1 * w1 + x2 * w2 
            double islem = x1 * weights[0] + x2 * weights[1];
            // İşlemin sonucu 0dan büyükse output = 1 küçükse output = -1 döndürür.
            return (islem >= 0) ? 1 : -1;
        }


    }
}