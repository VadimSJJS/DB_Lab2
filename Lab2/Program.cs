namespace Lab2
{
    internal class Program
    {
        static void Main()
        {
            RequestiesDB queries = new RequestiesDB();

            queries.SelectOneRelation();

            queries.SelectOneRelationWithFilter();

            queries.GetAverageMedicineCountsByCountry();

            queries.GetMedicineDiseaseRelations();

            queries.GetFilteredMedicinesWithConditions();

             var newProducerType = new Producer()
             {
                 Name = "100050",
                 Country = "Болезнь 50"
             };

             queries.DeleteOneRelation(newProducerType);

            queries.DeleteManyRelation();
            queries.UpdateTable();
        }
    }
}