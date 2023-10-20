//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using IS_5.Model;
//using Moq;
//using NUnit;
//using NUnit.Framework;

//namespace IS_5.Test
//{
//    internal class TestLocalityRepository : LocalityRepository
//    {
//        public List<Locality> Localitys { get; set; }
//        public new List<Locality> GetLocalitys()
//        {
//            return Localitys;
//        }
//    }

//    internal class TestActRepository : ActRepository
//    {
//        public List<Act> Acts { get; set; }
//        public new List<Act> GetActs()
//        {
//            return Acts;
//        }
//    }
    
//    internal class TestContractRepository : ContractRepository
//    {
//        public List<Contract> Contracts { get; set; }
//        public new List<Contract> GetContracts()
//        {
//            return Contracts;
//        }
//    }
//    [TestFixture]
//    internal class TestReportService : ReportService
//    {
//        private TestLocalityRepository _localityRepository;
//        private TestActRepository _actRepository;
//        private TestContractRepository _contractRepository;
//        private ReportService _reportService;
        
//        [SetUp]
//        public void Setup()
//        {
//            _localityRepository = new TestLocalityRepository();
//            _actRepository = new TestActRepository();
//            _contractRepository = new TestContractRepository();
//            _reportService = new ReportService();
//        }
//        [Test]
//        public void GetReport()
//        {
//            _localityRepository.Localitys = new List<Locality>
//            {
//                new Locality(1, "Абатский муниципальный район")
//            };
//            _contractRepository.Contracts = new List<Contract>
//            {
//                new Contract(1, "12434", new DateTime(23,6,10), new DateTime(24, 6, 10),
//                    new OrganizationsRepository().GetOrganization(1),
//                    new OrganizationsRepository().GetOrganization(2),
//                    new List<Localityprice> {new Localityprice(1, _localityRepository.Localitys[0], 1000)}, null)
//            };
//            var animals = new List<Animal>
//            {
//                new Animal(1, null, true, null, 0, null, null, null, null, null, null, null, _localityRepository.Localitys[0], null)
//            };
//            _actRepository.Acts = new List<Act>
//            {
//                new Act(1, animals, new OrganizationsRepository().GetOrganization(1), new DateTime(23, 6, 11), null)
//            };
//            _contractRepository.Contracts[0].Acts.Add(_actRepository.Acts[0]);
//            _reportService.Localityes = _localityRepository.Localitys;
//            _reportService.Contracts = _contractRepository.Contracts;

//            var trueReport = new List<string[]> { new string[]{ "Абатский муниципальный район", "1", "1000" } };
//            var from = new DateTime(23, 5, 1);
//            var to = new DateTime(23, 6, 11);
//            var report = _reportService.CreateReport(from, to);

            
//            for (int i = 0; i < report.Count; i++)
//                for (int j = 0; j < report[0].Length; j++)
//                    Assert.AreEqual(trueReport[i][j], report[i][j]);
           
//        }
//    }
//}
