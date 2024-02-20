using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace G9_63656_C02
{

    public class AK_63656_WystepujacyZnak
    {
        public int aK_63656_Ilosc { get; set; }
        public string aK_63656_Znak { get; set; }
        public string aK_63656_BinarnyCode { get; set; }
    }


    public class AK_63656_DrzewoHuffmana
    {

        public int aK_63656_BinarnyCode { get; set; }
        public string aK_63656_Znak { get; set; }
        public string aK_63656_Node { get; set; }
        public int aK_63656_Ilosc { get; set; }
    }



    public class AK_63656_HuffmanSourceDictionary
    {
        public string aK_63656_SingleChar { get; set; }
        public string aK_63656_BinaryCode { get; set; }
    }
    internal class Program
    {
        //Tutaj piszę moją metodę Main
        static void Main(string[] args)
        {

            string aK_63656_ciag_znaków = "XADJSOSDAOUAZADXSXODJAOUAOADAOXAAJSAXADOAOADO";
            Console.WriteLine("Wartość ciągu przed kompresja: " + aK_63656_ciag_znaków);


            List<string> aK_63656_resultCode = new List<string>();
            List<AK_63656_WystepujacyZnak> aK_63656_list_znaków = new List<AK_63656_WystepujacyZnak>();
            aK_63656_KompresjaHuffmana(aK_63656_ciag_znaków, ref aK_63656_resultCode, ref aK_63656_list_znaków);

            Console.WriteLine("\nZestawienie w porządku narastającym znaków zawartych w ciągu\r\nwejściowym, wraz z ich licznością, oraz przypisanym słowem kodowym\r\nodczytanym z drzewa Huffmana: ");
            foreach (var aK_63656_znak in aK_63656_list_znaków)
            {
                Console.WriteLine(aK_63656_znak.aK_63656_Znak + " " + aK_63656_znak.aK_63656_Ilosc + " " + aK_63656_znak.aK_63656_BinarnyCode);
            }

            Console.WriteLine("\nKod wynikowy: " + string.Join("", aK_63656_resultCode));


            Console.ReadKey();
        }

        /*
         Kodowanie metodą Hoffmanna
        */

        public static void aK_63656_KompresjaHuffmana(string aK_63656_source,
            ref List<string> aK_63656_resultCode, ref List<AK_63656_WystepujacyZnak> aK_63656_listaZnakow)
        {
            string aK_63656_pozostaly = aK_63656_source;
            string aK_63656_roboczy = aK_63656_pozostaly;
            string aK_63656_kolejnyZnak = "";
            int aK_63656_indexListy = 0;

            List<AK_63656_DrzewoHuffmana> aK_63656_drzewoHuffmana = new List<AK_63656_DrzewoHuffmana>();
            List<AK_63656_WystepujacyZnak> aK_63656_tymczasowaListaZnakow = new List<AK_63656_WystepujacyZnak>();
            List<AK_63656_DrzewoHuffmana> aK_63656_tymczasoweDrzewoHuffmana = new List<AK_63656_DrzewoHuffmana>();

            /*
              Poniższy kod oblicza częstotliwość występowania każdego znaku w ciągu wejściowym i przechowuje wyniki w liście obiektów AK_63656_WystepujacyZnak.
              Pętla while działa dopóki ciąg wejściowy aK_63656_pozostaly nie jest pusty  
             */

            do
            {

                //Wewnątrz pętli pobierany jest pierwszy znak z aK_63656_pozostaly i zapisywany w zmiennej aK_63656_kolejnyZnak
                //Następnie sprawdzenie, czy aK_63656_kolejnyZnak znajduje się na liście tymczasowej znaków aK_63656_tymczasowaListaZnakow
                aK_63656_roboczy = aK_63656_pozostaly;
                aK_63656_kolejnyZnak = aK_63656_roboczy.Substring(0, 1);
                aK_63656_indexListy = aK_63656_tymczasowaListaZnakow.FindIndex(f => f.aK_63656_Znak == aK_63656_kolejnyZnak);

                /*
                 Jeśli nie, tworzony jest nowy obiekt AK_63656_WystepujacyZnak i dodawany do listy z licznikiem równym 1
                 Jeśli aK_63656_kolejnyZnak znajduje się już na liście, skrypt znajduje odpowiadający mu obiekt na liście i zwiększa jego licznik o 1
                 */
                if (aK_63656_indexListy == -1)
                {
                    AK_63656_WystepujacyZnak aK_63656_nowyZnak = new AK_63656_WystepujacyZnak();
                    aK_63656_nowyZnak.aK_63656_Ilosc = 1;
                    aK_63656_nowyZnak.aK_63656_Znak = aK_63656_kolejnyZnak;
                    aK_63656_tymczasowaListaZnakow.Add(aK_63656_nowyZnak);
                }
                else
                {
                    aK_63656_tymczasowaListaZnakow.Where(w => w.aK_63656_Znak == aK_63656_kolejnyZnak).ToList().
                        ForEach(s => s.aK_63656_Ilosc = s.aK_63656_Ilosc + 1);

                }

                //Na koniec pierwszy znak usuwany jest z aK_63656_pozostaly, aby pętla mogła kontynuować z następnym znakiem
                aK_63656_pozostaly = aK_63656_roboczy.Remove(0, 1);

            }
            while (aK_63656_pozostaly.Length != 0);

            //Sortowanie listę obiektów typu AK_63656_WystepujacyZnak, która znajduje się w zmiennej aK_63656_tymczasowaListaZnakow, rosnąco według wartości właściwości aK_63656_Ilosc
            aK_63656_listaZnakow = aK_63656_tymczasowaListaZnakow.OrderBy(o => o.aK_63656_Ilosc).ToList();
            List<AK_63656_WystepujacyZnak> aK_63656_posortowanaListaZnakow = new List<AK_63656_WystepujacyZnak>(aK_63656_listaZnakow);

            int aK_63656_nrKorzenia = 0;
            int aK_63656_nowyKorzenWartosc = 0;
            string aK_63656_nowyKorzen = "node";


            //Pętla będzie kontynuowana, dopóki na liście aK_63656_posortowanaListaZnakow nie pozostanie tylko jeden element
            do
            {
                //Sprawdzenie, czy lista znaków aK_63656_posortowanaListaZnakow ma więcej niż jeden element.
                if (aK_63656_posortowanaListaZnakow.Count > 1)
                {
                    // Jeśli tak, to kod sprawdza, czy aK_63656_drzewoHuffmana ma jakieś węzły
                    if (aK_63656_drzewoHuffmana.Count > 0)
                    {
                        //Jeśli tak, to kod oblicza sumę częstotliwości dwóch najmniejszych elementów na liście aK_63656_posortowanaListaZnakow oraz częstotliwości węzła korzenia drzewa Huffmana
                        if (aK_63656_drzewoHuffmana[0].aK_63656_Ilosc + aK_63656_posortowanaListaZnakow[0].aK_63656_Ilosc >
                            aK_63656_posortowanaListaZnakow[0].aK_63656_Ilosc + aK_63656_posortowanaListaZnakow[1].aK_63656_Ilosc)

                            //Jeśli suma jest mniejsza niż suma częstotliwości dwóch najmniejszych elementów, węzeł korzenia drzewa Huffmana otrzymuje sumę częstotliwości dwóch najmniejszych elementów
                            aK_63656_nowyKorzenWartosc = aK_63656_drzewoHuffmana[0].aK_63656_Ilosc + aK_63656_posortowanaListaZnakow[0].aK_63656_Ilosc;

                        else
                            //W przeciwnym razie węzeł korzenia otrzymuje sumę częstotliwości dwóch najmniejszych elementów na liście
                            aK_63656_nowyKorzenWartosc = aK_63656_posortowanaListaZnakow[0].aK_63656_Ilosc + aK_63656_posortowanaListaZnakow[1].aK_63656_Ilosc;
                    }
                    else
                        //Jeśli drzewo Huffmana jest puste, węzeł korzenia otrzymuje sumę częstotliwości dwóch najmniejszych elementów na liście
                        if (aK_63656_drzewoHuffmana.Count == 0)
                        aK_63656_nowyKorzenWartosc = aK_63656_posortowanaListaZnakow[0].aK_63656_Ilosc +
                             aK_63656_posortowanaListaZnakow[1].aK_63656_Ilosc;

                    //Sprawdzenie, czy na liście aK_63656_posortowanaListaZnakow jest więcej niż dwa elementy
                    if (aK_63656_posortowanaListaZnakow.Count > 2)
                    {
                        //Jeśli suma częstotliwości węzła korzenia i trzeciego najmniejszego elementu na liście jest większa lub równa częstotliwości trzeciego najmniejszego elementu, numer węzła korzenia jest inkrementowany
                        if (aK_63656_nowyKorzenWartosc >= aK_63656_posortowanaListaZnakow[2].aK_63656_Ilosc &&
                            aK_63656_posortowanaListaZnakow.Count >= 3)

                            aK_63656_nrKorzenia++;
                    }
                    else
                        aK_63656_nrKorzenia++;

                    //AK_63656_WystepujacyZnak z częstotliwością węzła korzenia i przypisanym nowym znakiem 
                    AK_63656_WystepujacyZnak aK_63656_nowyZnak = new AK_63656_WystepujacyZnak
                    {
                        aK_63656_Ilosc = aK_63656_nowyKorzenWartosc,
                        aK_63656_Znak = aK_63656_nowyKorzen + aK_63656_nrKorzenia
                    };

                    //Ten obiekt jest następnie dodawany do listy aK_63656_posortowanaListaZnakow
                    aK_63656_posortowanaListaZnakow.Add(aK_63656_nowyZnak);



                    //Pętla for iteruje dwukrotnie, gdy wartość zmiennej aK_63656_i wynosi 0 i 1

                    for (int aK_63656_i = 0; aK_63656_i <= 1; aK_63656_i++)
                    {
                        // W każdej iteracji tworzony jest nowy obiekt klasy AK_63656_DrzewoHuffmana o nazwie aK_63656_drzewoHuffmanaItem
                        AK_63656_DrzewoHuffmana aK_63656_drzewoHuffmanaItem = new AK_63656_DrzewoHuffmana();

                        //Jeśli aK_63656_posortowanaListaZnakow zawiera więcej niż jeden element, aK_63656_drzewoHuffmanaItem.aK_63656_BinarnyCode przyjmuje wartość 0 lub 1, w zależności od wartości zmiennej aK_63656_i
                        if (aK_63656_posortowanaListaZnakow.Count > 1)
                            aK_63656_drzewoHuffmanaItem.aK_63656_BinarnyCode = aK_63656_i;
                        //W przeciwnym razie aK_63656_drzewoHuffmanaItem.aK_63656_BinarnyCode przyjmuje wartość 2
                        else
                            aK_63656_drzewoHuffmanaItem.aK_63656_BinarnyCode = 2;


                        //Wartościom właściwości obiektu aK_63656_drzewoHuffmanaItem przypisywane są wartości z listy aK_63656_posortowanaListaZnakow i zmiennej aK_63656_nowyKorzen
                        aK_63656_drzewoHuffmanaItem.aK_63656_Znak = aK_63656_posortowanaListaZnakow[aK_63656_i].aK_63656_Znak;
                        aK_63656_drzewoHuffmanaItem.aK_63656_Node = aK_63656_nowyKorzen + aK_63656_nrKorzenia.ToString();
                        aK_63656_drzewoHuffmanaItem.aK_63656_Ilosc = aK_63656_posortowanaListaZnakow[aK_63656_i].aK_63656_Ilosc;
                        //Na końcu każdej iteracji obiekt aK_63656_drzewoHuffmanaItem dodawany jest do listy aK_63656_drzewoHuffmana
                        aK_63656_drzewoHuffmana.Add(aK_63656_drzewoHuffmanaItem);
                    }

                    //Dwa najmniejsze elementy są usuwane z listy, a lista i drzewo są sortowane i aktualizowane
                    aK_63656_posortowanaListaZnakow.RemoveRange(0, 2);
                    aK_63656_tymczasowaListaZnakow = aK_63656_posortowanaListaZnakow.OrderBy(o => o.aK_63656_Ilosc).ToList();
                    aK_63656_tymczasoweDrzewoHuffmana = aK_63656_drzewoHuffmana.OrderByDescending(o => o.aK_63656_Ilosc).ToList();
                    aK_63656_drzewoHuffmana = aK_63656_tymczasoweDrzewoHuffmana;
                    aK_63656_posortowanaListaZnakow = aK_63656_tymczasowaListaZnakow;
                }
                else
                {
                    AK_63656_DrzewoHuffmana aK_63656_drzewoHuffmanaItrm = new AK_63656_DrzewoHuffmana
                    {
                        aK_63656_BinarnyCode = 2,
                        // Znak nowego korzenia
                        aK_63656_Znak = aK_63656_nowyKorzen + (aK_63656_nrKorzenia + 1).ToString(),
                        // nazwa węzła "TOP"
                        aK_63656_Node = "TOP"
                    };
                    //Dodanie elementu do drzewa Huffmana
                    aK_63656_drzewoHuffmana.Add(aK_63656_drzewoHuffmanaItrm);
                    //Wyczyszczenie listy posortowanych znaków 
                    aK_63656_posortowanaListaZnakow.Clear();
                }

            }
            while (aK_63656_posortowanaListaZnakow.Count != 0);

            /*Skrypt sortuje listę obiektów aK_63656_drzewoHuffmana po wartościach atrybutu aK_63656_Ilosc w porządku rosnącym,
             a następnie przypisuje tę posortowaną listę do zmiennej aK_63656_drzewoHuffmana
            */
            aK_63656_tymczasoweDrzewoHuffmana = aK_63656_drzewoHuffmana.OrderBy(o => o.aK_63656_Ilosc).ToList();
            aK_63656_drzewoHuffmana = aK_63656_tymczasoweDrzewoHuffmana;


            string aK_63656_tempBinaryCode = "";
            string aK_63656_actualNode = "";

            //Pętla for porównuje każdy element w liście aK_63656_drzewoHuffmana z jego następnikiem
            for (int aK_63656_i = 0; aK_63656_i < aK_63656_drzewoHuffmana.Count - 1; aK_63656_i++)
            {
                //Jeśli dwa sąsiednie elementy mają taką samą ilość wystąpień aK_63656_Ilosc i następny element ma długość (Length) większą niż jeden, to zamienia je miejscami
                if (aK_63656_drzewoHuffmana[aK_63656_i].aK_63656_Ilosc == aK_63656_drzewoHuffmana[aK_63656_i + 1].aK_63656_Ilosc &&
                    aK_63656_drzewoHuffmana[aK_63656_i + 1].aK_63656_Znak.Length > 1)
                {
                    AK_63656_DrzewoHuffmana aK_63656_tymczasowy = new AK_63656_DrzewoHuffmana();
                    aK_63656_tymczasowy = aK_63656_drzewoHuffmana[aK_63656_i];
                    aK_63656_drzewoHuffmana[aK_63656_i] = aK_63656_drzewoHuffmana[aK_63656_i + 1];
                    aK_63656_drzewoHuffmana[aK_63656_i + 1] = aK_63656_tymczasowy;
                    //Ustawia wartość aK_63656_BinarnyCode na 0 dla pierwszego z wymienionych elementów i na 1 dla drugiego.
                    aK_63656_drzewoHuffmana[aK_63656_i].aK_63656_BinarnyCode = 0;
                    aK_63656_drzewoHuffmana[aK_63656_i + 1].aK_63656_BinarnyCode = 1;
                }
            }

            //Ten kod odpowiada za przypisanie kodów Huffmana do każdego znaku w liście aK_63656_listaZnakow
            for (int aK_63656_i = 0; aK_63656_i < aK_63656_listaZnakow.Count; aK_63656_i++)
            {
                //Przypisanie wartości kolejnego znaku z listy aK_63656_listaZnakow
                aK_63656_kolejnyZnak = aK_63656_listaZnakow[aK_63656_i].aK_63656_Znak;
                //Znalezienie jego indeksu na liście drzewa Huffmana aK_63656_drzewoHuffmana
                aK_63656_indexListy = aK_63656_drzewoHuffmana.FindIndex(f => f.aK_63656_Znak == aK_63656_kolejnyZnak);
                aK_63656_actualNode = aK_63656_drzewoHuffmana[aK_63656_indexListy].aK_63656_Node;
                //Następnie przypisywana jest wartość aktualnego węzła i kodu binarnego do zmiennej tymczasowej aK_63656_tempBinaryCode
                aK_63656_tempBinaryCode = aK_63656_drzewoHuffmana[aK_63656_indexListy].aK_63656_BinarnyCode.ToString();

                //Pętla do-while wykonuje się, dopóki indeks węzła nie będzie równy -1
                do
                {
                    aK_63656_indexListy = aK_63656_drzewoHuffmana.FindIndex(f => f.aK_63656_Znak == aK_63656_actualNode);
                    if (aK_63656_indexListy != -1)
                    {
                        //Przypisanie wartości indeksu na liście drzewa Huffmana dla węzła aktualnego węzła aK_63656_actualNode 
                        aK_63656_actualNode = aK_63656_drzewoHuffmana[aK_63656_indexListy].aK_63656_Node;
                        //Następnie przypisanie wartości aktualnego węzła i kodu binarnego do zmiennej tymczasowej aK_63656_tempBinaryCode
                        aK_63656_tempBinaryCode = aK_63656_tempBinaryCode +
                            aK_63656_drzewoHuffmana[aK_63656_indexListy].aK_63656_BinarnyCode.ToString();

                        //Jeśli długość kodu binarnego jest większa niż 1 i jego pierwszy znak to 0, to usuwany jest pierwszy znak z kodu binarnego
                        if (aK_63656_tempBinaryCode.Length > 1 && aK_63656_tempBinaryCode.Substring(0, 1) == "0")
                            aK_63656_tempBinaryCode = aK_63656_tempBinaryCode.Remove(0, 1);
                    }
                }
                while (aK_63656_indexListy != -1);
                //Przypisywanie kodu binarny do znaku na liście aK_63656_listaZnakow.
                aK_63656_listaZnakow[aK_63656_i].aK_63656_BinarnyCode = aK_63656_tempBinaryCode;
            }

            //Pętla for iteruje przez każdy element ciągu znaków aK_63656_source
            for (int aK_63656_i = 0; aK_63656_i < aK_63656_source.Length; aK_63656_i++)
            {
                //W każdej iteracji pętli, kolejny znak jest pobierany  i przypisywany do zmiennej aK_63656_kolejnyZnak
                aK_63656_kolejnyZnak = aK_63656_source.Substring(aK_63656_i, 1);

                //Pętla for iteruje przez każdy element listy aK_63656_listaZnakow
                for (int aK_63656_j = 0; aK_63656_j < aK_63656_listaZnakow.Count; aK_63656_j++)
                {
                    //Program sprawdza, czy wartość  aK_63656_Znak  z listy jest równa kolejnemu znakowi aK_63656_kolejnyZnak
                    if (aK_63656_listaZnakow[aK_63656_j].aK_63656_Znak == aK_63656_kolejnyZnak)
                    {
                        //Program dodaje wartość  aK_63656_BinarnyCode  z listy, do kolekcji aK_63656_resultCode wraz z przecinkiem
                        aK_63656_resultCode.Add(aK_63656_listaZnakow[aK_63656_j].aK_63656_BinarnyCode + ",");
                        //Pętla zostaje przerwana poprzez przypisanie wartości aK_63656_listaZnakow.Count do zmiennej aK_63656_j
                        aK_63656_j = aK_63656_listaZnakow.Count;
                    }
                }
            }


        }





        //Dekodowanie
        public static void aK_63656_DekompresjaHuffmana(List<AK_63656_HuffmanSourceDictionary> aK_63656_sourceDictionary,
            string aK_63656_source, ref List<string> aK_63656_resultCode, ref bool aK_63656_dictionaryComplete)
        {
            string aK_63656_kolejnyZnak = "";
            int aK_63656_zawiera = 0;

            try
            {
                //pętla do-while warunek zewnętrzny sprawdza długość ciągu binarnego aK_63656_source
                do
                {
                    // pętla do-while usuwa początkowe zera lub jedynki ze źródłowego ciągu, aż do momentu, gdy pierwszy znak to 1 lub 0
                    do
                    {
                        //Jeśli źródłowy ciąg jest jeszcze dłuższy i pierwszy znak to 0 lub 1, zewnętrzna pętla do-while będzie kontynuować działanie
                        if (aK_63656_source.Length > 0 &&
                            aK_63656_source.Substring(0, 1) != "0" &&
                            aK_63656_source.Substring(0, 1) != "1")
                        {
                            aK_63656_source = aK_63656_source.Remove(0, 1);
                        }
                    }
                    while (aK_63656_source.Length > 0 &&
                           aK_63656_source.Substring(0, 1) != "1" &&
                           aK_63656_source.Substring(0, 1) != "0");

                    if (aK_63656_source.Length > 0)
                    {
                        //Pętla do-while dodaje kolejny znak do aK_63656_kolejnyZnak i usuwa go z źródłowego ciągu, dopóki kolejny znak to 0 lub 1
                        do
                        {
                            aK_63656_kolejnyZnak = aK_63656_kolejnyZnak + aK_63656_source.Substring(0, 1);
                            aK_63656_source = aK_63656_source.Remove(0, 1);
                        }
                        while (aK_63656_source.Substring(0, 1) == "0" ||
                               aK_63656_source.Substring(0, 1) == "1");

                        //aK_63656_zawiera otrzymuje indeks w aK_63656_sourceDictionary, który odpowiada aK_63656_kolejnyZnak
                        aK_63656_zawiera = aK_63656_sourceDictionary.FindIndex(f => f.aK_63656_BinaryCode == aK_63656_kolejnyZnak);
                        //Znak odpowiadający aK_63656_zawiera zostaje dodany do listy aK_63656_resultCode
                        aK_63656_resultCode.Add(aK_63656_sourceDictionary[aK_63656_zawiera].aK_63656_SingleChar);
                        //aK_63656_kolejnyZnak zostaje zresetowany do pustego ciągu znaków
                        aK_63656_kolejnyZnak = "";
                    }
                    else
                    {
                        //Jeśli źródłowy ciąg jest już pusty, aK_63656_dictionaryComplete zostaje ustawiony na true, a metoda zostaje zwrócona
                        aK_63656_dictionaryComplete = true;
                        return;
                    }
                }
                while (aK_63656_source.Length > 0);
            }
            catch (Exception aK_63656_ex)
            {
                //W przypadku wystąpienia wyjątku, aK_63656_dictionaryComplete zostaje ustawiony na false
                aK_63656_dictionaryComplete = false;
            }



        }



    }

}
