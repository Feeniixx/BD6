using LinqTutorials.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqTutorials
{
    public static class LinqTasks
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        static LinqTasks()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts

            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Pawe?? Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Micha?? Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "W??adys??aw Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public static IEnumerable<Emp> Task1()
        {
          IEnumerable<Emp> result = Emps.Where(e => e.Job == "Backend programmer");
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public static IEnumerable<Emp> Task2()
        {
            IEnumerable<Emp> result = Emps.Where(e => e.Job == "Frontend programmer" && e.Salary > 1000).OrderByDescending(e => e.Ename);

               return result;
        }


        /// <summary>
        ///     SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public static int Task3()
        {
             var result = Emps.Max(e => e.Salary);
              
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public static IEnumerable<Emp> Task4()
        {
            IEnumerable<Emp> result = Emps.Where(e => e.Salary == Emps.Max(e1 => e1.Salary));
            
            return result;
        }

        /// <summary>
        ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public static IEnumerable<object> Task5()
        {
            IEnumerable<object> result = Emps.Select(e => new { Ename = e.Ename, Job = e.Job });
            
            return result;
        }

        /// <summary>
        ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        ///     Rezultat: Z????czenie kolekcji Emps i Depts.
        /// </summary>
        public static IEnumerable<object> Task6()
        {
            IEnumerable<object> result = Emps.Join(Depts, e => e.Deptno, d => d.Deptno, (e, d) => new { Ename = e.Ename, Job = e.Job, Dname = d.Dname });
            return result;
        }

        /// <summary>
        ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public static IEnumerable<object> Task7()
        {
            IEnumerable<object> result = Emps.GroupBy(e => e.Job).Select(e => new
            {
                Praca = e.Key,
                LiczbaPracownikow = e.Count()
            });

            return result;
        }

        /// <summary>
        ///     Zwr???? warto???? "true" je??li cho?? jeden
        ///     z element??w kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public static bool Task8()
        {
            bool result = false;
            if (Emps.Any(e => e.Job.Equals("Backend programmer"))){
                return true;
            }
            else 
            return result;
        }

        /// <summary>
        ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        ///     ORDER BY HireDate DESC;
        /// </summary>
        public static Emp Task9()
        {
            Emp result = Emps.Where(e => e.Job == "Frontend programmer").OrderByDescending(e => e.HireDate).First(); 
            return result;
        }

        /// <summary>
        ///     SELECT Ename, Job, Hiredate FROM Emps
        ///     UNION
        ///     SELECT "Brak warto??ci", null, null;
        /// </summary>
        public static IEnumerable<object> Task10()
        {
            IEnumerable<object> result = Emps.Select(e => new
            {
                Ename = e.Ename,
                Job = e.Job,
                Date = e.HireDate

            }).Union(Emps.Select(e => new
            {
                Ename = "Brak warto??ci",
                Job = (String)null,
                Date = (DateTime?)null

            }));
            return result;
        }

        /// <summary>
        /// Wykorzystuj??c LINQ pobierz pracownik??w podzielony na departamenty pami??taj??c, ??e:
        /// 1. Interesuj?? nas tylko departamenty z liczb?? pracownik??w powy??ej 1
        /// 2. Chcemy zwr??ci?? list?? obiekt??w o nast??puj??cej srukturze:
        ///    [
        ///      {name: "RESEARCH", numOfEmployees: 3},
        ///      {name: "SALES", numOfEmployees: 5},
        ///      ...
        ///    ]
        /// 3. Wykorzystaj typy anonimowe
        /// </summary>
        public static IEnumerable<object> Task11()
        {
            IEnumerable<object> result = Emps.GroupBy(e => e.Job).Select(e => new
            {
                name = e.Key,
                numOfEmployees = e.Count()
            }).Where(e => e.numOfEmployees > 1);
            return result;
        }

        /// <summary>
        /// Napisz w??asn?? metod?? rozszerze??, kt??ra pozwoli skompilowa?? si?? poni??szemu fragmentowi kodu.
        /// Metod?? dodaj do klasy CustomExtensionMethods, kt??ra zdefiniowana jest poni??ej.
        /// 
        /// Metoda powinna zwr??ci?? tylko tych pracownik??w, kt??rzy maj?? min. 1 bezpo??redniego podw??adnego.
        /// Pracownicy powinny w ramach kolekcji by?? posortowani po nazwisku (rosn??co) i pensji (malej??co).
        /// </summary>
        public static IEnumerable<Emp> Task12()
        {
            
                IEnumerable<Emp> result = CustomExtensionMethods.GetEmpsWithSubordinates(Emps);
                return result;
        }

        /// <summary>
        /// Poni??sza metoda powinna zwraca?? pojedyczn?? liczb?? int.
        /// Na wej??ciu przyjmujemy list?? liczb ca??kowitych.
        /// Spr??buj z pomoc?? LINQ'a odnale???? t?? liczb??, kt??re wyst??puja w tablicy int'??w nieparzyst?? liczb?? razy.
        /// Zak??adamy, ??e zawsze b??dzie jedna taka liczba.
        /// Np: {1,1,1,1,1,1,10,1,1,1,1} => 10
        /// </summary>
        public static int Task13(int[] arr)
        {
            int result=arr.GroupBy(e => e).Where(e1 => e1.Count() % 2 != 0).Select(e1 => e1.Key).First();
            
            return result;
        }

        /// <summary>
        /// Zwr???? tylko te departamenty, kt??re maj?? 5 pracownik??w lub nie maj?? pracownik??w w og??le.
        /// Posortuj rezultat po nazwie departament rosn??co.
        /// </summary>
        public static IEnumerable<Dept> Task14()
        {
            IEnumerable<Dept> result = null;
            //result =
            return result;
        }
    }

    public static class CustomExtensionMethods
    {
        //Put your extension methods here
        public static IEnumerable<Emp> GetEmpsWithSubordinates(this IEnumerable<Emp> emps)
       {
           
        IEnumerable<Emp> result = emps.Where(e => e.Mgr != null).OrderBy(e => e.Ename).ThenByDescending(e => e.Salary);
           return result;
        }

    }
}
 
