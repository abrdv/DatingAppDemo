namespace API.Extentions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Подсчитывает количество лет(возраст)
        /// </summary>
        /// <returns>Возвращает количество лет прошедших от даты рождения до текущей даты БЕЗ УЧЁТА високосных дней</returns>
        /// <exception cref="ExceptionType">Если Дата рождения больше текущей даты, то возраст будет отрицательный, для високосного года может быть ошибка в один день.</exception>
        public static int CalculateAge(this DateOnly dateofbirth)
        {
            //i'm not considering leap years, TODO
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - dateofbirth.Year;
            if (dateofbirth > today.AddYears(-age)) age--;
            return age;
        }
    }
}
