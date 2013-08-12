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

        public void SendToTwo(string frameData)
        {
            Clients.All.recieveFromOne(frameData);
        }

        public void SendToOne(string frameData)
        {
            Clients.All.recieveFromTwo(frameData);
        }

        public void Send(string frameData)
        {
            Clients.All.recieveData(frameData);
        }

        public void SendGesture(string gestureData)
        {
            var gesture = JsonConvert.DeserializeObject<dynamic>(gestureData).gesture;

            if(gesture == "rock")
                Clients.All.recieveRock();
            if (gesture == "paper")
                Clients.All.recievePaper();
            if (gesture == "scissors")
                Clients.All.recieveScissors();
        }
    }
}