using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace LeapDemo
{
    public class DemoHub : Hub
    {
        public void Send(string frameData)
        {
            Clients.All.recieveData(frameData);
            var frame = JsonConvert.DeserializeObject<dynamic>(frameData);
            foreach (var gesture in frame.gestures)
            {
                if (gesture.type == "circle" && gesture.state == "stop")
                {
                    Clients.All.recieveCircle();
                }
            }
        }
    }
}