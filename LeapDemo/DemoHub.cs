using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace LeapDemo
{
    public class DemoHub : Hub
    {
        private const int Threshold = 200;
        public static int RockCount;
        public static int PaperCount;
        public static int ScissorsCount;

        public void Send(string frameData)
        {
            Clients.All.recieveData(frameData);
            var frame = JsonConvert.DeserializeObject<dynamic>(frameData);

            //TODO: should I remove the elses? > or < accurate?
            //TODO: Would this code be better off on the client???
            if (FrameContainsRockGesture(frame))
            {
                RecordRock();
            }
            else if(FrameContainsScissorsGesture(frame))
            {
                RecordScissors();
            }
            else if (FrameContainsPaperGesture(frame))
            {
                RecordPaper();
            }
        }

        private static bool FrameContainsPaperGesture(dynamic frame)
        {
            return frame.hands.Count == 1 && frame.pointables.Count == 5;
        }

        private static bool FrameContainsScissorsGesture(dynamic frame)
        {
            return frame.hands.Count == 1 && frame.pointables.Count == 2;
        }

        private static bool FrameContainsRockGesture(dynamic frame)
        {
            return frame.hands.Count == 1 && frame.pointables.Count == 0;
        }

        public void RecordRock()
        {
            lock (this)
            {
                if (RockCount == Threshold)
                {
                    Clients.All.recieveRock();
                    RockCount = 0;
                }
                else
                    RockCount++;
            }   
        }

        public void RecordPaper()
        {
            lock (this)
            {
                if (PaperCount == Threshold)
                {
                    Clients.All.recievePaper();
                    PaperCount = 0;
                }
                else
                    PaperCount++;
            }
        }

        public void RecordScissors()
        {
            lock (this)
            {
                if (ScissorsCount == Threshold)
                {
                    Clients.All.recieveScissors();
                    ScissorsCount = 0;
                }
                else
                    ScissorsCount++;    
            }
        }
    }
}