using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yuruisoft.RS.Model;

namespace TxtReaderForDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Administrator\Desktop\小词典数据库文件\dicData\汉英分组\z.txt";

            string Temp = System.IO.File.ReadAllText(path, Encoding.Default);

            string[] TempArr = Temp.Replace("* * *\r\n\r\n\r\n\r\n\r\n\r\n* * *", "* * *").Replace("* * *\r\n\r\n\r\n\r\n用法说明", "用法说明").Replace("\r\n\r\n\r\n\r\n\r\n\r\n", "\r\n\r\n\r\n\r\n").Replace("* * *", "#").Split('#');
            
            string Before = null;
            string Middle = null;
            string End = null;
            string Or = null;
            DbContext db = new WxDicEntities();



            string TempString = null;
            WxDic_CnToEn_z Curent = new WxDic_CnToEn_z();//字典插入
            for (var i = 0; i < TempArr.Length; i++)
            {
                Before = TempArr[i].Substring(("\r\n\r\n\r\n\r\n").Length);
                Middle = Before.Substring(0, Before.IndexOf("\r\n\r\n"));
                Or = "\r\n\r\n\r\n\r\n" + Middle + "\r\n\r\n";
                End = TempArr[i].Substring(Or.Length);
                Curent.WKey = Middle;
                TempString = TempString + "#" + Middle;
                Curent.WValue = End;
                db.Set<WxDic_CnToEn_z>().Add(Curent);
                db.SaveChanges();
            }

            //导出为TXT
            FileStream fileStream = new FileStream(@"C:\Users\Administrator\Desktop\小词典数据库文件\dicData\汉英分组\zToWxclient.txt", FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
            streamWriter.Write(TempString);
            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();





            //WxDic_SearchKey_EnToCn_Z CurentS = new WxDic_SearchKey_EnToCn_Z();//搜索插入

            //for (var i = 0; i < TempArr.Length; i++)
            //{
            //    Before = TempArr[i].Substring(("\r\n\r\n\r\n\r\n").Length);
            //    Middle = Before.Substring(0, Before.IndexOf("\r\n\r\n"));
            //    Or = "\r\n\r\n\r\n\r\n" + Middle + "\r\n\r\n";
            //    End = TempArr[i].Substring(Or.Length);

            //    CurentS.SKey = Middle;

            //    db.Set<WxDic_SearchKey_EnToCn_Z>().Add(CurentS);

            //    db.SaveChanges();
            //}



            //Console.ReadKey();
            
            //string Test = "\r\n\r\n\r\n\r\nA, a\r\n\r\n";

            //string T = Test.Substring(("\r\n\r\n\r\n\r\n").Length);

            //string Mid = T.Substring(0, T.IndexOf("\r\n\r\n"));

            //string Orig = "\r\n\r\n\r\n\r\n" + Mid + "\r\n\r\n";


            //正则表达式
            //string input = "/fdfdf/fff/fdfdf/";
            //string pattern = @"^\/.*\/$";
            //foreach (Match match in Regex.Matches(input, pattern))
            //    Console.WriteLine(match.Value);
            //Console.ReadKey();





            //ArrayList TempDic = new ArrayList(Temp.Replace("\r\n","#").Split('#'));

            //检查遍历
            //var a = 1;
            //for (var i = 1; i <= TempDic.Count; i = i + 2)
            //{
            //    Console.WriteLine("第{1}条：{0}\r\n", TempDic[i],a);
            //    Console.ReadKey();
            //    a++;
            //}


            //DbContext db = new WxDicEntities();
            //WxDic_Dictionary Curent = new WxDic_Dictionary();
            //for (var n = 0; n < TempDic.Count; n = n + 2)
            //{
            //    Curent.WKey = TempDic[n].ToString();
            //    Curent.WValue = TempDic[n + 1].ToString();
            //    db.Set<WxDic_Dictionary>().Add(Curent);
            //    db.SaveChanges();
            //}

            //Curent.WKey = "Test";
            //Curent.WValue = Temp01;
            //db.Set<WxDic_Dictionary>().Add(Curent);
            //db.SaveChanges();
        }


    }
}
