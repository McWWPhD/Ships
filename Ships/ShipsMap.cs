using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ships
{
    class ShipsMap
    {
        Random random = new Random((int)DateTime.Now.Ticks);

        byte[,] map = new byte[10, 10];
        byte[,] buttonNumbers = new byte[10, 10];

        //wypełnia mapę i listę przycisków numerami
        public void FillTheMaps ()
        {
            byte number = 1;

            for (int i = 0; i< 10; i++)
			{
                for (int j = 0; j < 10; j++)
                {
                    buttonNumbers[i, j] = number;
                    map[i, j] = number;
                    number++;
                }   
            }
		}

        
        public byte[] GenerateShip(byte size)
        {
            byte[] shipLocation = new byte[size];

            byte row, column;
            //1 - pionowo; 2 - poziomo
            byte vOrH = (byte)random.Next(1, 3);


            switch (vOrH)
            {

                case 1:

                    do
                    {
                        row = (byte)random.Next(0, 11 - size);
                        column = (byte)random.Next(0, 10);

                    } while (CheckTheMap(row, column, size, 1));
                    
                        for (int i = 0; i < size; i++)
                        {
                            shipLocation[i] = buttonNumbers[row + i, column];
                            map[row + i, column] = 0;
                            Bubble(row + i, column);
                        }
                    

                    break;

                case 2:

                    do
                    {

                        row = (byte)random.Next(0, 10);
                        column = (byte)random.Next(0, 11 - size);

                    } while (CheckTheMap(row, column, size, 2));


                        for (int i = 0; i < size; i++)
                        {
                            shipLocation[i] = buttonNumbers[row, column + i];
                            map[row, column + i] = 0;
                            Bubble(row, column + i);
                        }
                    

                    break;
                
            }
            
                return shipLocation;
            

        }

        //sprawdza na mapie czy miesce na statek jest dozwolone
        public bool CheckTheMap (byte row, byte column, byte size, byte vOrH )
        {
            //false - wolne; true - zajęte
            bool taken = false;

            switch (vOrH)
            {
                case 1:

                    for (int i = 0; i < size; i++)
                    {

                        if (map[row + i, column] == 0)
                        {
                            taken = true;
                            break;
                        }
                        taken = false;

                    }
                    break;

                case 2:

                    for (int i = 0; i < size; i++)
                    {

                        if (map[row, column + i] == 0)
                        {
                            taken = true;
                            break;
                        }

                        taken = false;

                    }
                    break;

                default:
                    break;
            }

            return taken;
        }

        //zaznacza na mapie niedozwolone miejsca wokół elementu statku
        public void Bubble (int row, int column)
        {
            //góra
            if (row - 1 >= 0 )
            {
                map[row - 1, column] = 0;
            }

            //dół
            if (row + 1 <= 9)
            {
                map[row +1, column] = 0;
            }

            //lewy
            if (column - 1>= 0)
            {
                map[row, column - 1] = 0;
                
            }

            //prawy
            if (column + 1 <= 9)
            {
                map[row, column + 1] = 0;

            }

            //SKOSY
            //lewy-góra
            if (row - 1 >= 0 &&  column - 1 >= 0 )                
                     
            {             
                map[row - 1, column - 1] = 0;                
            }

            //lewy-dół
            if (row + 1 <= 9 && column - 1 >= 0)

            {              
                map[row + 1, column - 1] = 0;
            }

            //prawy-góra
            if (row - 1 >= 0 && column + 1 <= 9)

            {

                map[row - 1, column + 1] = 0;
            }

            //prawy-dół
            if (row + 1 <= 9 && column + 1 <= 9)

            {
                map[row + 1, column + 1] = 0;
            }

            else return;
        }

        
    }
}
