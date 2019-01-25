using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace LINQ
{
    public class Person
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public decimal Salary { get; set; }
        public string Location { get; set; }
        public int DepartmentID { get; set; }
    }

    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }

    class Program
    {
        public static ArrayList List = new ArrayList() { 101, "Navjyot", "Gurhale", 25, 202.30 };

        public static List<Department> Department = new List<Department>()
        {
            new Department{ DepartmentID=1, DepartmentName="Software" },
            new Department{ DepartmentID=2, DepartmentName="BDE" },
            new Department{ DepartmentID=3, DepartmentName="Manager" }
        };

        public static List<Person> Employee = new List<Person>() 
        { 
            new Person { PersonID=101, FirstName ="Navjyot", LastName = "Gurhale", Age=24, Location="Latur", Salary=3000, DepartmentID=1 },
            new Person { PersonID=102, FirstName ="Netaji", LastName = "Jadhav", Age=30, Location="Latur", Salary=3500, DepartmentID=2 },
            new Person { PersonID=103, FirstName ="Shekhar", LastName = "Kashid", Age=27, Location="Baramati", Salary=3800, DepartmentID=2 },
            new Person { PersonID=104, FirstName ="Nitin", LastName = "Kulkarni", Age=26, Location="Parbhani", Salary=2800, DepartmentID=3 },
            new Person { PersonID=104, FirstName ="Ankit", LastName = "Rai", Age=30, Location="Pune", Salary=4000, DepartmentID=1 }
        };

        public static void DisplayData(List<Person> Employee)
        {
            foreach (Person p1 in Employee)
            {
                Console.WriteLine("ID : " + p1.PersonID);
                Console.WriteLine("Name : " + p1.FirstName + " " + p1.LastName);
                Console.WriteLine("Age : " + p1.Age);
                Console.WriteLine("Location : " + p1.Location);
                Console.WriteLine("Salary : " + p1.Salary);
                Console.WriteLine();
            }
        }

        public static void DisplayArrayListData(IEnumerable data)
        {
            foreach (var d in data)
            {
                Console.WriteLine(d.ToString());
            }
        }

        static void Main(string[] args)
        {
            //Query Syntax
            var ListData_Query = from person in Employee where person.Location.Contains("Latur") select person;
            //Method Syntax
            var ListData_Method = Employee.Where(P => P.Salary > 2000 && P.Salary <= 3500);

            
            
            //1. Where
            //Actual Implimentation
            Func<Person, bool> Predicate = delegate(Person P1)
            {
                return P1.Age > 25 && P1.Age < 30;
            };
            var filterData = from p in Employee where Predicate(p) select p;
            //Query
            var filterData_Query = from p in Employee where p.Age > 25 && p.Age < 30 select p;
            //Method
            var filterData_Method = Employee.Where(p => p.Age > 25 && p.Age < 30);
            //multiple where 
            //Query
            var filterData_Query_Multiple = from p in Employee where p.Age>25 where p.Age<30 select p;
            //Method
            var filterData_Method_Multiple = Employee.Where(p => p.Age > 25).Where(p=>p.Age<30);

            
            
            //2. OfType
            var OfType_Query = from L in List.OfType<double>() select L;
            var OfType_Method = List.OfType<string>();



            //3. OrderBy, OrderByDescending 
            //4. ThenBy, ThenByDescending (Only present in Method)
            //var OrderBy_Query = from p in Employee orderby p.PersonID select p;
            //var OrderBy_Method= Employee.OrderBy(p=>p.PersonID);
            //var OrderBy_Query = from p in Employee orderby p.PersonID descending select p;
            //var OrderBy_Method = Employee.OrderByDescending(p => p.PersonID);
            //Muliple Sorting
            var OrderBy_Query = from p in Employee orderby p.Location, p.PersonID select p;
            var OrderBy_Method = Employee.OrderByDescending(p => p.PersonID).ThenBy(p => p.Salary).ThenByDescending(p => p.FirstName);



            //5. GroupBy, ToLookUp
            var GroupBy_Query = from p in Employee group p by p.Age;
            var GroupBy_Method = Employee.GroupBy(p => p.Age);
            var ToLookUp_Method = Employee.ToLookup(p => p.Age); //(ToLookup is only applicable in Method )
            //foreach (var p1 in ToLookUp_Method)
            //{
            //    Console.WriteLine("Age Group : " + p1.Key);
            //    foreach (Person p in p1)
            //        Console.WriteLine("Name : " + p.FirstName + "" + p.LastName);
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

            //6. Join, GroupJoin
            var JoinResult_Query = from emp in Employee join dept in Department on emp.DepartmentID equals dept.DepartmentID select new { EmpName = emp.FirstName + "" + emp.LastName, DeptName = dept.DepartmentName };
            foreach (var p in JoinResult_Query)
                Console.WriteLine(p.EmpName + " (" + p.DeptName + ")");


            //Display List
            //DisplayData(JoinResult_Query.ToList()); // Put all above var data to check 

            //Display ArrayList
            //DisplayArrayListData(GroupBy_Query); // Put all var data above to check
            
            Console.ReadKey();
        }
    }
}
/*http://www.tutorialsteacher.com/linq/linq-tutorials
 * 
 * What is LINQ ?
 * LINQ (Language Integrated Query) is uniform query syntax in C# and VB.NET to retrieve data from different sources and formats.
 * SQL is a Structured Query Language used to save and retrieve data from a database.
 * In the same way, LINQ is a structured query syntax built in C# and VB.NET to retrieve data from different types of data sources such as collections, ADO.Net DataSet, XML Docs, web service and MS SQL Server and other databases.
 * LINQ queries return results as objects.
 * It enables you to uses object-oriented approach on the result set and not to worry about transforming diffent formats of results into objects.
 * 
 * 
 * 
 * Why LINQ ?
 * Familiar language
 * Less coding
 * Readable code
 * Standardized way of querying multiple data sources
 * Compile time safety of queries
 * IntelliSense Support
 * Shaping data
 * 
 * 
 * 
 * LINQ API
 * We can write LINQ queries for the classes that implement IEnumerable<T> or IQueryable<T> interface. 
 * The System.Linq namespace includes the following classes and interfaces require for LINQ queries.
 * 
 * 
 * 
 * Use System.Linq namespace to use LINQ.
 * LINQ api includes two main static class Enumerable & Queryable.
 * The static Enumerable class includes extension methods for classes that implements the IEnumerable<T> interface.
 * IEnumerable<T> type of collections are in-memory collection like List, Dictionary, SortedList, Queue, HashSet, LinkedList.
 * The static Queryable class includes extension methods for classes that implements the IQueryable<T> interface.
 * Remote query provider implements e.g. Linq-to-SQL, LINQ-to-Amazon etc.
 * 
 * 
 * 
 * LINQ Qery Syntax :
 * As name suggest, Query Syntax is same like SQL (Structure Query Language) syntax.
 * Query Syntax starts with from clause and can be end with Select or GroupBy clause.
 * Use various other opertors like filtering, joining, grouping, sorting operators to construct the desired result.
 * Implicitly typed variable - var can be used to hold the result of the LINQ query.
 * 
 * 
 * 
 * LINQ Method Syntax :
 * The compiler converts query syntax into method syntax at compile time.
 * Example : var teenAgerStudents = studentList.Where(s => s.Age > 12 && s.Age < 20).ToList<Student>();
 * As name suggest, Method Syntax is like calling extension method.
 * LINQ Method Syntax aka Fluent syntax because it allows series of extension methods call.
 * Implicitly typed variable - var can be used to hold the result of the LINQ query.
 * 
 * Lambda Expression 
 * Lambda Expression is a shorter way of representing anonymous method.
 * Lambda Expression syntax: parameters => body expression
 * Lambda Expression can have zero parameter.
 * Lambda Expression can have multiple parameters in parenthesis ().
 * Lambda Expression can have multiple statements in body expression in curly brackets {}.
 * Lambda Expression can be assigned to Func, Action or Predicate delegate.
 * Lambda Expression can be invoked in a similar way to delegate.
 * 
 * _____________________________________________________
 * Classification	(Standard Query Operators)
 * _____________________________________________________
 * Filtering	(Where, OfType)
 * Sorting	(OrderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse)
 * Grouping	(GroupBy, ToLookup)
 * Join	(GroupJoin, Join)
 * Projection	(Select, SelectMany)
 * Aggregation	(Aggregate, Average, Count, LongCount, Max, Min, Sum)
 * Quantifiers	(All, Any, Contains)
 * Elements	(ElementAt, ElementAtOrDefault, First, FirstOrDefault, Last, LastOrDefault, Single, SingleOrDefault)
 * Set	(Distinct, Except, Intersect, Union)
 * Partitioning	(Skip, SkipWhile, Take, TakeWhile)
 * Concatenation	(Concat)
 * Equality	(SequenceEqual)
 * Generation	(DefaultEmpty, Empty, Range, Repeat)
 * Conversion	(AsEnumerable, AsQueryable, Cast, ToArray, ToDictionary, ToList)
 * ___________________________________________________
 * 
 * 1. Where :-
 * Where is used for filtering the collection based on given criteria.
 * Where extension method has two overload methods. Use a second overload method to know the index of current element in the collection.
 * Method Syntax requires the whole lambda expression in Where extension method whereas Query syntax requires only expression body.
 * Multiple Where extension methods are valid in a single LINQ query.
 * 
 * 
 * 
 * 2. OfType :- 
 * The Where operator filters the collection based on a predicate function.
 * The OfType operator filters the collection based on a given type
 * Where and OfType extension methods can be called multiple times in a single LINQ query.
 * 
 * 
 * 
 * 3. OrderBy :- 
 * LINQ includes five sorting operators: OrderBy, OrderByDescending, ThenBy, ThenByDescending and Reverse
 * LINQ query syntax does not support OrderByDescending, ThenBy, ThenByDescending and Reverse. 
 * It only supports 'Order By' clause with 'ascending' and 'descending' sorting direction.
 * LINQ query syntax supports multiple sorting fields seperated by comma whereas you have to use ThenBy & ThenByDescending methods for secondary sorting.
 * ThenBy,ThenByDescending, Reverse (Only valid in method syntax)
 * 
 * 
 * 
 * 4. ThenBy :- 
 * OrderBy and ThenBy sorts collections in ascending order by default.
 * ThenBy or ThenByDescending is used for second level sorting in method syntax.
 * ThenByDescending method sorts the collection in decending order on another field.
 * ThenBy or ThenByDescending is NOT applicable in Query syntax.
 * Apply secondary sorting in query syntax by separating fields using comma.
 * 
 * 
 * 
 * 5. GroupBy, ToLookUp
 * GroupBy & ToLookup return a collection that has a key and an inner collection based on a key field value.
 * The execution of GroupBy is deferred whereas that of ToLookup is immediate.
 * A LINQ query syntax can be end with the GroupBy or Select clause.
 * 
 * 
 * 6. Join
 * Join and GroupJoin are joining operators.
 * Join is like inner join of SQL. It returns a new collection that contains common elements from two collections whosh keys matches.
 * Join operates on two sequences inner sequence and outer sequence and produces a result sequence.
 * Join query syntax:
   from... in outerSequence
   join... in innerSequence 
   on  outerKey equals innerKey
   select ...
 * 
 * 
 * 
 * 7. GroupJoin
 * 8. Select
 * 9. All, Any
 * 10. Contains
 * 11. Aggregate
 * 12. Average
 * 13. Count
 * 14. Max
 * 15. Sum
 * 16. ElementAt, ElementAtOrDefault
 * 17. First, FirstOrDefault
 * 18. Last, LastOrDefault
 * 19. Single, SingleOrDefault
 * 20. SequenceEqual
 * 21. Concat
 * 22. DefaultIfEmpty
 * 23. Empty, Range, Repeat
 * 24. Distinct
 * 25. Expect 
 * 26. Intersect
 * 27. Union
 * 28. Skip, SkipWhile
 * 29. Take, TakeWhile
 * 30. Conversion Operators
 * 
 */