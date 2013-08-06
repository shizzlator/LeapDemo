using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace LeapDemo
{
    public class DemoHub : Hub
    {
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