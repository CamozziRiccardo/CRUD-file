using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CRUD
{
    public partial class Form1 : Form
    {
        #region dichiarazione variabili globali
        public struct Prod
        {
            public string prod;
            public string prezzo;
        }

        public static Prod prodotto = new Prod();

        string filename;

        #endregion

        public Form1()
        {
            InitializeComponent();
            filename = @"carrello.csv";
        }

        #region pulsanti
        private void button1_Click(object sender, EventArgs e)
        {
            //richiamo la funzione di creazione e aggiornamento del file
            aggiornamentofile(textBox1.Text, textBox2.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            visualizza();
        }

        #endregion

        #region funzioni di servizio

        //funzione di creazione e aggiornamento file
        void aggiornamentofile(string nome, string prezzo)
        {
            //verifico che non ci siano elementi nello struct per creare il file
            if (String.IsNullOrEmpty(prodotto.prod))
            {
                //creazione del file
                using (StreamWriter sw = new StreamWriter(filename, append: false))
                {
                    //copio nel file le stringhe delle textBox
                    sw.WriteLine(textBox1.Text + " €" + textBox2.Text);
                }
            }
            //nel caso sia già stato creato il file, lo aggiorno
            else
            {
                //apro il file
                using(StreamWriter sw = new StreamWriter(filename, append: true))
                {
                    //copio nel file le stringhe delle textBox
                    sw.WriteLine(textBox1.Text + " €" + textBox2.Text);
                }
            }

            //trasferisco i valori della textBox nello struct per verificare successivamente se il file esiste o meno
            prodotto.prod = textBox1.Text;
            prodotto.prezzo = textBox2.Text;

        }

        //funzione di stampa del file
        void visualizza ()
        {
            //pulisco la listview per stampare il file
            listView1.Items.Clear();

            //verifico che il file esista
            if(!File.Exists(filename))
            {
                MessageBox.Show("Il file non è ancora stato creato");
            }
            else
            {
                using (StreamReader sr = File.OpenText(filename))
                {
                    //creo una stringa momentanea
                    string s;

                    //mentre la stringa momentanea non diventa nulla assumeno i valori delle stringhe nel file...
                    while ((s = sr.ReadLine()) != null)
                    {
                        //... stampo la stringa nella listview
                        listView1.Items.Add(s);
                    }

                }
            }
        }
        #endregion

    }
}
