// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;

namespace parallelpruts
{
    class LPT_X
    {
        //inpout.dll

        [DllImport("inpoutx64.dll")]
        private static extern void Out32(int PortAddress, int Data);

        [DllImport("inpoutx64.dll")]
        private static extern UInt32 IsInpOutDriverOpen();

      
        //inpoutx64.dll

        [DllImport("inpoutx64.dll", EntryPoint = "IsInpOutDriverOpen")]
        private static extern UInt32 IsInpOutDriverOpen_x64();

        [DllImport("inpoutx64.dll", EntryPoint = "Out32")]
        private static extern void Out32_x64(int PortAddress, short Data);

        [DllImport("inpoutx64.dll", EntryPoint = "Inp32")]
        private static extern char Inp32_x64(int PortAddress);

        
        private int DataAddress;
        private int StatusAddress;
        private int ControlAddress;

        public LPT_X(int PortAddress)
        {
            DataAddress = PortAddress;
            StatusAddress = (int)(DataAddress + 1);
            ControlAddress = (int)(DataAddress + 2);

            //now the code tries to determine if it should use inpout32.dll or inpoutx64.dll
            uint nResult = 0;

           
            try
            {
                Console.WriteLine("trying 64 bits");
                nResult = IsInpOutDriverOpen_x64();
                if (nResult != 0)
                {
                    Console.WriteLine("using 64 bits");
                   
                    return;
                }
            }
            catch (BadImageFormatException)
            {
                Console.WriteLine("64 bits failed");
            }
            catch (DllNotFoundException)
            {
                throw new ArgumentException("Unable to find InpOutx64.dll");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            throw new ArgumentException("Unable to open either inpout32 and inpoutx64");
        }

        public void WriteData(short Data)
        {
         
                Out32_x64(DataAddress, Data);
          
        }

        public void WriteControl(short Data)
        {
           
                Out32_x64(ControlAddress, Data);
            
        }

        public byte ReadData()
        {
         
                return (byte)Inp32_x64(DataAddress);
       
  
        }

        public byte ReadControl()
        {
       
                return (byte)Inp32_x64(ControlAddress);
        
        }

        public byte ReadStatus()
        {
          
                return (byte)Inp32_x64(StatusAddress);
       
        }
    }
}
