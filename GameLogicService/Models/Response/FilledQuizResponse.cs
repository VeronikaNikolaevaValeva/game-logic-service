namespace GameLogicService.Models.Response
{
    public class FilledQuizResponse
    {
        //{"accountEmailAddress":"1",
        //"category":"General Knowledge",
        //"correctAnswers":[["622a1c367cc59eab6f950079","Washington DC"],["622a1c397cc59eab6f950fb3","Apple"],["622a1c3d7cc59eab6f951b87","Apple"],["622a1c3e7cc59eab6f95226c","Brakes"]],
        //"givenAnswers":[" ",[["622a1c367cc59eab6f950079","Washington DC"]],[["622a1c397cc59eab6f950fb3","Apple"]],[["622a1c3d7cc59eab6f951b87","Microsoft"]],[["622a1c3e7cc59eab6f95226c","Engine"]]]}

        public string? accountEmailAddress{ get; set; }
        public string? category { get; set; }
        public List<List<string>?>? correctAnswers { get; set; }
        public List<List<string>?>? givenAnswers { get; set; }
        
    }
}
