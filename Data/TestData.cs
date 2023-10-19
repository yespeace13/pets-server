using PetsServer.Authorization.Model;
using PetsServer.Context;
using PetsServer.Locality.Controller;
using PetsServer.Locality.Model;
using PetsServer.Locality.Repository;
using PetsServer.Organization.Model;
using PetsServer.Organization.Service;

namespace IS_5
{
    public static class TestData
    {
        public static List<UserModel> Users { get; set; }
        public static List<Role> Roles { get; set; }

        //public static List<Act> Acts { get; set; }
        //public static List<Contract> Contracts { get; set; }

        //public static List<Plan> Plans { get; set; }
        //public static List<List<ContentPlan>> ContentPlans { get; set; }

        static TestData()
        {
            CreateRoles();
            CreateUsers();
            //CreateContracts();
            //CreateContentPlans();
            //CreatePlans();
            //CreateActs();
        }

        //private static void CreateActs()
        //{
        //    var animals = new List<Animal>
        //    {
        //        new Animal(1, "category", true, "breed", 45, "wool", null, null, null, null, "fdfds", "125623", Localitys[0], null)
        //    };

        //    //Acts = new List<Act>
        //    //{
        //    //    new Act(1, animals, Organizations[5], new DateTime(2022,11,28), null)
        //    //};
        //    //Contracts[0].Acts.Add(Acts[0]);
        //}

        //private static void CreateContracts()
        //{
        //    //Contracts = new List<Contract>
        //    //{
        //    //    new Contract(1, "1234", DateTime.Now, new DateTime(2023, 12, 31), Organizations[1], Organizations[2],
        //    //        new List<Localityprice>()
        //    //        {
        //    //            new Localityprice(1, Localitys[0], 5000),
        //    //            new Localityprice(2, Localitys[1], 25000),
        //    //            new Localityprice(3, Localitys[2], 10000)
        //    //        },
        //    //        new List<Scan>()
        //    //        {
        //    //            new Scan(1, "C:\\Users\\1\\Desktop\\img.jpg"),
        //    //            new Scan(2, "C:\\Users\\1\\Desktop\\-с-руководителем-1-ba6d559b.png")
        //    //        }),
        //    //    new Contract(2, "5468864", new DateTime(2022, 12, 31), new DateTime(2023, 12, 31), Organizations[7], Organizations[5],
        //    //        new List<Localityprice>()
        //    //        {
        //    //            new Localityprice(1, Localitys[0], 5000),
        //    //            new Localityprice(2, Localitys[2], 25000),
        //    //            new Localityprice(3, Localitys[1], 10000)
        //    //        },
        //    //        new List<Scan>()
        //    //        {
        //    //            new Scan(1, "C:\\Users\\1\\Desktop\\img.jpg"),
        //    //            new Scan(2, "C:\\Users\\1\\Desktop\\-с-руководителем-1-ba6d559b.png")
        //    //        })
        //    //};
        //}

        private static void CreateRoles()
        {
            Roles = new List<Role>
            {
                {
                    new Role(1, "Куратор ВетСлужбы",
                        new Tuple<Restrictions, Possibilities[]>(
                            //Что будет отображать датагрид
                            Restrictions.All,
                            //Что он может делать на форме
                            new Possibilities[] {Possibilities.Insert, Possibilities.Update, Possibilities.Delete}),
                        new Tuple<Restrictions, Possibilities[]>(Restrictions.All, null),
                        new Tuple<Restrictions, Possibilities[], int[]>(Restrictions.Locality, new Possibilities[] { Possibilities.Read, Possibilities.Update, Possibilities.Delete }, null),
                        new Tuple<Restrictions, Possibilities[]>(Restrictions.All, null))
                },
                {
                    new Role(2, "Оператор ВетСлужбы",
                        new Tuple<Restrictions, Possibilities[]>(Restrictions.All,
                            new Possibilities[] {Possibilities.Insert, Possibilities.Update, Possibilities.Delete}),
                        new Tuple<Restrictions, Possibilities[]>(Restrictions.All, null),
                        new Tuple<Restrictions, Possibilities[], int[]>(Restrictions.All,
                            new Possibilities[]
                            {
                                Possibilities.Insert, Possibilities.Delete, Possibilities.Update
                            },
                            new int[]{ 2, 3, 7}),
                        new Tuple<Restrictions, Possibilities[]>(
                            Restrictions.All, null))
                },
                //{
                //    new Role(3, "Оператор ОМСУ",
                //        new Tuple<Restrictions, Possibilities[]>(Restrictions.Locality,
                //            new Possibilities[] { Possibiliti }),
                //        new Tuple<Restrictions, Possibilities[]>(Restrictions.Locality, null),
                //        new Tuple<Restrictions, Possibilities[], int[]>(Restrictions.Locality,
                //            new Possibilities[]
                //            {
                //                Possibilities.Add, Possibilities.Delete, Possibilities.Change,
                //                Possibilities.AddFile, Possibilities.DelFile
                //            },
                //            new int[]{ 4, 5, 6, 7, 9, 10, 11}),
                //        new Tuple<Restrictions, Possibilities[]>(Restrictions.Locality,
                //            new Possibilities[]
                //            {
                //                Possibilities.Add, Possibilities.Change, Possibilities.Delete,
                //                Possibilities.AddFile, Possibilities.DelFile
                //            }))
                //},
                //{
                //    new Role(4, "Оператор по отлову",
                //        new Tuple<Restrictions, Possibilities[]>(Restrictions.Locality,
                //            new Possibilities[] { Possibilities.OpenAndEdit, Possibilities.Add, Possibilities.Delete }),
                //        new Tuple<Restrictions, Possibilities[]>(Restrictions.Locality, new Possibilities[]
                //        {
                //            Possibilities.Add, Possibilities.Delete, Possibilities.Change,
                //                Possibilities.AddFile, Possibilities.DelFile
                //            }),
                //        new Tuple<Restrictions, Possibilities[], int[]>(Restrictions.None, null, null),
                //        new Tuple<Restrictions, Possibilities[]>(Restrictions.None, null))
                //}

            };
        }

        private static void CreateUsers()
        {
            var locality = new LocalityRepository();
            var organization = new OrganizationService();
            Users = new List<UserModel>
            {
                { new UserModel(1, "User1", "1234", locality.GetOne(1), organization.GetOne(0), Roles[0]) },
                { new UserModel(2, "User2", "1234", null, null, Roles[1])},
                { new UserModel(3, "User3", "1234", locality.GetOne(2), null, Roles[1])}
                //{ new User(4, "User4", "1234", null, Organizations[5], Roles[3])}
            };
        }

        //private static void CreateContentPlans()
        //{
        //    ContentPlans = new List<List<ContentPlan>>
        //    {
        //        new List<ContentPlan>
        //        {
        //            {new ContentPlan(1, Localitys[0], "Республики 120",false)},
        //            {new ContentPlan(2, Localitys[0],"Республики 160",true)}
        //        },
        //        new List<ContentPlan>
        //        {
        //            {new ContentPlan(1, Localitys[0], "Ленина 120",true)},
        //            {new ContentPlan(2, Localitys[0],"Ленина 160", false)}
        //        }
        //    };
        //}

        //private static void CreatePlans()
        //{

        //    Plans = new List<Plan>
        //    {
        //        {new Plan(1,2,2023,ContentPlans[0])},
        //        {new Plan(3,4,2022,ContentPlans[0])},
        //        {new Plan(2,3,2023,ContentPlans[1])}
        //    };
        //}
    }
}
