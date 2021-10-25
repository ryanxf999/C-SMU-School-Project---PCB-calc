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



namespace CSE_7343_Project
{
    internal class BlockData
    {

        public int arrival;
        public int index;
        public int burst_time;
        public int priority;

        public BlockData()
        {

        }
        public BlockData(int i, int value, int b)
        {
            index = i;
            arrival = value;
            burst_time = b;
        }

        public BlockData(int i, int value, int b, int p)
        {
            index = i;
            arrival = value;
            burst_time = b;
            priority = p;
        }

    }
}