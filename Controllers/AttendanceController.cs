using Microsoft.AspNetCore.Mvc;
using ic_ASPnet.Models;
using ic_ASPnet.Utils;
using System.Text;

namespace ic_ASPnet.Controllers
{
    public class AttendanceController : Controller
    {
        private const string EmployeeCsv = "Data/employee.csv";
        private const string LogCsv = "Data/log.csv";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckIn(string inputUid)
        {
            string uid = FakeReader.GetCardUid(inputUid);
            string name = FindEmployeeName(uid);

            if (name == null)
            {
                ViewBag.Message = "未登録のカードです。";
                return View("Index");
            }

            string status = DetermineStatus(uid);
            SaveLog(name, uid, status);
            ViewBag.Message = $"{name} さんが {status} しました。";
            return View("Index");
        }

        private string FindEmployeeName(string uid)
        {
            if (!System.IO.File.Exists(EmployeeCsv)) return null;
            foreach (var line in System.IO.File.ReadAllLines(EmployeeCsv))
            {
                var parts = line.Split(',');
                if (parts.Length >= 2 && parts[1] == uid)
                    return parts[0];
            }
            return null;
        }

        private string DetermineStatus(string uid)
        {
            if (!System.IO.File.Exists(LogCsv)) return "出勤";
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            var logs = System.IO.File.ReadAllLines(LogCsv);
            foreach (var line in logs)
            {
                if (line.Contains(today) && line.Contains(uid) && line.Contains("出勤"))
                    return "退勤";
            }
            return "出勤";
        }

        private void SaveLog(string name, string uid, string status)
        {
            var logLine = $"{DateTime.Now},{uid},{name},{status}";
            System.IO.Directory.CreateDirectory("Data");
            System.IO.File.AppendAllText(LogCsv, logLine + Environment.NewLine, Encoding.UTF8);
        }
    }
}
