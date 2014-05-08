using System.Collections.Specialized;

namespace TargetServerCommunicator.Servers
{
    public class MockGameServerInterface: GameServerInterface
    {
		//private const string CONST_TARGETS_DATA = "[{\"status\": 0, \"movingState\": false, \"led\": 11, \"name\": \"one\", \"spawnRate\": 2.0, \"isMoving\": false, \"points\": 10.0, \"startTime\": 0, \"x\": 10.0, \"y\": 1.0, \"input\": 7, \"z\": 2.0, \"id\": 1, \"hit\": 0, \"dutyCycle\": 1.5}, {\"status\": 1, \"movingState\": false, \"led\": 15, \"name\": \"two\", \"spawnRate\": 2.0, \"isMoving\": false, \"points\": 30.0, \"startTime\": 0, \"x\": 10.0, \"y\": 2.0, \"input\": 13, \"z\": 43.0, \"id\": 2, \"hit\": 0, \"dutyCycle\": 1.5}, {\"status\": 0, \"movingState\": false, \"led\": 16, \"name\": \"three\", \"spawnRate\": 2.0, \"isMoving\": false, \"points\": 32.0, \"startTime\": 0, \"x\": 3.0, \"y\": 2.0, \"input\": 12, \"z\": 21.0, \"id\": 3, \"hit\": 1, \"dutyCycle\": 1.5}, {\"status\": 1, \"movingState\": false, \"led\": 22, \"name\": \"four\", \"spawnRate\": 2.0, \"isMoving\": false, \"points\": 34.0, \"startTime\": 0, \"x\": 2.0, \"y\": 4.0, \"input\": 18, \"z\": 2.0, \"id\": 4, \"hit\": 0, \"dutyCycle\": 1.5}]";
		private const string CONST_TARGETS_DATA = "[{\"hit\": 0, \"status\": 0, \"movingState\": false, \"led\": 11, \"name\": \"one\", \"spawnRate\": 2.0, \"isMoving\": false, \"points\": 10.0, \"startTime\": 0, \"x\": -9.0, \"y\": 24.75, \"input\": 7, \"z\": 1.0, \"id\": 1, \"dutyCycle\": 1.5}, {\"hit\": 0, \"status\": 0, \"movingState\": false, \"led\": 15, \"name\": \"two\", \"spawnRate\": 2.0, \"isMoving\": false, \"points\": 30.0, \"startTime\": 0, \"x\": -4.25, \"y\": 29.0, \"input\": 13, \"z\": 1.0, \"id\": 2, \"dutyCycle\": 1.5}, {\"hit\": 1, \"status\": 0, \"movingState\": false, \"led\": 16, \"name\": \"three\", \"spawnRate\": 2.0, \"isMoving\": false, \"points\": 32.0, \"startTime\": 0, \"x\": 1.5, \"y\": 35, \"input\": 12, \"z\": 1.0, \"id\": 3, \"dutyCycle\": 1.5}, {\"hit\": 0, \"status\": 0, \"movingState\": false, \"led\": 22, \"name\": \"four\", \"spawnRate\": 2.0, \"isMoving\": false, \"points\": 34.0, \"startTime\": 0, \"x\": 10.0, \"y\": 33.5, \"input\": 18, \"z\": 1.0, \"id\": 4, \"dutyCycle\": 1.5}]";
        private const string CONST_GAME_DATA    = "{\"games\": [\"test, test1\"]}";

        
        public MockGameServerInterface(string teamName)
            : base(teamName)
        {

        }

        public MockGameServerInterface(string teamName, string ipAddress, int port)
            : base(teamName, ipAddress, port)
        { 

        }

        protected override string DownloadString(string route, string request)
        {
            string data = "";
            switch(route.ToLower())
            {
                case ROUTE_GAMES:
                    data = CONST_GAME_DATA;
                    break;
                case ROUTE_TARGETS:
                    data = CONST_TARGETS_DATA;
                    break;
                
            }
            return data;
        }
        protected override void UploadValues(string route, NameValueCollection values)
        {
            // DO NOTHING ON PURPOSE
        }
    }
}
