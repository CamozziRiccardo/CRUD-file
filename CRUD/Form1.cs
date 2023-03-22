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

        private void button3_Click(object sender, EventArgs e)
        {
            //richiamo la vera funzione cancellamento
            cancellazione(textBox3.Text);

            //pulisco le textBox per un nuovo inserimento
            textBox3.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //richiamo la funzione di modifica
            mod(textBox3.Text, textBox4.Text, textBox5.Text);

            //pulisco le textBox per un nuovo inserimento
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            visualizza();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            delete();
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
                    sw.WriteLine(nome + " €" + prezzo);
                }
            }

            //nel caso sia già stato creato il file, lo aggiorno
            else
            {
                //apro il file
                using(StreamWriter sw = new StreamWriter(filename, append: true))
                {
                    //copio nel file le stringhe delle textBox
                    sw.WriteLine(nome + " €" + prezzo);
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
                    //... se il nome appartiene alla stringa ...
                    if (s.Contains(nome))
                    {
                        //... stampo una messagebox di ritrovamento...
                        MessageBox.Show("La stringa è stata trovata");

                        //... e ritorno al programma ...
                        return;
                    }
                }

                //... altrimenti ...
                {
                    //... stampo una messagebox che avvisa che la stringa non esiste...
                    MessageBox.Show("La stringa non è stata trovata");
                }
            }
        }

        //funzione di cancellamento
        void cancellazione(string nome)
        {
            using (StreamReader sr = File.OpenText(filename))
            {
                //creo una stringa momentanea
                string s;

                //creo un file temporaneo
                using(StreamWriter sr2 = new StreamWriter("temp.csv"))
                {
                    //mentre la stringa momentanea non diventa nulla assumeno i valori delle stringhe nel file...
                    while ((s = sr.ReadLine()) != null)
                    {
                        //... se nella stringa momentanea non è presente il nome ricercato ...
                        if (s.Contains(nome) == false)
                        {
                            //... la stampo nel file temporaneo
                            sr2.WriteLine(s);
                        }
                    }
                }
            }

            //cancello il file principale
            File.Delete(filename);

            //e rinomino il file momentaneo, rendendolo il nuovo principale
            File.Move("temp.csv", filename);
        }

        //funzione di modifica
        void mod(string ricerca, string nome, string prezzo)
        {
            using (StreamReader sr = File.OpenText(filename))
            {
                //creo una stringa momentanea
                string s;

                //creo un file temporaneo
                using (StreamWriter sr2 = new StreamWriter("temp.csv", append: true))
                {
                    //mentre la stringa momentanea non diventa nulla assumeno i valori delle stringhe nel file...
                    while ((s = sr.ReadLine()) != null)
                    {
                        //... se nella stringa momentanea non è presente il nome ricercatoe ...
                        if (s.Contains(ricerca))
                        {
                            //... stampo la stringa modificata nel file temporaneo ...
                            sr2.WriteLine(nome + " €" + prezzo);
                        }
                        //... mentre se la stringa dovesse corrispondere ...
                        else
                        {
                            //... la stampo nel file temporaneo ...
                            sr2.WriteLine(s);
                        }
                    }
                }
            }

            //cancello il file principale
            File.Delete(filename);

            //e rinomino il file momentaneo, rendendolo il nuovo principale
            File.Move("temp.csv", filename);
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

        //funzione di cancellamento del file
        void delete()
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }
        #endregion
    }
}
