//using Lucene.Net.Analysis;
//using Lucene.Net.Analysis.PanGu;
//using PanGu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Common
{
  public  class WebCommon
    {
      /// <summary>
      /// 对字符串进行MD5运算
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static string Md5String(string str)
      {
         MD5 md5 = MD5.Create();
         byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
         byte[]md5Buffer= md5.ComputeHash(buffer);
         StringBuilder sb = new StringBuilder();
         foreach (byte b in md5Buffer)
         {
             sb.Append(b.ToString("x2"));
         }
         return sb.ToString();
      }
      ////对输入的搜索条件进行分词
      //public static string[] PanGuSplitWord(string str)
      //{
      //    List<string> list = new List<string>();
      //     Analyzer analyzer = new PanGuAnalyzer();
      //      TokenStream tokenStream = analyzer.TokenStream("", new StringReader(str));
      //      Lucene.Net.Analysis.Token token = null;
      //      while ((token = tokenStream.Next()) != null)
      //      {
      //          list.Add(token.TermText());
      //      }
      //      return list.ToArray();
      //}


      //// /创建HTMLFormatter,参数为高亮单词的前后缀
      //public static string CreateHightLight(string keywords, string Content)
      //{
      //    PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter =
      //     new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
      //    //创建Highlighter ，输入HTMLFormatter 和盘古分词对象Semgent
      //    PanGu.HighLight.Highlighter highlighter =
      //    new PanGu.HighLight.Highlighter(simpleHTMLFormatter,
      //    new Segment());
      //    //设置每个摘要段的字符数
      //    highlighter.FragmentSize = 150;
      //    //获取最匹配的摘要段
      //    return highlighter.GetBestFragment(keywords, Content);
      //}

    }
}
