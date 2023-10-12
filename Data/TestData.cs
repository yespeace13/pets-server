using IS_5.Organization.Model;
using SupportLibrary.Model.Locality;
using SupportLibrary.Model.Organization;

namespace IS_5
{
    public static class TestData
    {
        public static List<OrganizationModel> OrganizationsModel { get; set; }
        public static List<TypeOrganizationModel> TypeOrganizationsModel { get; set; }
        public static List<LegalTypeModel> LegalTypesModel { get; set; }
        //public static List<User> Users { get; set; }
        public static List<LocalityModel> Localitys { get; set; }
        //public static List<Role> Roles { get; set; }

        //public static List<Act> Acts { get; set; }
        //public static List<Contract> Contracts { get; set; }

        //public static List<Plan> Plans { get; set; }
        //public static List<List<ContentPlan>> ContentPlans { get; set; }

        static TestData()
        {
            CreateTypeOrganizations();
            CreateTypeOwnerOrganizations();
            CreateLocalitys();
            CreateOrganizations();
            //CreateRoles();
            //CreateUsers();
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

        private static void CreateLocalitys()
        {
            Localitys = new List<LocalityModel>
            {
                { new LocalityModel(1, "Тюменский муниципальный район") },
                { new LocalityModel(2, "Сорокинский муниципальный район") },
                { new LocalityModel(3, "Ялуторовский муниципальный район") }
            };
        }
        //private static void CreateRoles()
        //{
        //    Roles = new List<Role>
        //    {
        //        {
        //            new Role(1, "Куратор ВетСлужбы",
        //                new Tuple<Restrictions, Possibilities[]>(
        //                    //Что будет отображать датагрид
        //                    Restrictions.All,
        //                    //Что он может делать на форме
        //                    new Possibilities[] {Possibilities.OpenAndEdit}),
        //                new Tuple<Restrictions, Possibilities[]>(Restrictions.All, null),
        //                new Tuple<Restrictions, Possibilities[], int[]>(Restrictions.All, null, null),
        //                new Tuple<Restrictions, Possibilities[]>(Restrictions.All, null))
        //        },
        //        {
        //            new Role(2, "Оператор ВетСлужбы",
        //                new Tuple<Restrictions, Possibilities[]>(Restrictions.All,
        //                    new Possibilities[] {Possibilities.OpenAndEdit}),
        //                new Tuple<Restrictions, Possibilities[]>(Restrictions.All, null),
        //                new Tuple<Restrictions, Possibilities[], int[]>(Restrictions.All,
        //                    new Possibilities[]
        //                    {
        //                        Possibilities.Add, Possibilities.Delete, Possibilities.Change,
        //                        Possibilities.AddFile, Possibilities.DelFile
        //                    },
        //                    new int[]{ 2, 3, 7}),
        //                new Tuple<Restrictions, Possibilities[]>(
        //                    Restrictions.All, null))
        //        },
        //        {
        //            new Role(3, "Оператор ОМСУ",
        //                new Tuple<Restrictions, Possibilities[]>(Restrictions.Locality,
        //                    new Possibilities[] { Possibilities.OpenAndEdit }),
        //                new Tuple<Restrictions, Possibilities[]>(Restrictions.Locality, null),
        //                new Tuple<Restrictions, Possibilities[], int[]>(Restrictions.Locality,
        //                    new Possibilities[]
        //                    {
        //                        Possibilities.Add, Possibilities.Delete, Possibilities.Change,
        //                        Possibilities.AddFile, Possibilities.DelFile
        //                    },
        //                    new int[]{ 4, 5, 6, 7, 9, 10, 11}),
        //                new Tuple<Restrictions, Possibilities[]>(Restrictions.Locality,
        //                    new Possibilities[]
        //                    {
        //                        Possibilities.Add, Possibilities.Change, Possibilities.Delete,
        //                        Possibilities.AddFile, Possibilities.DelFile
        //                    }))
        //        },
        //        {
        //            new Role(4, "Оператор по отлову",
        //                new Tuple<Restrictions, Possibilities[]>(Restrictions.Locality,
        //                    new Possibilities[] { Possibilities.OpenAndEdit, Possibilities.Add, Possibilities.Delete }),
        //                new Tuple<Restrictions, Possibilities[]>(Restrictions.Locality, new Possibilities[]
        //                {
        //                    Possibilities.Add, Possibilities.Delete, Possibilities.Change,
        //                        Possibilities.AddFile, Possibilities.DelFile
        //                    }),
        //                new Tuple<Restrictions, Possibilities[], int[]>(Restrictions.None, null, null),
        //                new Tuple<Restrictions, Possibilities[]>(Restrictions.None, null))
        //        }

        //    };
        //}

        //private static void CreateUsers()
        //{
        //    Users = new List<User>
        //    {
        //        { new User(1, "User1", "1234", Localitys[0], null, Roles[0]) },
        //        { new User(2, "User2", "1234", null, null, Roles[1])},
        //        { new User(3, "User3", "1234", Localitys[2], null, Roles[2])}
        //        //{ new User(4, "User4", "1234", null, Organizations[5], Roles[3])}
        //    };
        //}

        private static void CreateTypeOrganizations()
        {
            TypeOrganizationsModel = new List<TypeOrganizationModel>
            {
                { new TypeOrganizationModel(1, "Значения справочника")},
                { new TypeOrganizationModel(2, "Исполнительный орган государственной власти")},
                { new TypeOrganizationModel(3, "Орган местного самоуправления") },
                { new TypeOrganizationModel(4, "Организация по отлову") },
                { new TypeOrganizationModel(5, "Организация по отлову и приют") },
                { new TypeOrganizationModel(6, "Организация по транспортировке") },
                { new TypeOrganizationModel(7, "Ветеринарная клиника: государственная")},
                { new TypeOrganizationModel(8, "Ветеринарная клиника: муниципальная")},
                { new TypeOrganizationModel(9, "Ветеринарная клиника: частная")},
                { new TypeOrganizationModel(10, "Благотворительный фонд")},
                { new TypeOrganizationModel(11, "Организации по продаже товаров и предоставлению услуг для животных")},
                { new TypeOrganizationModel(12, "Заявитель (для регистрации представителя юридического лица, подающего заявку на отлов)")}
            };
        }

        private static void CreateOrganizations()
        {
            OrganizationsModel = new List<OrganizationModel>
            {
                { new OrganizationModel(1, "Организация1", "123", "123", "1234", TypeOrganizationsModel[1], LegalTypesModel[0], Localitys[1])},
                { new OrganizationModel(2, "Организация2", "123", "123", "1234", TypeOrganizationsModel[2], LegalTypesModel[1], Localitys[0]) },
                { new OrganizationModel(3, "Организация3", "123", "123", "1234", TypeOrganizationsModel[3], LegalTypesModel[1], Localitys[2]) },
                { new OrganizationModel(4, "Организация4", "123", "123", "1234", TypeOrganizationsModel[4], LegalTypesModel[0], Localitys[0]) },
                { new OrganizationModel(5, "Организация5", "123", "123", "1234", TypeOrganizationsModel[5], LegalTypesModel[0], Localitys[2]) },
                { new OrganizationModel(6, "Организация6", "123", "123", "1234", TypeOrganizationsModel[9], LegalTypesModel[1], Localitys[1]) },
                { new OrganizationModel(7, "Организация7", "123", "123", "1234", TypeOrganizationsModel[0], LegalTypesModel[0], Localitys[0]) },
                { new OrganizationModel(8, "Организация8", "123", "123", "1234", TypeOrganizationsModel[2], LegalTypesModel[1], Localitys[2]) },
                { new OrganizationModel(9, "Организация9", "123", "123", "1234", TypeOrganizationsModel[6], LegalTypesModel[1], Localitys[1]) },
                { new OrganizationModel(10, "Организация10", "123", "123", "1234", TypeOrganizationsModel[1], LegalTypesModel[0], Localitys[1]) },
                { new OrganizationModel(11,"Организация11", "123", "123", "1234", TypeOrganizationsModel[6], LegalTypesModel[1], Localitys[0]) },
                { new OrganizationModel(12, "Организация12", "123", "123", "1234", TypeOrganizationsModel[1], LegalTypesModel[0], Localitys[2]) }
            };
        }
        private static void CreateTypeOwnerOrganizations()
        {
            LegalTypesModel = new List<LegalTypeModel>
            {
                { new LegalTypeModel(1, "Индивидуальный предприниматель") },
                { new LegalTypeModel(2, "Юридическое лицо") }
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
