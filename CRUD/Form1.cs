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
        #region dichiarazione e inizializzazione variabili globali
        string filename;

        public Form1()
        {
            InitializeComponent();
            filename = @"carrello.csv";
        }
        #endregion

        #region pulsanti
        private void button1_Click(object sender, EventArgs e)
        {
            //richiamo la funzione di creazione e aggiornamento del file
            aggiornamentofile(textBox1.Text, textBox2.Text);

            //pulisco le textBox per un nuovo inserimento
            textBox1.Text = "";
            textBox2.Text = "";

            //seleziono in automatico la prima textBox
            textBox1.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //richiamo la funzione di ricerca "falsa"
            falsesearch(textBox3.Text);
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
            if (!File.Exists(filename))
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

        }

        //funzione di ricerca "falsa"
        void falsesearch(string nome)
        {
            using (StreamReader sr = File.OpenText(filename))
            {
                //creo una stringa momentanea
                string s;

                //mentre la stringa momentanea non diventa nulla assumeno i valori delle stringhe nel file...
                while((s = sr.ReadLine()) != null)
                {
                    //... se la stringa è uguale al nome ...
                    if (s == nome)
                    {
                        //stampo una messagebox di ritrovamento...
                        MessageBox.Show("La stringa è stata trovata");
                    }
                    //... altrimenti ...
                    else
                    {
                        //... stampo una messagebox che avvisa che la stringa non esiste...
                        MessageBox.Show("La stringa non è stata trovata");
                    }
                }
            }
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
                        //... e stampo la stringa nella listview
                        listView1.Items.Add(s);
                    }

                }
            }
        }
        #endregion
    }
}
