using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Test_iAge
{
    /// <summary>
    /// Класс, описывающий сотрудника
    /// </summary>
    public class Worker
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Зарплата в час
        /// </summary>
        public decimal SalaryPerHour { get; set; }

        /// <summary>
        /// Директория с файлом
        /// </summary>
        string fileName = @"Workers.json";

        /// <summary>
        /// Добавление сотрудника в JSON-файл
        /// </summary>
        /// <param name="args">Аругменты</param>
        /// <returns>Возвращает статус выполнения операции</returns>
        public int Add(string[] args)
        {
            if (args.Length != 4) return -1;
            var listWorkers = GetAll();
            if (listWorkers == null || listWorkers.Count <= 0) Id = 1;
            else Id = listWorkers.OrderByDescending(w => w.Id).First().Id + 1;
            var newWorker = SwitchChoise(args);
            newWorker.Id = Id;
            if (listWorkers == null) listWorkers = new List<Worker> { newWorker };
            else listWorkers.Add(newWorker); 
            File.WriteAllText(fileName, JsonConvert.SerializeObject(listWorkers));
            return 0;
        }

        /// <summary>
        /// Обновление данных о сотруднике
        /// </summary>
        /// <param name="args">Аругменты</param>
        /// <returns>Возвращает статус выполнения операции</returns>
        public int Update(string[] args)
        {
            var listWorkers = GetAll();
            if (listWorkers == null || listWorkers.Count <= 0) return -1;
            var updateWorker = SwitchChoise(args);
            if (updateWorker.Id < 0 || updateWorker == null) return -2;
            var oldWorker = listWorkers.FirstOrDefault(w => w.Id == updateWorker.Id);
            if (oldWorker == null) return -3;
            var index = listWorkers.IndexOf(oldWorker);
            if (!string.IsNullOrEmpty(updateWorker.FirstName)) oldWorker.FirstName = updateWorker.FirstName;
            if (!string.IsNullOrEmpty(updateWorker.LastName)) oldWorker.LastName = updateWorker.LastName;
            if (updateWorker.SalaryPerHour > 0) oldWorker.SalaryPerHour = updateWorker.SalaryPerHour;
            listWorkers[index] = updateWorker;
            File.WriteAllText(fileName, JsonConvert.SerializeObject(listWorkers));
            return 0;
        }

        /// <summary>
        /// Получить полный список сотрудников
        /// </summary>
        /// <returns>Возвращает список сотрудников</returns>
        public List<Worker> GetAll()
        {
            string fileContains = "";
            if (File.Exists(fileName))
            {
                fileContains = File.ReadAllText(fileName);
            }
            return JsonConvert.DeserializeObject<List<Worker>>(fileContains);
        }

        /// <summary>
        /// Парсинг аргументов
        /// </summary>
        /// <param name="arg">Аргумент</param>
        /// <returns>Возвращает наименование параметра и его значение</returns>
        (string, string) ParseString(string arg)
        {
            if (!arg.Contains(":")) return ("","");
            var args = arg.Split(':');
            return (args[0], args[1]);
        }

        /// <summary>
        /// Получить данные о сотруднике
        /// </summary>
        /// <param name="args">Аргументы</param>
        /// <returns>Возвращает данные о сотруднике</returns>
        public string Get(string[] args)
        {
            var (key, value) = ParseString(args[1]);
            var f = int.TryParse(value, out int id);
            if (!f) return "Неверный формат идентификатора!";
            var listWorkers = GetAll();
            if (listWorkers == null || listWorkers.Count <= 0) return "Список сотрудников пуст";
            var worker = listWorkers.FirstOrDefault(w => w.Id == id);
            if (worker == null) return "Сотрудника с таким идентификатором не существует";
            return worker.Format();
        }

        /// <summary>
        /// Удаляет сотрудника
        /// </summary>
        /// <param name="args">Аргументы</param>
        /// <returns>Возвращает статус исполнения</returns>
        public int Delete(string[] args)
        {
            var (key, value) = ParseString(args[1]);
            var f = int.TryParse(value, out int id);
            if (!f) return -1;
            var listWorkers = GetAll();
            if (listWorkers == null || listWorkers.Count <= 0) return -2;
            var worker = listWorkers.FirstOrDefault(w => w.Id == id);
            if (worker == null) return -3;
            listWorkers.Remove(worker);
            File.WriteAllText(fileName, JsonConvert.SerializeObject(listWorkers));
            return 0;
        }

        /// <summary>
        /// Получить объект "сотрудник" в заданном формате
        /// </summary>
        /// <returns>Возвращает данные о сотруднике в заданном формате</returns>
        public string Format()
        {
            return $"Id = {Id}, FirstName = {FirstName}, LastName = {LastName}, SalaryPerHour = {SalaryPerHour}";
        }

        /// <summary>
        /// Разбор аргументов
        /// </summary>
        /// <param name="args">Аргументы</param>
        /// <returns>Возвращает данные о сотруднике</returns>
        Worker SwitchChoise(string[] args)
        {
            var worker = new Worker();
            foreach (string arg in args)
            {
                var (key, value) = ParseString(arg);
                switch (key)
                {
                    case "Id":
                        bool f = int.TryParse(value, out int id);
                        if (f) worker.Id = id;
                        else worker.Id = -1;
                        break;
                    case "FirstName":
                        worker.FirstName = value;
                        break;
                    case "LastName":
                        worker.LastName = value;
                        break;
                    case "Salary":
                        f = decimal.TryParse(value, out decimal Salary);
                        if (f) worker.SalaryPerHour = Salary;
                        else worker.SalaryPerHour = - 1;
                        break;
                }
            }
            return worker;
        }
    }
}
