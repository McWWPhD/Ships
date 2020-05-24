using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ships
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        ShipLocation ship = new ShipLocation();

        List<byte> map = new List<byte>();

        private void Form1_Load(object sender, EventArgs e)
        {
            int index = 1;
            byte X = 0, Y = 0;

            List<Control> controls = this.Controls.OfType<Button>().Cast<Control>().ToList();
            controls.Reverse();

            //Button[,] grid = new Button[10, 10];

            foreach (var item in controls)
            {
                Button btn = (Button)item;

                btn.Tag = string.Format("Button{0}", index);
                btn.Text = index.ToString();
                btn.Click += Btn_Click;
                index++;

                ////wypełnienie tablicy przyciskami
                //grid[X,Y] = btn;
                //Y++;

                //if (Y == 10)
                //{
                //    X++;
                //    Y = 0;
                //}
            }

            //ustawienie 4-masztowca
            byte[] fourMastShip = ship.GenerateShip(4);
            map.AddRange(fourMastShip);

            foreach (byte part in fourMastShip)
            {
               Button location = FindShipButtonByTag(part);
               location.Tag = string.Format("Trafiony Czteromasztowiec");
               location.Click -= Btn_Click;
               location.Click += Btn_ClickHit;
            }

            //ustawienie 3-masztowców

            for (int i = 0; i < 2; i++)
            {
                byte[] threeMastShip = ship.GenerateShip(3);

                while (ship.Check(map, threeMastShip))
                {
                    ship.Move(threeMastShip);
                }

                map.AddRange(threeMastShip);


                foreach (byte part in threeMastShip)
                {
                    Button location = FindShipButtonByTag(part);
                    location.Tag = string.Format("Trafiony Trzymasztowiec");
                    location.Click -= Btn_Click;
                    location.Click += Btn_ClickHit;
                }
            }

            ////ustawienie 2-masztowców

            for (int i = 0; i < 3; i++)
            {
                byte[] twoMastShip = ship.GenerateShip(2);

                while (ship.Check(map, twoMastShip))
                {
                    ship.Move(twoMastShip);
                }

                map.AddRange(twoMastShip);


                foreach (byte part in twoMastShip)
                {
                    Button location = FindShipButtonByTag(part);
                    location.Tag = string.Format("Trafiony Dwumasztowiec");
                    location.Click -= Btn_Click;
                    location.Click += Btn_ClickHit;
                }
            }


            //ustawienie 1-masztowców

            for (int i = 0; i < 4; i++)
            {
                byte[] oneMastShip = ship.GenerateShip(1);

                while (ship.Check(map, oneMastShip))
                {
                    ship.Move(oneMastShip);
                }

                map.AddRange(oneMastShip);


                foreach (byte part in oneMastShip)
                {
                    Button location = FindShipButtonByTag(part);
                    location.Tag = string.Format("Trafiony Zatopiony");
                    location.Click -= Btn_Click;
                    location.Click += Btn_ClickHit;
                }
            }




        }

        private Button FindShipButtonByTag (int number)
        {
            string pattern = string.Format("Button{0}", number);
            var item = Controls.Cast<Control>().FirstOrDefault(control => Equals(control.Tag, pattern) );
            return (item == null) ? null : (Button)item;

        }


        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            //MessageBox.Show("Pudło");
            
        }

        private void Btn_ClickHit(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //MessageBox.Show(btn.Tag.ToString());
            btn.Enabled = false;
            btn.BackColor = Color.Red;
        }

    }
}
