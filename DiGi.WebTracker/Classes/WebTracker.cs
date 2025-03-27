using System.Timers;

namespace DiGi.WebTracker.Classes
{
    public class WebTracker
    {
        private bool busy = false;
        private HttpClient httpClient = new HttpClient();
        private string pageHash;
        private System.Timers.Timer timer;
        private string url;
        public WebTracker(string url, int interval)
        {
            timer = new System.Timers.Timer(interval);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;

            pageHash = null;

            this.url = url;
        }

        public bool Busy
        {
            get
            {
                return busy;
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
        
        private async Task<bool> Check()
        {
            if(busy)
            {
                return false;
            }
            
            busy = true;

            string pageHash = await Query.PageHash(httpClient, url);

            busy = false;

            if(this.pageHash == null)
            {
                this.pageHash = pageHash;
                return true;
            }

            if(pageHash != this.pageHash)
            {
                return false;
            }

            return true;

        }

        private async void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            bool check = await Check();
        }
    }
}
