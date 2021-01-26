
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp2;

namespace cw9_forms
{
    public class LinqSamples
    {
        public IEnumerable<Emp> Emps { get; set; }
        public IEnumerable<Dept> Depts { get; set; }
        public Form1 MyForm { get; set; }
        public LinqSamples()
        {
            LoadData();
        }
                
        

        public LinqSamples(Form1 form1)
        {
            this.MyForm = form1;
            LoadData();
        }
        public void LoadData()
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
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
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
                Ename = "Władysław Torzewski",
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


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
         *  Rezultat zapytania powinien zostać wyświetlony za pomocą kontrolki DataGrid.
         *  W tym celu końcowy wynik należy rzutować do Listy (metoda ToList()).
         *  Jeśli dane zapytanie zwraca pojedynczy wynik możemy je wyświetlić w kontrolce
         *  TextBox WynikTextBox.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Przyklad1()
        {
            //var res = new List<Emp>();
            //foreach(var emp in Emps)
            //{
            //    if (emp.Job == "Backend programmer") res.Add(emp);
            //}

            //1. Query syntax (SQL)
            var res = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Zawod = emp.Job
                      };

            MyForm.setData(res.ToList());
            //2. Lambda and Extension methods
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Przyklad2()
        {
            var res = from emp in Emps
                      join dept in Depts on emp.Deptno equals dept.Deptno
                      where emp.Job == "Frontend programmer" && emp.Salary > 1000
                      orderby emp.Ename descending
                      select emp;

            var res2 = Emps.Where(em => em.Job == "Frontend programmer" && em.Salary > 1000).OrderByDescending(emp => emp.Ename)
                .Select(en => new
            {
                en.Ename,
                en.Salary
            }) ;

            MyForm.setData(res2.ToList());
        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Przyklad3()
        {
            var max = Emps.Max(emp => emp.Salary);
            var min = Emps.Min(emp => emp.Salary);
            var avg = Emps.Average(emp => emp.Salary);
            var groupBy = Emps.GroupBy(emp => emp.Deptno);

            var res = Emps.Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new
            {
                emp,dept
            });

            
            MyForm.setData(max.ToString());
        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public void Przyklad4()
        {
            var res = from emp in Emps
                      where emp.Salary == Emps.Max(emplo => emplo.Salary)
                      select emp;
            MyForm.setData(res.ToList());
        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public void Przyklad5()
        {
            var res = from emp in Emps
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Praca = emp.Job
                      };

            MyForm.setData(res.ToList());
        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public void Przyklad6()
        {
            var res = from emp in Emps
                      join dept in Depts on emp.Deptno equals dept.Deptno
                      select new
                      {
                          emp.Ename,
                          emp.Job,
                          dept.Dname
                      };

            MyForm.setData(res.ToList());
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public void Przyklad7()
        {
            var res = Emps.GroupBy(emp => new { emp.Job })
                     .Select(e => new
                     {
                         Praca = e.Key.Job,
                         Count = e.Count()
                     });

            MyForm.setData(res.ToList());
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public void Przyklad8()
        {
            var res = Emps.FirstOrDefault(emp => emp.Job == "Backend programmer");
            if (res != null)
            {
               MyForm.setData("TRUE");
            }
            else
            {
                MyForm.setData("FALSE");               
            }
        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Przyklad9()
        {

            var res = (from emp in Emps
                          where emp.Job == "Frontend programmer"
                          orderby emp.HireDate descending
                          select emp).Take(1);

            MyForm.setData(res.ToList());
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public void Przyklad10()
        {
            var res1 = (new[] { new
            {
                Ename = "Brak wartosci",
                Job = (string)null,
                HireDate = (DateTime?)null
            }
            }).ToList();

            var res2 = Emps.Select(emp => new
            {
                emp.Ename,
                emp.Job,
                emp.HireDate
            }).Union(res1);

            MyForm.setData(res2.ToList());
        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public void Przyklad11()
        {
             var res1 = Emps.Aggregate((emp1,emp2) => (emp1.Salary > emp2.Salary) ? emp1:emp2);//wyswietli obiekt

            var res2 = Emps.Aggregate(0, (maxSal, next) => (next.Salary >= maxSal ? next.Salary : maxSal));
         
            
            MyForm.setData(res2.ToString());
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public void Przyklad12()
        {
            var res = from emp in Emps
                        from dept in Depts
                        select new
                        {
                            emp.Ename,
                            dept.Deptno

                        };

            MyForm.setData(res.ToList());
        }
        public void Przyklad13()
        {
            //Dept
            var res = Depts.OrderBy(d => d.Deptno);

            MyForm.setData(res.ToList());
        }

        public void Przyklad14()
        {
            //Emp
            var res = Emps.OrderBy(e => e.Empno);

            MyForm.setData(res.ToList());
                  
            
        }
    }
}
