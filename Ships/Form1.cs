﻿using System;
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
        
        ShipsMap ship = new ShipsMap();
        byte hitCount =0;
        

        private void Form1_Load(object sender, EventArgs e)
        {
            int index = 1;
            byte X = 0, Y = 0;

            List<Control> controls = this.Controls.OfType<Button>().Cast<Control>().ToList();
            controls.Reverse();

            ship.FillTheMaps();

            
                foreach (var item in controls)
                {
                    Button btn = (Button)item;

                    btn.Tag = string.Format("Button{0}", index);
                    btn.Text = index.ToString();
                    btn.Click += Btn_Click;
                    index++;

                }

            Button fireButton = FindShipButtonByTag(101);
            fireButton.Tag = string.Format("Fire");
            fireButton.Text = string.Format("Fire !!!");
            fireButton.Click -= Btn_Click;

            Button bomb = FindShipButtonByTag(102);
            bomb.Tag = string.Format("Lucky");
            bomb.Text = string.Format("I feel Lucky !!!");
            bomb.Click -= Btn_Click;


            //ustawienie 4-masztowca

            byte[] fourMastShip = ship.GenerateShip(4);

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
                byte[] threeMastship = ship.GenerateShip(3);

                
                foreach (byte part in threeMastship)
                {
                    Button location = FindShipButtonByTag(part);
                    location.Tag = string.Format("Trafiony trzymasztowiec");
                    location.Click -= Btn_Click;
                    location.Click += Btn_ClickHit;
                }
            }

            //ustawienie 2-masztowców

            for (int i = 0; i < 3; i++)
            {
                byte[] twoMastship = ship.GenerateShip(2);


                foreach (byte part in twoMastship)
                {
                    Button location = FindShipButtonByTag(part);
                    location.Tag = string.Format("Trafiony dwumasztowiec");
                    location.Click -= Btn_Click;
                    location.Click += Btn_ClickHit;
                }
            }


            //ustawienie 1-masztowców

            for (int i = 0; i < 4; i++)
            {
                byte[] oneMastship = ship.GenerateShip(1);


                foreach (byte part in oneMastship)
                {
                    Button location = FindShipButtonByTag(part);
                    location.Tag = string.Format("Trafiony zatopiony");
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
            MessageBox.Show(btn.Tag.ToString());
            btn.Enabled = false;
            btn.BackColor = Color.Red;
            hitCount++;
            lblHitCount.Text = string.Format("{0}", hitCount);

        }


        private void btnFire_Click(object sender, EventArgs e)
        {
            byte chosenNumber = (byte)numTarget.Value;

            string pattern = string.Format("{0}", chosenNumber);
            Button btn = Controls.OfType<Button>().FirstOrDefault(control => Equals(control.Text, pattern));
            if (btn.Enabled == false)
            {
                MessageBox.Show("Ale to już było...");
            }
            else if (btn.Enabled == true )
            {
                btn.PerformClick();
            }
           

        }

        private void btnLucky_Click(object sender, EventArgs e)
        {
            Random random = new Random((int)DateTime.Now.Ticks);

            Button btn;

            do
            {
                int randomButton = random.Next(1, 101);

                string pattern = string.Format("{0}", randomButton);
                btn = Controls.OfType<Button>().FirstOrDefault(control => Equals(control.Text, pattern));


            } while (btn.Enabled == false);


                btn.PerformClick();
                //btnLucky.Enabled = false;

        
        }
    }
}
