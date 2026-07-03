using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace A01_Biblioteka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void PopuniListView()
        {
            listView1.Items.Clear();
            foreach(Citalac x in Citalac.UcitajSve())
            {
                string[] vek = new string[5];
                vek[0] = x.Sifra.ToString();
                vek[1] = x.JMBG;
                vek[2] = x.Ime;
                vek[3] = x.Prezime;
                vek[4] = x.Adresa;
                ListViewItem item = new ListViewItem(vek);
                listView1.Items.Add(item);
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopuniListView();
            listView1.FullRowSelect = true;

            comboBox1.DataSource = Citalac.UcitajSve();  //na drugom tabu
        }

        private void textBoxSifra_TextChanged(object sender, EventArgs e)
        {
            if(textBoxSifra.Text ==string.Empty)
            {
                textBoxJMBG.Clear(); textBoxIme.Clear(); 
                textBoxPrezime.Clear(); textBoxAdresa.Clear();
                buttonUpisi.Enabled = true;
                return;
            }

            buttonUpisi.Enabled = false;
            try
            {
                int sif = int.Parse(textBoxSifra.Text);

                List<Citalac> lista = Citalac.UcitajSve();
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Sifra == sif)
                    {
                        listView1.Items[i].Selected = true;
                        textBoxJMBG.Text = lista[i].JMBG;
                        textBoxIme.Text = lista[i].Ime;
                        textBoxPrezime.Text = lista[i].Prezime;
                        textBoxAdresa.Text = lista[i].Adresa;
                    }
                }
            }
            catch (Exception)
            {

            }

            // int ind = lista.FindIndex(x=> x.Sifra==sif);  //ili ovako
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            textBoxJMBG.Text = e.Item.SubItems[1].Text;
        }

        private void buttonUpisi_Click(object sender, EventArgs e)
        {
            Citalac x = new Citalac();
            if(textBoxJMBG.Text!=string.Empty) x.JMBG=textBoxJMBG.Text;
            if (textBoxIme.Text != string.Empty) x.Ime = textBoxIme.Text;
            if (textBoxPrezime.Text != string.Empty) x.Prezime = textBoxPrezime.Text;
            if (textBoxAdresa.Text != string.Empty) x.Adresa = textBoxAdresa.Text;

            if(x.Upisi())
            {
                MessageBox.Show("Uspesan upis");
                PopuniListView();
                int poslednji = listView1.Items.Count-1;
                textBoxSifra.Text= listView1.Items[poslednji].SubItems[0].Text;
            }
            else
            {
                MessageBox.Show("Nije upisano");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int g1 = (int)numericUpDown1.Value;
            int g2 = (int)numericUpDown2.Value;
            int sif = ((Citalac)comboBox1.SelectedItem).Sifra;
            dataGridView1.DataSource = Analiza.Podaci(g1, g2, sif);

            chart1.Series[0].XValueMember = "Godina";
            chart1.Series[0].XValueType = ChartValueType.Int32;
            chart1.Series[0].YValueType = ChartValueType.Int32;
            chart1.Series[0].YValueMembers = "Broj";

            chart1.Series[1].XValueMember = "Godina";
            chart1.Series[1].XValueType = ChartValueType.Int32;
            chart1.Series[1].YValueType = ChartValueType.Int32;
            chart1.Series[1].YValueMembers = "Dug";

            chart1.DataSource = Analiza.Podaci(g1, g2, sif);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch(e.Node.Text)
            {
                case "Čitaoci": 
                    textBox1.Text = "U svakom momentu, u listView kontroli su prikazni podaci o svim korisnicima biblioteke."; 
                    break;
                case "Broj članske karte": 
                    textBox1.Text = "Broj članske karte je šifra čitaoca. Ako postoji čitalas sa šifrom koja je uneta u ovo polje, podatke o njemu se prikazuju u poljima forme. \r\nAko uneta šifra ne postoji u bazi ili ako je polje šifre prazno, resetuje se sadržaj tekstualnih polja na formi.\r\n"; 
                    break;
                case "Upiši": 
                    textBox1.Text = "Kreira se novi korisnik ii u bazu se upisju podaci o korisniku koji su uneti u polja forme. Ako je upis uspešan, prikazuje se šifra na koju je u bazi upisan novi autor i osvežava se sadržaj kontrola."; 
                    break;
                case "Izađi": 
                    textBox1.Text = "Zatvara celu aplikaciju."; 
                    break;
                case "Pregled iznajmljivanja": 
                    textBox1.Text = "U comboBox-u se nalazi lista svih korisnika (prikazani su u formatu šifra-ime prezime).  \r\n"; 
                    break;
                case "Prikaži": 
                    textBox1.Text = "Za izbranog čitaoca, za svaku godinu iz zadatog perioda, prikazati koliko je knjiga korisnik pozajmio iz biblioteke i koliko njiga nije vratio. Podrazumevano se podaci prikazuju za 10 godina unazad."; 
                    break;
                case "O aplikaciji": 
                    textBox1.Text = "Kratko korisničko uputstvo i dokumentacija o aplikaciji."; 
                    break;
            }
        }
    }
}
