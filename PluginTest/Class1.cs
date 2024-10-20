using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using Newtonsoft.Json;
using PhoneApp.Domain;
using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;

namespace EmployeesLoaderPlugin
{
    [Author(Name = "Dane4ka")]
    public class Plugin : IPluggable
    {
      
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            Console.Write("Введите путь к JSON файлу: ");
            string filePath = Console.ReadLine(); 

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл не найден по указанному пути: {filePath}");
               return Enumerable.Empty<DataTransferObject>(); // Возвращаем пустую коллекцию
            }

            try
            {
                
                List<EmployeesDTO> source = JsonConvert.DeserializeObject<List<EmployeesDTO>>(File.ReadAllText(filePath));

               
                return source.Cast<DataTransferObject>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Ошибка при десериализации JSON: " + ex.Message);
               return Enumerable.Empty<DataTransferObject>(); // Возвращаем пустую коллекцию
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                return Enumerable.Empty<DataTransferObject>(); // Возвращаем пустую коллекцию
            }

        }


    }
}
