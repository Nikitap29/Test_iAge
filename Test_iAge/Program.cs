using System;

namespace Test_iAge
{
    class Program
    {
        /// <summary>
        /// Основной метод при запуске программы
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            int res;
            switch (args[0])
            {
                case "-add":
                    res = worker.Add(args);
                    if (res == 0) Console.WriteLine("Данные о новом сотруднике успешно добавлены!");
                    else Console.WriteLine("Ошибка при добавлении данных о новом сотруднике");
                    break;
                case "-update":
                    res = worker.Update(args);
                    if (res == 0) Console.WriteLine("Данные о сотруднике успешно обновлены!");
                    else Console.WriteLine("Ошибка при обновлении данных о сотруднике");
                    break;
                case "-get":
                    string message = worker.Get(args);
                    Console.WriteLine(message);
                    break;
                case "-delete":
                    res = worker.Delete(args);
                    if (res == 0) Console.WriteLine("Данные о сотруднике успешно удалены!");
                    else if (res == -3) Console.WriteLine("Сотрудника с указанным идентификатором не существует");
                    else Console.WriteLine("Ошибка при удалении данных о сотруднике");
                    break;
                case "-getall":
                    var list = worker.GetAll();
                    if (list == null) Console.WriteLine("Отсутствуют данные о сотрудниках");
                    else
                    {
                        foreach (var w in list) Console.WriteLine(w.Format());
                    }
                    break;
            }
            Console.WriteLine("Нажмите Enter");
            Console.ReadLine();
        }
    }
}
