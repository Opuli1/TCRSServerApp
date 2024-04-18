using TCRSServerApp.Data;

namespace TCRSServerApp.Services
{
    public class BannerService
    {
        private readonly TCRSContext _context;

        private bool _clientVisible;

        public bool Visible { get; set; }

        public bool ClientVisible
        {
            get => _clientVisible && Visible;
            set
            {
                _clientVisible = value;

                OnChange?.Invoke();
            }
        }

        public string Message { get; set; }

        public event Action OnChange;

        public BannerService(TCRSContext context)
        {
            _context = context;

            LoadInitialSettings();
        }

        private void LoadInitialSettings()
        {
            var settings = _context.BannerSettings.FirstOrDefault();

            if (settings != null)
            {
                Visible = settings.Visible;

                _clientVisible = settings.Visible;

                Message = settings.Message;
            } else
            {
                throw new InvalidOperationException("(LoadInitialSettings) No Banner Settings Found...");
            }
        }

        public void UpdateBanner(bool visibility, string message)
        {
            var settings = _context.BannerSettings.FirstOrDefault();

            if(settings != null)
            {
                settings.Visible = visibility;

                settings.Message = message;

                _context.SaveChanges();

                Visible = visibility;

                _clientVisible = visibility;

                Message = message;

                OnChange?.Invoke();
            }
        }

        public void ToggleVisibility(bool visibility)
        {
            ClientVisible = visibility;
        }
    }
}
