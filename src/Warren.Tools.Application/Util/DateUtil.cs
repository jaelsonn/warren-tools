namespace Warren.Tools.Application.Util
{
    public class DateUtil
    {
        public static DateTime DataValida(DateTime dataAtual)
        {
            while (!IsDiaUtil(dataAtual))
            {
                dataAtual = dataAtual.AddDays(-1);
            }
            return dataAtual;
        }

        public static bool IsDiaUtil(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;            
            return dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday;
        }
    }
}
