using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab2
{
    internal class RequestiesDB
    {
        public void SelectOneRelation()
        {
            Console.WriteLine("1. Выборка всех данных из таблицы Producer:\n");

            using (var context = new PharmacyContext())
            {
                var producers = context.Producers.ToList();
                Console.WriteLine("Список производителей:");
                foreach (var producer in producers)
                {
                    Console.WriteLine($"ID: {producer.Id}, Название: {producer.Name}, Страна: {producer.Country}");
                }
            }
        }

        public void SelectOneRelationWithFilter()
        {
            Console.WriteLine("2. Выборка данных из таблицы Producer с применением фильтра:\n");

            using (var context = new PharmacyContext())
            {

                // Пример выборки производителей с условием, что страна - "США"
                var filteredProducers = context.Producers
                    .Where(producer => producer.Country == "США")
                    .ToList();
                
                foreach (var producer in filteredProducers)
                {
                    Console.WriteLine($"ID: {producer.Id}, Название: {producer.Name}, Страна: {producer.Country}");
                }
            }
        }

        public void GetAverageMedicineCountsByCountry()
        {
            Console.WriteLine("3. Выборка данных, сгруппированных по полю DiseasesId и сгрупированных по полю count:\n");

            using (var context = new PharmacyContext())
            {
                var query = from producer in context.Producers
                            join medicine in context.Medicines on producer.Id equals medicine.ProducerId
                            group new { producer, medicine } by producer.Country into grouped
                            select new
                            {
                                Country = grouped.Key,
                                AverageMedicineCount = grouped.Average(x => x.medicine.Count)
                            };

                foreach (var result in query)
                {
                    Console.WriteLine($"Country: {result.Country}, Average Medicine Count: {result.AverageMedicineCount}");
                }
            }
        }

        public void GetMedicineDiseaseRelations()
        {
            Console.WriteLine("Выборка данных из таблиц Medicine и Disease:");

            using (var context = new PharmacyContext())
            {
                var query = from medicine in context.Medicines
                            join medicineForDisease in context.MedicinesForDiseases
                            on medicine.Id equals medicineForDisease.MidicinesId
                            select new
                            {
                                MedicineName = medicine.Name,
                                DiseaseName = medicineForDisease.Diseases.Name
                            };

                foreach (var result in query)
                {
                    Console.WriteLine($"Medicine: {result.MedicineName}, Disease: {result.DiseaseName}");
                }
            }
        }

        public void GetFilteredMedicinesWithConditions()
        {
            Console.WriteLine("Выборка данных с условием");

            using (var context = new PharmacyContext())
            {
                var query = from medicine in context.Medicines
                            where medicine.Count > 10 &&
                                  medicine.MedicinesForDiseases.Any()
                            select medicine;

                var result = query.ToList();

                Console.WriteLine("Список лекарств, удовлетворяющих условиям:");
                foreach (var medicine in result)
                {
                    Console.WriteLine($"ID: {medicine.Id}");
                    Console.WriteLine($"Название: {medicine.Name}");
                    Console.WriteLine($"Описание: {medicine.ShortDescription}");
                    Console.WriteLine($"Активное вещество: {medicine.ActiveSubstance}");
                    Console.WriteLine($"Количество: {medicine.Count}");
                    Console.WriteLine($"Место хранения: {medicine.StorageLocation}");
                    Console.WriteLine();
                }
            }
        }

        public void InsertOneRelation(Disease diseaseType)
        {
            Console.WriteLine("6. Вставку данных в таблицу Diseases, стоящей на стороне отношения 'Один'");
            Console.WriteLine($"Вставка в таблицу вид страхования:\n\tНазвание - {diseaseType.Name}\n\tМеждународный код - {diseaseType.InternationalCode}");
            using (var context = new PharmacyContext())
            {
                context.Diseases.Add(diseaseType);
                context.SaveChanges();
                Console.WriteLine("Данные успешко добавлены в базу данных.");
            }
            Console.WriteLine("\n\n");
        }

        

        public void DeleteOneRelation(Producer producerType)
        {
            Console.WriteLine("8. Удаление данных из таблицы, стоящей на стороне отношения 'Один'");
            using (var context = new PharmacyContext())
            {

                Console.WriteLine($"Удаление из таблицы Producers:" +
                $"\n\tНазвание - {producerType.Name}" +
                $"\n\tОписание - {producerType.Country}");
                Console.WriteLine("Данные из таблицы Producers удалены успешно.");

                var removeInsuraceType = context.Producers.FirstOrDefault(e => e.Name.Contains(producerType.Name));
                context.Producers.Remove(removeInsuraceType);
                context.SaveChanges();
            }
            Console.WriteLine("\n\n");
        }


        public void DeleteManyRelation()
        {
            using (var context = new PharmacyContext())
            {
                // Находим запись, которую вы хотите удалить (по ID или другому условию)
                var medicineToDelete = context.Medicines.FirstOrDefault(m => m.Name == "Лекарство 1");

                if (medicineToDelete != null)
                {
                    // Удаляем запись из контекста данных и сохраняем изменения
                    context.Medicines.Remove(medicineToDelete);
                    context.SaveChanges();
                    Console.WriteLine("Запись успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Запись не найдена.");
                }
            }
        }

        public void UpdateTable()
        {
            Console.WriteLine("Обновление данных из таблицы Producer");
            using (var context = new PharmacyContext())
            {
                // Находим записи, которые удовлетворяют определенному условию (например, по имени производителя)
                var producersToUpdate = context.Producers.Where(p => p.Name == "Производитель 1").ToList();

                if (producersToUpdate.Count > 0)
                {
                    // Обновляем каждую запись
                    foreach (var producer in producersToUpdate)
                    {
                        producer.Name = "Новое имя"; // Новое значение
                        producer.Country = "Новая страна"; // Новое значение
                                                           // Вы можете также обновить другие свойства по вашему выбору
                    }

                    // Сохраняем изменения в базе данных
                    context.SaveChanges();
                    Console.WriteLine("Записи производителей успешно обновлены.");
                }
                else
                {
                    Console.WriteLine("Записи для обновления не найдены.");
                }
            }
        }

    }
}
