using F1T.Core;
using F1T.Settings;
using F1T.Structs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace F1T.MVVM.ViewModels
{
    public class F1LViewModel : BaseModuleViewModel<F1LSettings>
    {
        // === BEGINING OF MODULE SETUP ===
        // === Singleton Instance with Thread Saftey ===
        private static F1LViewModel _instance = null;
        private static object _singletonLock = new object();
        public static F1LViewModel GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new F1LViewModel(); }
                return _instance;
            }
        }

        // HTTP
        private HttpClient client;
        private CookieContainer cookies;
        private long _userId = -1;
        private long UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if(value != _userId && value != -1)
                {
                    _userId = value;
                    GetDriver();
                }
                else
                {
                    _userId = value;
                }
            }
        }
        private long DriverId;

        // Settings
        private F1LSettings _settings = new F1LSettings().Read<F1LSettings>();
        public override F1LSettings Settings { get => _settings; }

        private F1LViewModel() : base()
        {
            LoggedInLabel = "Not logged in...";

            FormationLapReplayFileLocation = "Not selected...";
            RaceReplayFileLocation = "Not selected...";
            QualifyingResultsFileLocation = "Not selected...";
            RaceResultsFileLocation = "Not selected...";

            cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            client = new HttpClient(handler);
            client.BaseAddress = new Uri("http://localhost:8080/api/");

            if (Settings.RefreshToken != "")
            {
                cookies.Add(client.BaseAddress, new Cookie("rid", Settings.RefreshToken));
                RefreshAuthentication();
            }
        }


        private CollectionView _races;
        public CollectionView Races
        {
            get { return _races; }
        }

        private Race _race;
        public Race Race
        {
            get { return _race; }
            set { SetField(ref _race, value, "Race"); }
        }

        public async void LoginAsync()
        {
            try
            {
                var request = new LoginRequest(Settings.Username, Password);
                var content = new StringContent(JsonConvert.SerializeObject(request, Formatting.None, new ByteArrayConverter()), Encoding.UTF8, "application/json");
                var URL = client.BaseAddress + "auth/login";

                var response = client.PostAsync(URL, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();

                    var responseCookies = cookies.GetCookies(new Uri(URL)).Cast<Cookie>();
                    var jwt = JsonConvert.DeserializeObject<JwtResponse>(body);

                    LoggedInLabel = "Logged in as " + jwt.username;
                    // Reversing here to remove the old rid
                    Settings.RefreshToken = responseCookies.Reverse().FirstOrDefault(x => x.Name == "rid").Value;
                    Settings.AuthToken = jwt.token;
                    UserId = jwt.id;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async void RefreshAuthentication()
        {
            try
            {
                var URL = client.BaseAddress + "auth/refresh";

                var response = client.PostAsync(URL, new StringContent("")).Result;


                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();

                    var responseCookies = cookies.GetCookies(new Uri(URL)).Cast<Cookie>();
                    var jwt = JsonConvert.DeserializeObject<JwtResponse>(body);

                    LoggedInLabel = "Logged in as " + jwt.username;
                    Settings.RefreshToken = responseCookies.FirstOrDefault(x => x.Name == "rid").Value;
                    Settings.AuthToken = jwt.token;
                    UserId = jwt.id;

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Logout()
        {
            try
            {
                var URL = client.BaseAddress + "auth/logout";

                var response = client.PostAsync(URL, new StringContent("")).Result;

                if (response.IsSuccessStatusCode)
                {

                    LoggedInLabel = "Not logged in...";
                    Settings.RefreshToken = "";
                    Settings.AuthToken = "";
                    UserId = -1;
                }
            } catch (Exception ex)
            {

            }
        }

        public async void GetDriver()
        {
            try 
            {
                var URL = client.BaseAddress + "users/" + UserId + "/driver";
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Settings.AuthToken);

                var response = client.GetAsync(URL).Result;

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();

                    var driver = JsonConvert.DeserializeObject<Driver>(body);
                    DriverId = driver.id;

                    GetRaces();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public async void GetRaces()
        {
            try
            {
                var URL = client.BaseAddress + "drivers/" + DriverId + "/races?limit=3&completed=false&start=" + DateTime.UtcNow.AddDays(-1).ToString("o", CultureInfo.InvariantCulture) + "&end=" + DateTime.UtcNow.AddDays(1).ToString("o", CultureInfo.InvariantCulture);

                var response = client.GetAsync(URL).Result;

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    _races = new CollectionView(JsonConvert.DeserializeObject<Race[]>(body));
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async void UploadResults()
        {
            Trace.WriteLine(Race);
        }


        private String _loggedInLabel;
        public String LoggedInLabel
        {
            get { return _loggedInLabel; }
            set { SetField(ref _loggedInLabel, value, "LoggedInLabel"); }
        }

        private String _password;
        public String Password
        {
            get { return _password; }
            set { SetField(ref _password, value, "Password"); }
        }

        private String _formationLapReplayFileLocation;
        public String FormationLapReplayFileLocation
        {
            get { return _formationLapReplayFileLocation; }
            set { SetField(ref _formationLapReplayFileLocation, value, "FormationLapReplayFileLocation"); }
        }

        private String _raceReplayFileLocation;
        public String RaceReplayFileLocation
        {
            get { return _raceReplayFileLocation; }
            set { SetField(ref _raceReplayFileLocation, value, "RaceReplayFileLocation"); }
        }

        private String _qualifyingResultsFileLocation;
        public String QualifyingResultsFileLocation
        {
            get { return _qualifyingResultsFileLocation; }
            set { SetField(ref _qualifyingResultsFileLocation, value, "QualifyingResultsFileLocation"); }
        }

        private String _raceResultsFileLocation;
        public String RaceResultsFileLocation
        {
            get { return _raceResultsFileLocation; }
            set { SetField(ref _raceResultsFileLocation, value, "RaceResultsFileLocation"); }
        }
    }

    class LoginRequest
    {
        public String username;
        public String password;

        public LoginRequest(String username, String password)
        {
            this.username = username;
            this.password = password;
        }
    }

    class JwtResponse
    {
        public String token;
        public String type = "Bearer";
        public long id;
        public String username;
        public String email;

        public JwtResponse(String token, long id, String username, String email)
        {
            this.token = token;
            this.id = id;
            this.username = username;
            this.email = email;
        }
    }

    class Driver
    {
        public long id;

        public Driver(long id)
        {
            this.id = id;
        }
    }

    public class Race : ObservableObject
    {

        private long _id;
        public long Id
        {
            get { return _id; }
            set { SetField(ref _id, value, "Id"); }
        }

        private String _circuitDisplayName;
        public String CircuitDisplayName
        {
            get { return _circuitDisplayName; }
            set { SetField(ref _circuitDisplayName, value, "CircuitDisplayName"); }
        }

        private DateTime _scheduledDate;
        public DateTime ScheduledDate
        {
            get { return _scheduledDate; }
            set { SetField(ref _scheduledDate, value, "ScheduledDate"); }
        }


        public Race(long id, String circuitDisplayName, DateTime scheduledDate)
        {
            this.Id = id;
            this.CircuitDisplayName = circuitDisplayName;
            this.ScheduledDate = scheduledDate;
        }
    }
}
