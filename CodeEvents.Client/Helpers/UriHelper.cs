namespace CodeEvents.Client.Helpers
{
    public class UriHelper
    {
        private const string root = "api/events";
        private const string include = "?includelectures=true";
        public static string GetEvents(bool includeLectures = false) => includeLectures ?  $"{root}/{include}" : $"{root}";
        public static string GetEvent(string eventName, bool includeLectures = false) => includeLectures ?  $"{root}/{eventName}/{include}" : $"{root}/{eventName}";
        public static string GetLectures(string eventName) => $"{root}/{eventName}/lectures";
        public static string GetLectureForEvent(string eventName, int id) => $"{root}/{eventName}/lectures/{id}";
    }
}
