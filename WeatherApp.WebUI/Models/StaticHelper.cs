namespace WeatherApp.WebUI.Models
{
    public static class StaticHelper
    {
        public static DateTime ToCustomDate(this string date)
        {
            
            var inputData = date.Split('-');

            return new DateTime(int.Parse(inputData[0]), int.Parse(inputData[1]), int.Parse(inputData[2]));
        }
    }
}
