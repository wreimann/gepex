using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;

namespace Model.Base
{
    [Serializable]
    public static class DataSetUtil<T> where T : BaseEntidade
    {
        public static DataSet ConvertToDataSet(IList<T> lista) 
        {
            DataSet dataSet = new DataSet();
            if (lista.Count > 0)
            {
                CreateDataSet(dataSet, typeof(T), false);
                FillDataSet(typeof(T), lista, dataSet, -1);
                CreateRelations(dataSet, lista.GetType(), null);
            }
            else
                dataSet = null;
            return dataSet;
        }
        /// Create the structure for all the tables in the data set  
        ///Data set in which tables will be created  
        /// Type of which dataset has to be created  
        /// Whether current type is a child table  

        private static void CreateDataSet(DataSet dataSet, Type type, bool isChildTable)
        {
            DataTable dataTable = new DataTable(type.Name);
            //Create the ID columns for having relation in the tables  
            dataTable.Columns.Add(new DataColumn("ID2", typeof(int)));
            if (isChildTable)
            {
                dataTable.Columns.Add(new DataColumn("ParentID2", typeof(int)));
            }
            // Create the structure for the data tables to be  
            // added in the the data set  
            foreach (PropertyInfo pInfo in type.GetProperties())
            {
                if (pInfo.PropertyType.IsGenericType &&
                    (pInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                    || pInfo.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)))
                {

                    CreateDataSet(dataSet, pInfo.PropertyType.GetGenericArguments()[0], true);
                }
                else
                {
                    dataTable.Columns.Add(new DataColumn(pInfo.Name, pInfo.PropertyType));
                }
            }
            //Add the table to the dataset  
            dataSet.Tables.Add(dataTable);
        }
        /// Fill all the tables of data set with data in the respective list  
        /// Type of which datatable is to be filled  
        /// List of data  
        /// Data Set in which data tables will be filled with data  
        /// ID of parent record. If -1 one then no parent  

        private static void FillDataSet(Type type, IList<T> list, DataSet dataSet, int parentID)
        {
            PropertyInfo[] propertyInfos = type.GetProperties();
            DataTable dataTable = dataSet.Tables[type.Name];
            int id = dataTable.Rows.Count + 1;
            foreach (object item in list)
            {
                DataRow row = dataTable.NewRow();
                // Set new id and related parent id  
                row["Codigo"] = id;
                if (parentID != -1)
                    row["ParentID"] = parentID;
                // Load all the data from the properties of the type  
                // and save them into the datatable  
                foreach (PropertyInfo info in propertyInfos)
                {
                    if (info.PropertyType.IsGenericType &&
                        (info.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                        || info.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)))
                    {
                       // IList subList = (IList)info.GetValue(item, null);
                       // if (subList != null && subList.Count > 0)
                       // {
                       //     FillDataSet(subList[0].GetType(), subList, dataSet, id);
                       // }
                    }
                    else
                    {
                        row[info.Name] = info.GetValue(item, null);
                    }
                }
                dataTable.Rows.Add(row);
                id++;
            }
        }

        /// Creates the relation between the tables according to the  
        /// type and parent table on field ID and ParentID  
        /// Data set containing parent and child table  
        /// Type of the list  
        /// Parent table to which relations has to be done  

        private static void CreateRelations(DataSet dataSet, Type type, DataTable parentTable)
        {
            DataTable dataTable = dataSet.Tables[type.Name];
            // If parent table exsits then create relation  
            // with child table on field Parent ID  

            if (parentTable != null)
            {

                dataSet.Relations.Add(

                new DataRelation(parentTable.TableName + "_ID_"

                + "PARENTID_" + dataTable.TableName,

                parentTable.Columns["ID"],

               dataTable.Columns["ParentID"]));

            }

            // Check for other lists under current object  
            // go for another relation if exists  

            foreach (PropertyInfo pInfo in type.GetProperties())
            {
                if (pInfo.PropertyType.IsGenericType &&

                (pInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                || pInfo.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)))
                {
                     // If associate lists are there make then another table  
                    CreateRelations(dataSet, pInfo.PropertyType.GetGenericArguments()[0], dataTable);

                }
            }
        }
    }
}
