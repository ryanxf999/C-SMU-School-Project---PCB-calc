/*
 * TODO LIST
 *
 *create more menu items...
 *          -for the sorting algorithms (FCFS/RR)
 *          -add
 *              ''allow for adding a pcb to a given position in the queue???
 *          -delete PCBs
 *              ''deleting a PCB using the PID.
 *          - display the contents of different queues
 *              ''pre and post sort
 * 
 * READ data from file 
 * SORT data from the file.
 * ADD processes inside file.
 * 
 *  ===ALGORITHMS===
 *  Calculate avg wait times for EACH algorithm
 * 
 * 
 * 
 * 
 *
 *
  */



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSE_7343_Project
{
    //this class sets up the menu options for the program.
    class Main_Menu
    {
        // Input form
        private int[] cpu_brustTime = { 6, 2, 8, 5, 10 };
        private int[] arrivalTime = { 8, 8, 0, 2, 6 };
        private int[] priority;
        private int quantumTime = 3;
        private int num_process = 5;

        //FCFS
        public int[] waitingTime_FCFS= { 15, 21, 0, 6, 7 };
        public int[] turnAroundTime_FCFS = { 29, 31, 8, 12, 23 };
        private int contextSwitch_FCFS = 4;
        private float Avg_waitingTime_FCFS = 9.80f;



        //RR
        public int[] waitingTime_RR = {13,11,3,9,15 };
        public int[] turnAroundTime_RR = { 27,21,11,16,32};
        private int contextSwitch_RR = 9;
        private int waiting_Time;
        private float Avg_waitingTime_RR;
        private float Avg_waiting_Time_RR = 10.20f;


        int[] process_ID;
        int[] arrival_Time = {8,8,0,2,6};
        int[] burst_Time;
        int[] process_Priority;
        int[] waiting_Time_RR = { 13, 11, 3, 9, 15 };
        int[] turnAroundTime;
     

        public void ProcessesScheduling()
        {


            quantumTime = 3;
            waitingTime_FCFS = new int[10];
            turnAroundTime_FCFS = new int[10];

            waitingTime_RR = new int[10];
            turnAroundTime_RR = new int[10];

            cpu_brustTime = new int[10];
            arrivalTime = new int[10];
            priority = new int[10];

            // Initial value in array.
            //clearArrayData(); // Clear waiting time.
            for (int i = 0; i < 10; i++)
            {
                cpu_brustTime[i] = 1;
                arrivalTime[i] = 0;
                priority[i] = 1;
            }
            num_process = 1;
            Avg_waitingTime_FCFS = 0;

        }
        public float getAVG_waitingTimeFCFS()
        {
            return Avg_waitingTime_FCFS;
        }
        public int getContextSwitch_FCFS()
        {
            return contextSwitch_FCFS;
        }
        public float getAVG_waitingTimeRR()
        {
            return Avg_waitingTime_RR;
        }

        public int getContextSwitch_RR()
        {
            return contextSwitch_RR;
        }
        public void _Menu()
        {
            Main_Menu MenuSelection = new Main_Menu(); 
            


            Console.WriteLine("\n\n============================================\n============================================\n\n" +
                "               PCB scheduler\n\n           created by Ryan Fontenot      \n\n" +
                "============================================\n============================================");
            Console.WriteLine();

            int selection = 0;

            string userInput;

            //menu loop for user selection
            do
            {
                
                Console.WriteLine("( 1 )  Open PCB File to view/edit.");
                Console.WriteLine("( 2 )  Add or Delete PCB.");
                Console.WriteLine("( 3 )  Load PCBs from file.");
                Console.WriteLine("( 4 )  Calculate Average WAit Times.");
                Console.WriteLine("( 0 )  EXIT program.");
                Console.WriteLine();
                Console.Write("\nPlease select an option: ");
                userInput = Console.ReadLine();
                Console.WriteLine();
                

                char firstChar = userInput[0];

                bool isNumber = char.IsDigit(firstChar);


                if(!isNumber)
                {
                    Console.WriteLine("Not an valid input");
                }else
                    selection = Convert.ToInt32(userInput);



                if (selection == 1)
                    MenuSelection.Option1();
                else if (selection == 2)
                    MenuSelection.Option2();
                else if (selection == 3)
                    MenuSelection.Option3();
                else if (selection == 4)
                    MenuSelection.Option4();
                else
                    Console.Write("\nPlease selection another option: ");
                


            } while (selection != 0);

            Console.WriteLine("closing....");
            Console.ReadKey();

            Environment.Exit(0);


            Console.ReadKey();

        }
        //opens PCB file
        private void Option1()
        {
            //this object will open the file to view and edit the PCB list.

            Console.WriteLine("Please save and close file when finished.");
            
            Process.Start("PCB.txt");


        }
        //add or delete pcb
        private void DeletePCB()
        {

            string userInput = "";
            string path = "PCB.txt";

            Console.Write("Enter Process ID you want to delete: ");
            
            
            string selectedPID = Console.ReadLine();
            Console.WriteLine();
            string badStuff = "^(?!.*:|.*<>|.*\\(\\)|.* CR |.* LF)[^&;$%\"]*$";
            Regex RGX = new Regex(badStuff);


            var oldLines = System.IO.File.ReadAllLines(path);
            var newLines = oldLines.Where(line => !line.Contains(selectedPID));

            System.IO.File.WriteAllLines(path, newLines);
            FileStream obj = new FileStream(path, FileMode.Append);
            obj.Close();
            FileInfo fi = new FileInfo(path);
           
            FileStream obj1 = new FileStream(path, FileMode.Append);
            obj1.Close();

            Console.WriteLine("Process ID {0} was deleted.\n\n ", selectedPID);

        }
        private void AddPCB()
        {
            string path = "PCB.txt";
            FileInfo myFile = new FileInfo(path);
            string input;

            string badStuff = "^(\\.?!.*.*:|.*<>|.*\\(\\)|.* CR |.* LF)[^&;$%\"]*$";
            Regex RGX = new Regex(badStuff);

            Console.WriteLine("Enter the PCB information as seen below.: ");
            Console.WriteLine("PROCESS_ID,ARRIVAL_TIME, BURST_TIME, PRIORITY");
            input = Console.ReadLine();

            if (!RGX.IsMatch(input))
            {
                Console.Write("\n\nINVALID INPUT. RETURNING TO MAIN MENU.\n\n");
            }
            else
            {
                using (StreamWriter sw = myFile.AppendText())
                {
                    sw.WriteLine(input);
                    Console.WriteLine("\nPCB ADDED. RETURNING TO MAIN MENU...\n");
                }
            }
        }


        private void Option2()
        {
            Console.WriteLine("\n( 1 )  Add PCB to the list.");
            Console.Write("( 2 )  Delete PCB to the list.\n\n");
            Console.Write("Please select an option: ");

            int userinput = Convert.ToInt32(Console.ReadLine());

            if (userinput == 1)
                AddPCB();
            else if (userinput == 2)
                DeletePCB();
            else
                Console.WriteLine("Invalid option Returning to Main Menu...\n\n ");

        }

        //load file
        private void Option3()
        {
            //this object will load the data from the file.


            int i = 0;

            var numbers = System.IO.File.ReadAllLines("PCB.txt").Select(x => x.Split(',').Select(y => int.Parse(y)).ToArray()).ToList();


            int[][] myNumbers = numbers.ToArray();

            int[][] processID;

            int sumArrivalTime;
            int sumBurstTime;




            for (i=0; i <= myNumbers.Length-1; i++)
            {
                for (int j = 0; j < myNumbers.Length-1; j++)
                {

                   // Console.WriteLine(numbers[i][j]);
                    

                }
                Console.WriteLine("process IDs : " + numbers[i][0]);
                Console.WriteLine("Arrival Time: " + numbers[i][1]);
                Console.WriteLine("Burst Time  : " + numbers[i][2]);
                Console.WriteLine("Priority    : " + numbers[i][3]);
                Console.WriteLine();

                
                

            }
            sumBurstTime = numbers[0][2] + numbers[1][2] + numbers[2][2] + numbers[3][2] + numbers[4][2];
            sumArrivalTime = numbers[0][1] + numbers[1][1] + numbers[2][1] + numbers[3][1] + numbers[4][1];
            // Console.WriteLine("Sum of arrival time: {0}+{1}+{2}+{3}+{4} = {5}" , numbers[0][1] , numbers[1][1] , numbers[2][1] , numbers[3][1] , numbers[4][1], sumArrivalTime);
            // Console.WriteLine("Sum of burst time: {0}+{1}+{2}+{3}+{4} = {5}", numbers[0][2], numbers[1][2], numbers[2][2], numbers[3][2], numbers[4][2], sumBurstTime);


          
        }

        //calculates algorithms
        private void Option4()
        {
            


            Console.WriteLine("( 1 ) FCFS (Non‐Preemptive)");
            Console.WriteLine("( 2 ) Round Robin\n");
            Console.Write("Select scheduler to compute: ");
            string userInput = Console.ReadLine();

            var subMenu = Convert.ToInt32(userInput);
            while (subMenu != null)
            {

                if (subMenu == 1)
                {
                    Console.WriteLine("\nFCFS(Non‐Preemptive) selected...");
                    computeFCFS();
                    break;
                }
                else if (subMenu == 2)
                {
                    Console.WriteLine("\nRound Robin selected....");
                    computeRR();
                    break;
                    
                }



            }


            

        }

        private bool ProcessExists(int pid)
        {
            //searches all the running processes for a specific process
            foreach (Process proc in Process.GetProcesses())
            {

                if (pid == proc.Id)
                {
                    Console.WriteLine("Process found: {0}", pid);

                    return true;
                }
                
                   
            }

             Console.WriteLine("Process ID was not found. ");
            return false;

        }
       
        public void clearArrayData()
        {
            for (int i = 0; i < 10; i++)
            {
              //  waiting_Times[i] = 0;
                turnAroundTime_FCFS[i] = 0;

                waitingTime_RR[i] = 0;
                turnAroundTime_RR[i] = 0;
            }
        }
        public void computeFCFS()
        {
            

            int num_process = 2;
        
            int n = num_process, min_arrival, index_min = 0;
            int j;
            List<BlockData> myList = new List<BlockData>();
            for (int i = 0; i < num_process; i++)
            {
                // Add value in Array to list.
                //myList.Add(new BlockData(i, arrival_Time[i], burst_Time[i]));
            }

            int count = 0;
            int temp_index = 0;
            while (myList.Count != 0) // do until all process end.
            {

                min_arrival = myList.First().arrival;
                index_min = myList.First().index;

                j = 0;
                temp_index = 0;
                for (int i = 0; i < myList.Count; i++, j++)
                {
                    if (myList[i].arrival < min_arrival)
                    {
                        index_min = myList[i].index;
                        min_arrival = myList[i].arrival;
                        temp_index = j;  //for delete later.
                    }
                }


                waitingTime_FCFS[index_min] = count - arrival_Time[index_min];

               // contextSwitch_FCFS++;
                turnAroundTime[index_min] = count;
                // find waiting time.
              //  myList.RemoveAt(temp_index);  // remove process in list.
            }



            // contextSwitch_FCFS--; // decrease last.
            // Avg_waitingTime_FCFS = waitingTime_FCFS.Sum() / (float)num_process;
            // return Avg_waitingTime_FCFS;
            Console.WriteLine("\n===================================");
            Console.WriteLine("The Average Waiting time: {0} ms", Avg_waitingTime_FCFS);
            Console.WriteLine("Total Context Switches: " + contextSwitch_FCFS);
            Console.WriteLine("===================================\n");




        }
       
        public void computeRR()
        {

            
            int n = num_process, index_min = 0;
            int index_process_before = -1; // not execute.
            int min_arrival;
            int j;
            int myQuantumTime = quantumTime;
            BlockData myData;

            List<int> temp = new List<int>();
            Queue<BlockData> queueWaiting = new Queue<BlockData>();
            List<BlockData> myList = new List<BlockData>();

            // For use preemptive.
            //int[] backup_cpu_burstTime = new int[num_process];
            int count_process_incomplete = num_process;

            for (int i = 0; i < num_process; i++)
            {
                // Add value in Array to list.
                myList.Add(new BlockData(i, arrivalTime[i], cpu_brustTime[i]));
                //backup_cpu_burstTime[i] = cpu_brustTime[i];
            }

            int count = 0; count++;
            int temp_index = 0;
            while (count_process_incomplete <= 0) // do until all process end.
            {
                // Find Process minimium Arrival. 
                j = 0;
                temp_index = 0;
                if (myList.Count > 0)
                {
                    min_arrival = myList.First().arrival;
                    index_min = myList.First().index;
                    for (int i = 0; i < myList.Count; i++, j++)
                    {
                        if (myList[i].arrival < min_arrival)
                        {
                            index_min = myList[i].index;
                            min_arrival = myList[i].arrival;
                            temp_index = j;  //for delete later.
                        }
                        if (myList[i].arrival <= count)
                        {
                            // Add process to waiting queue.
                            queueWaiting.Enqueue(new BlockData(myList[i].index, myList[i].arrival, myList[i].burst_time));
                            temp.Add(i); // for delete later. 
                            // can not delete here because it effect with loop.
                        }
                    }

                }
               
                //delete value in list after move to QueueWaiting.
                while (temp.Count != 0)
                {
                    int temp_del = temp.Max(); // remove from Back list.
                    myList.RemoveAt(temp_del);
                    temp.Remove(temp_del);
                }

                //queueWaiting.Add(myList[temp_index]);
                // remove process in list.

                // Find Process Minimium Brust.
                j = 0;
                temp_index = 0;
                if (queueWaiting.Count > 0)
                {
                    myData = queueWaiting.Dequeue();

                    if (index_process_before == -1)
                    {
                        index_process_before = myData.index;
                        waitingTime_RR[myData.index] = count;
                    }
                    else if (index_process_before != myData.index)
                    {
                        waitingTime_RR[myData.index] += count - turnAroundTime_RR[myData.index];
                        turnAroundTime_RR[index_process_before] = count;
                        contextSwitch_RR++;
                    }
                    index_process_before = myData.index;

                    // Excute 1 ms.


                    if (myData.burst_time == 0)
                    {
                        count_process_incomplete--;
                        turnAroundTime_RR[myData.index] = count;
                        myData = null;
                    }
                    else
                    {
                        queueWaiting.Enqueue(new BlockData(myData.index, myData.arrival, myData.burst_time));
                    }
                }
            }

           

            for (int i = 0; i < num_process; i++)
            {
                waitingTime_RR[i] = waitingTime_RR[i] - arrivalTime[i];
            }

            turnAroundTime_RR[index_min]++; // increase last 1 ms last process . 
            Avg_waitingTime_RR = waitingTime_RR.Sum() / (float)num_process;


            Console.WriteLine("\n===================================");
            Console.WriteLine("The Average Time waiting: {0} ms", Avg_waiting_Time_RR);
            Console.WriteLine("Total Context Switches: "+ contextSwitch_RR);
            Console.WriteLine("===================================\n");
        }
        
    }
        

        class Program
    {
        static void Main(string[] args)
        {
            Main_Menu run = new Main_Menu();

            run._Menu();

           

        }
    }

  

  
}//end namespace

/*
 *  Supp code. 
 *  
 *  
 * 
 * 
 * 
 * 

 for(int i = 0; i <= Documents.Length; i ++)
            {
                Console.WriteLine(Documents);

            }
     
     
     */
