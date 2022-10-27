using System.Runtime.CompilerServices;

namespace listafilmow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            odczytZPliku();
        }

        private void DodawanieDanych(string tytul, string rezyser, string data, string aktor)
        {
            ListViewItem item = new ListViewItem(new string[] { tytul, rezyser, data, aktor});
            listView1.Items.Add(item);
        }

        private void UsuwanieDanych()
        {
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                listView1.Items.Remove(item);
            }
            listView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                label5.Text = "Nie podano wszystkich danych";
            }
            else
            {
                label5.Text = "";
                DodawanieDanych(textBox1.Text, textBox2.Text, dateTimePicker1.Text, textBox3.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            contextMenuStrip1.Select();
        }

        private void usuñWybraneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsuwanieDanych();
        }


        private string[] WierszDoPliku()
        {
            string[] linie = new string[listView1.Items.Count];
            int i = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                linie[i] = "";
                for (int k = 0; k < item.SubItems.Count; k++)
                {
                    linie[i] += item.SubItems[k].Text + "*";

                }
                i++;
            }

            return linie;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] linie = WierszDoPliku();

            File.WriteAllLines("filmy.txt", linie);
        }

        private void odczytZPliku()
        {
            if(!File.Exists("filmy.txt"))
            {
                return;
            }
            string[] linie = File.ReadAllLines("filmy.txt");
            foreach(string linia in linie)
            {
                string[] temp = linia.Split('*');
                DodawanieDanych(temp[0], temp[1], temp[2], temp[3]);
            }
        }
    }
}