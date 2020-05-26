using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Ships
{
    class ShipLocation
    {
        Random random = new Random((int)DateTime.Now.Ticks);


        public byte[] GenerateShip (byte size)
        {
           
                byte[] shipLocation = new byte[size];


                //1 - pionowo; 2 - poziomo
                byte VOrH = (byte)random.Next(1, 3);


                if (VOrH == 1)
                {
                    byte row = (byte)random.Next(0, 10-size);
                    byte column = (byte)random.Next(1, 10);

                byte startingPoint = Convert.ToByte(string.Format("{0}{1}", row, column));


                    for (int i = 0; i < size; i++)
                    {

                        shipLocation[i] = startingPoint;
                        startingPoint += 10; 
                    }
                }
                else if (VOrH == 2)
                {
                    byte row = (byte)random.Next(0, 10);
                    byte column = (byte)random.Next(1, 10-size);

                    byte startingPoint = Convert.ToByte(string.Format("{0}{1}", row, column));


                    for (int i = 0; i < size; i++)
                    {

                        shipLocation[i] = startingPoint;
                        startingPoint += 1;
                    }

                }

                return shipLocation;            
        }

        public byte VOrH (byte[] ship)
        {
            byte orientation;

            if (ship.Length > 1 && ship[0] == ship[1] + 10)
            {
                orientation = 1;

            }            
                else orientation = 2;
            
            return orientation;
        }


        public bool Check(List<byte> map, byte[] ship)
        {
            //1 - pionowo; 2 - poziomo
            byte vorh = VOrH(ship);

            bool tooclose = false;

            if (map.Intersect(ship).Any())
            {
                tooclose = true;
            }

         return tooclose;
          

        }

        public void Move(byte[] ship)
        {
            //1 - pionowo; 2 - poziomo
            byte vorh = VOrH(ship);

            if (vorh == 1)
            {
                if (ship[ship.Length-1] < 100)
                {
                    for (int i = 0; i < ship.Length; i++)
                    {
                        ship[i] += 1;
                    }
                }
                else if (ship[ship.Length-1] == 100)
                {
                    for (int i = 0; i < ship.Length; i++)
                    {
                        ship[i] -= 1;
                    }

                }

            }

            else if (vorh == 2)
            {
                if (ship[ship.Length-1] < 100)
                {
                    for (int i = 0; i < ship.Length; i++)
                    {
                        ship[i] += 10;
                    }
                }
                else if (ship[ship.Length-1] == 100)
                {
                    for (int i = 0; i < ship.Length; i++)
                    {
                        ship[i] -= 10;
                    }

                }

            }
            
        }
    }
}
