using System;
using System.Net;
using System.IO;

namespace Egg_roll.Server
{
    public class ServerHandler
    {
        public string responseString = "none";

        public bool scoresFetched, scoresSent = false;

        string deviceID;

        public bool ScoresFetched
        {
            get { return scoresFetched; }
        }

        public bool ScoresSent
        {
            get { return scoresSent; }
        }

        public void BeginFetchHighScores()
        {
            scoresFetched = false;
            FetchHighScores();
        }

        public void BeginPushHighScores(float score, string playerName)
        {
            scoresSent = false;
            scoresFetched = false;
            PushHighScore(score, playerName);
        }

        public ScoreTable HighScores()
        {
            ScoreTable scoreTable = new ScoreTable();
            string[] splitResponse = responseString.Split(' ');
            if (responseString != "Could not get high scores.")
            {
                foreach (string x in splitResponse)
                {
                    string[] split = x.Split('_');
                    scoreTable.Add(split[1], split[2]);
                }
            }
            return scoreTable;
        }

        private void FetchHighScores()
        {
            string uri = "http://eggroll.grupp3.se/requestscores.php?Format=TOP10";
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.BeginGetResponse(GetHighScoresRequest, request);
            }
            catch
            {
                //do something if we cant get high scores
            }
        }

        private void GetHighScoresRequest(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            //request.Method = "GET";

            if (request != null)
            {
                try
                {
                    using (var response = request.EndGetResponse(result))
                    {
                        using (var stream = new StreamReader(response.GetResponseStream()))
                        {
                            responseString = stream.ReadToEnd();
                            scoresFetched = true;
                        }
                    }
                }
                catch (WebException e)
                {
                    responseString = "Could not get high scores.";
                    scoresFetched = true;
                    return;
                }
            }
        }

        private void PushHighScore(float score, string playerName)
        {
            int scoreToSend = Convert.ToInt32(score); 
            object uniqueID;
            uniqueID = Microsoft.Phone.Info.DeviceExtendedProperties.GetValue("DeviceUniqueId");
            byte[] bID = (byte[])uniqueID;
            deviceID = Convert.ToBase64String(bID); 

            string uri = "http://eggroll.grupp3.se/newscore.php?Name=" + playerName + "&Score=" + scoreToSend + "&DeviceID=" + deviceID;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.BeginGetResponse(SendHighScoresRequest, request);
            }
            catch
            {
                //do something if we cant send high scores
            }
        }

        private void SendHighScoresRequest(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            //request.Method = "GET";
            

            if (request != null)
            {
                try
                {
                    using (var response = request.EndGetResponse(result))
                    {
                        using (var stream = new StreamReader(response.GetResponseStream()))
                        {
                            responseString = stream.ReadToEnd();
                            scoresSent = true;
                        }
                    }
                }
                catch (WebException e)
                {
                    responseString = "Could not send high score.";
                    responseString += e.ToString();
                    scoresSent = true;
                    return;
                }
            }
        }

    }
}
