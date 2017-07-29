using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microstore
{
    public interface IPerson
    {
        string FirstName
        {
            get;
            set;
        }
        string LastName
        {
            get;
            set;
        }
        DateTime BirthDate
        {
            get;
            set;
        }
    }
    public class Employee : IPerson
    {
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public DateTime BirthDate
        {
            get;
            set;
        }

        public string Department
        {
            get;
            set;
        }
        public string JobTitle
        {
            get;
            set;
        }
    }
    public class PersonConverter : Newtonsoft.Json.Converters.CustomCreationConverter<IPerson>
    {
        //重写abstract class CustomCreationConverter<T>的Create方法  
        public override IPerson Create(Type objectType)
        {
            return new Employee();
        }
    }
    public partial class testjson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)  
            //    TestJson();  
        }
        public class Product
        {
            public string Name { get; set; }
            public string Expiry { get; set; }
            public Decimal Price { get; set; }
            public string[] Sizes { get; set; }
        }
        #region 序列化 -Newtonsoft.Json.JsonConvert.SerializeObject(,)-单个对象与泛集合（嵌套）序列化
        public string TestJsonSerialize()
        {
            Product product = new Product();
            product.Name = "Apple";
            product.Expiry = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd hh:mm:ss");
            product.Price = 3.99M;
            //product.Sizes = new string[] { "Small", "Medium", "Large" };  
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(product); //没有缩进输出  
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(product, Newtonsoft.Json.Formatting.Indented);
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(  
            //    product,   
            //    Newtonsoft.Json.Formatting.Indented,  
            //    new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore }  
            //);  
            return string.Format("<p>{0}</p>", json);
        }
        public string TestListJsonSerialize()
        {
            Product product = new Product();
            product.Name = "Apple";
            product.Expiry = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd hh:mm:ss");
            product.Price = 3.99M;
            product.Sizes = new string[] { "Small", "Medium", "Large" };

            List<Product> plist = new List<Product>();
            plist.Add(product);
            plist.Add(product);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(plist, Newtonsoft.Json.Formatting.Indented);
            return string.Format("<p>{0}</p>", json);
        }
        #endregion

        #region 反序列化
        public string TestJsonDeserialize()
        {
            string strjson = "{\"Name\":\"Apple\",\"Expiry\":\"2014-05-03 10:20:59\",\"Price\":3.99,\"Sizes\":[\"Small\",\"Medium\",\"Large\"]}";
            Product p = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(strjson);

            string template = @"<p><ul>  
                                    <li>{0}</li>  
                                    <li>{1}</li>  
                                    <li>{2}</li>  
                                    <li>{3}</li>  
                                </ul></p>";

            return string.Format(template, p.Name, p.Expiry, p.Price.ToString(), string.Join(",", p.Sizes));
        }
        public string TestListJsonDeserialize()
        {
            string strjson = "{\"Name\":\"Apple\",\"Expiry\":\"2014-05-03 10:20:59\",\"Price\":3.99,\"Sizes\":[\"Small\",\"Medium\",\"Large\"]}";
            List<Product> plist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(string.Format("[{0},{1}]", strjson, strjson));

            string template = @"<p><ul>  
                                    <li>{0}</li>  
                                    <li>{1}</li>  
                                    <li>{2}</li>  
                                    <li>{3}</li>  
                                </ul></p>";

            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            plist.ForEach(x =>
                strb.AppendLine(
                    string.Format(template, x.Name, x.Expiry, x.Price.ToString(), string.Join(",", x.Sizes))
                )
            );
            return strb.ToString();
        }
        #endregion

        #region 自定义反序列化
        public string TestListCustomDeserialize()
        {
            string strJson = "[ { \"FirstName\": \"Maurice\", \"LastName\": \"Moss\", \"BirthDate\": \"1981-03-08T00:00Z\", \"Department\": \"IT\", \"JobTitle\": \"Support\" }, { \"FirstName\": \"Jen\", \"LastName\": \"Barber\", \"BirthDate\": \"1985-12-10T00:00Z\", \"Department\": \"IT\", \"JobTitle\": \"Manager\" } ] ";
            List<IPerson> people = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IPerson>>(strJson, new PersonConverter());
            IPerson person = people[0];

            string template = @"<p><ul>  
                                    <li>当前List<IPerson>[x]对象类型：{0}</li>  
                                    <li>FirstName：{1}</li>  
                                    <li>LastName：{2}</li>  
                                    <li>BirthDate：{3}</li>  
                                    <li>Department：{4}</li>  
                                    <li>JobTitle：{5}</li>  
                                </ul></p>";

            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            people.ForEach(x =>
                strb.AppendLine(
                    string.Format(
                        template,
                        person.GetType().ToString(),
                        x.FirstName,
                        x.LastName,
                        x.BirthDate.ToString(),
                        ((Employee)x).Department,
                        ((Employee)x).JobTitle
                    )
                )
            );
            return strb.ToString();
        }
        #endregion

        #region 反序列化成Dictionary

        public string TestDeserialize2Dic()
        {
            //string json = @"{""key1"":""zhangsan"",""key2"":""lisi""}";  
            //string json = "{\"key1\":\"zhangsan\",\"key2\":\"lisi\"}";  
            string json = "{key1:\"zhangsan\",key2:\"lisi\"}";
            Dictionary<string, string> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            string template = @"<li>key：{0}，value：{1}</li>";
            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            strb.Append("Dictionary<string, string>长度" + dic.Count.ToString() + "<ul>");
            dic.AsQueryable().ToList().ForEach(x =>
            {
                strb.AppendLine(string.Format(template, x.Key, x.Value));
            });
            strb.Append("</ul>");
            return strb.ToString();
        }

        #endregion

        #region NullValueHandling特性
        public class Movie
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Classification { get; set; }
            public string Studio { get; set; }
            public DateTime? ReleaseDate { get; set; }
            public List<string> ReleaseCountries { get; set; }
        }
        /// <summary>  
        /// 完整序列化输出  
        /// </summary>  
        public string CommonSerialize()
        {
            Movie movie = new Movie();
            movie.Name = "Bad Boys III";
            movie.Description = "It's no Bad Boys";

            string included = Newtonsoft.Json.JsonConvert.SerializeObject(
                movie,
                Newtonsoft.Json.Formatting.Indented, //缩进  
                new Newtonsoft.Json.JsonSerializerSettings { }
            );

            return included;
        }
        /// <summary>  
        /// 忽略空（Null）对象输出  
        /// </summary>  
        /// <returns></returns>  
        public string IgnoredSerialize()
        {
            Movie movie = new Movie();
            movie.Name = "Bad Boys III";
            movie.Description = "It's no Bad Boys";

            string included = Newtonsoft.Json.JsonConvert.SerializeObject(
                movie,
                Newtonsoft.Json.Formatting.Indented, //缩进  
                new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore }
            );

            return included;
        }
        #endregion        

        #region DefaultValueHandling默认值
        public class Invoice
        {
            public string Company { get; set; }
            public decimal Amount { get; set; }

            // false is default value of bool  
            public bool Paid { get; set; }
            // null is default value of nullable  
            public DateTime? PaidDate { get; set; }

            // customize default values  
            [System.ComponentModel.DefaultValue(30)]
            public int FollowUpDays { get; set; }

            [System.ComponentModel.DefaultValue("")]
            public string FollowUpEmailAddress { get; set; }
        }
        public void GG()
        {
            Invoice invoice = new Invoice
            {
                Company = "Acme Ltd.",
                Amount = 50.0m,
                Paid = false,
                FollowUpDays = 30,
                FollowUpEmailAddress = string.Empty,
                PaidDate = null
            };

            string included = Newtonsoft.Json.JsonConvert.SerializeObject(
                invoice,
                Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings { }
            );
            // {  
            //   "Company": "Acme Ltd.",  
            //   "Amount": 50.0,  
            //   "Paid": false,  
            //   "PaidDate": null,  
            //   "FollowUpDays": 30,  
            //   "FollowUpEmailAddress": ""  
            // }  

            string ignored = Newtonsoft.Json.JsonConvert.SerializeObject(
                invoice,
                Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings { DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore }
            );
            // {  
            //   "Company": "Acme Ltd.",  
            //   "Amount": 50.0  
            // }  
        }
        #endregion

        #region JsonIgnoreAttribute and DataMemberAttribute 特性

        public string OutIncluded()
        {
            Car car = new Car
            {
                Model = "zhangsan",
                Year = DateTime.Now,
                Features = new List<string> { "aaaa", "bbbb", "cccc" },
                LastModified = DateTime.Now.AddDays(5)
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(car, Newtonsoft.Json.Formatting.Indented);
        }
        public string OutIncluded2()
        {
            Computer com = new Computer
            {
                Name = "zhangsan",
                SalePrice = 3999m,
                Manufacture = "red",
                StockCount = 5,
                WholeSalePrice = 34m,
                NextShipmentDate = DateTime.Now.AddDays(5)
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(com, Newtonsoft.Json.Formatting.Indented);
        }

        public class Car
        {
            // included in JSON  
            public string Model { get; set; }
            public DateTime Year { get; set; }
            public List<string> Features { get; set; }

            // ignored  
            [Newtonsoft.Json.JsonIgnore]
            public DateTime LastModified { get; set; }
        }

        //在nt3.5中需要添加System.Runtime.Serialization.dll引用  
        [System.Runtime.Serialization.DataContract]
        public class Computer
        {
            // included in JSON  
            [System.Runtime.Serialization.DataMember]
            public string Name { get; set; }
            [System.Runtime.Serialization.DataMember]
            public decimal SalePrice { get; set; }

            // ignored  
            public string Manufacture { get; set; }
            public int StockCount { get; set; }
            public decimal WholeSalePrice { get; set; }
            public DateTime NextShipmentDate { get; set; }
        }

        #endregion

        #region IContractResolver特性
        public class Book
        {
            public string BookName { get; set; }
            public decimal BookPrice { get; set; }
            public string AuthorName { get; set; }
            public int AuthorAge { get; set; }
            public string AuthorCountry { get; set; }
        }
        public void KK()
        {
            Book book = new Book
            {
                BookName = "The Gathering Storm",
                BookPrice = 16.19m,
                AuthorName = "Brandon Sanderson",
                AuthorAge = 34,
                AuthorCountry = "United States of America"
            };
            string startingWithA = Newtonsoft.Json.JsonConvert.SerializeObject(
                book, Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new DynamicContractResolver('A') }
            );
            // {  
            //   "AuthorName": "Brandon Sanderson",  
            //   "AuthorAge": 34,  
            //   "AuthorCountry": "United States of America"  
            // }  

            string startingWithB = Newtonsoft.Json.JsonConvert.SerializeObject(
                book,
                Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new DynamicContractResolver('B') }
            );
            // {  
            //   "BookName": "The Gathering Storm",  
            //   "BookPrice": 16.19  
            // }  
        }
        public class DynamicContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            private readonly char _startingWithChar;

            public DynamicContractResolver(char startingWithChar)
            {
                _startingWithChar = startingWithChar;
            }

            protected override IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
            {
                IList<Newtonsoft.Json.Serialization.JsonProperty> properties = base.CreateProperties(type, memberSerialization);

                // only serializer properties that start with the specified character  
                properties =
                    properties.Where(p => p.PropertyName.StartsWith(_startingWithChar.ToString())).ToList();

                return properties;
            }
        }

        #endregion

        #region Serializing Partial JSON Fragment Example
        public class SearchResult
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string Url { get; set; }
        }

        public string SerializingJsonFragment()
        {
            #region
            string googleSearchText = @"{  
            'responseData': {  
                'results': [{  
                    'GsearchResultClass': 'GwebSearch',  
                    'unescapedUrl': 'http://en.wikipedia.org/wiki/Paris_Hilton',  
                    'url': 'http://en.wikipedia.org/wiki/Paris_Hilton',  
                    'visibleUrl': 'en.wikipedia.org',  
                    'cacheUrl': 'http://www.google.com/search?q=cache:TwrPfhd22hYJ:en.wikipedia.org',  
                    'title': '<b>Paris Hilton</b> - Wikipedia, the free encyclopedia',  
                    'titleNoFormatting': 'Paris Hilton - Wikipedia, the free encyclopedia',  
                    'content': '[1] In 2006, she released her debut album...'  
                },  
                {  
                    'GsearchResultClass': 'GwebSearch',  
                    'unescapedUrl': 'http://www.imdb.com/name/nm0385296/',  
                    'url': 'http://www.imdb.com/name/nm0385296/',  
                    'visibleUrl': 'www.imdb.com',  
                    'cacheUrl': 'http://www.google.com/search?q=cache:1i34KkqnsooJ:www.imdb.com',  
                    'title': '<b>Paris Hilton</b>',  
                    'titleNoFormatting': 'Paris Hilton',  
                    'content': 'Self: Zoolander. Socialite <b>Paris Hilton</b>...'  
                }],  
                'cursor': {  
                    'pages': [{  
                        'start': '0',  
                        'label': 1  
                    },  
                    {  
                        'start': '4',  
                        'label': 2  
                    },  
                    {  
                        'start': '8',  
                        'label': 3  
                    },  
                    {  
                        'start': '12',  
                        'label': 4  
                    }],  
                    'estimatedResultCount': '59600000',  
                    'currentPageIndex': 0,  
                    'moreResultsUrl': 'http://www.google.com/search?oe=utf8&ie=utf8...'  
                }  
            },  
            'responseDetails': null,  
            'responseStatus': 200  
        }";
            #endregion

            Newtonsoft.Json.Linq.JObject googleSearch = Newtonsoft.Json.Linq.JObject.Parse(googleSearchText);
            // get JSON result objects into a list  
            List<Newtonsoft.Json.Linq.JToken> listJToken = googleSearch["responseData"]["results"].Children().ToList();
            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            string template = @"<ul>  
                                <li>Title:{0}</li>  
                                <li>Content: {1}</li>  
                                <li>Url:{2}</li>  
                            </ul>";
            listJToken.ForEach(x =>
            {
                // serialize JSON results into .NET objects  
                SearchResult searchResult = Newtonsoft.Json.JsonConvert.DeserializeObject<SearchResult>(x.ToString());
                strb.AppendLine(string.Format(template, searchResult.Title, searchResult.Content, searchResult.Url));
            });
            return strb.ToString();
        }

        #endregion

        #region ShouldSerialize
        public class CC
        {
            public string Name { get; set; }
            public CC Manager { get; set; }

            //http://msdn.microsoft.com/en-us/library/53b8022e.aspx  
            public bool ShouldSerializeManager()
            {
                // don't serialize the Manager property if an employee is their own manager  
                return (Manager != this);
            }
        }
        public string ShouldSerializeTest()
        {
            //create Employee mike  
            CC mike = new CC();
            mike.Name = "Mike Manager";

            //create Employee joe  
            CC joe = new CC();
            joe.Name = "Joe Employee";
            joe.Manager = mike; //set joe'Manager = mike  

            // mike is his own manager  
            // ShouldSerialize will skip this property  
            mike.Manager = mike;
            return Newtonsoft.Json.JsonConvert.SerializeObject(new[] { joe, mike }, Newtonsoft.Json.Formatting.Indented);
        }
        #endregion
        //驼峰结构输出（小写打头，后面单词大写）  
        public string JJJ()
        {
            microstore.testjson.Product product = new microstore.testjson.Product
            {
                Name = "Widget",
                Expiry = DateTime.Now.ToString(),
                Price = 9.99m,
                Sizes = new[] { "Small", "Medium", "Large" }
            };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(
                product,
                Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }
            );
            return json;

            //{  
            //  "name": "Widget",  
            //  "expiryDate": "2010-12-20T18:01Z",  
            //  "price": 9.99,  
            //  "sizes": [  
            //    "Small",  
            //    "Medium",  
            //    "Large"  
            //  ]  
            //}  
        }
        #region ITraceWriter
        public class Staff
        {
            public string Name { get; set; }
            public List<string> Roles { get; set; }
            public DateTime StartDate { get; set; }
        }
        public void KKKK()
        {
            Staff staff = new Staff();
            staff.Name = "Arnie Admin";
            staff.Roles = new List<string> { "Administrator" };
            staff.StartDate = new DateTime(2000, 12, 12, 12, 12, 12, DateTimeKind.Utc);

            Newtonsoft.Json.Serialization.ITraceWriter traceWriter = new Newtonsoft.Json.Serialization.MemoryTraceWriter();
            Newtonsoft.Json.JsonConvert.SerializeObject(
                staff,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    TraceWriter = traceWriter,
                    Converters = { new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter() }
                }
            );

            Console.WriteLine(traceWriter);
            // 2012-11-11T12:08:42.761 Info Started serializing Newtonsoft.Json.Tests.Serialization.Staff. Path ''.  
            // 2012-11-11T12:08:42.785 Info Started serializing System.DateTime with converter Newtonsoft.Json.Converters.JavaScriptDateTimeConverter. Path 'StartDate'.  
            // 2012-11-11T12:08:42.791 Info Finished serializing System.DateTime with converter Newtonsoft.Json.Converters.JavaScriptDateTimeConverter. Path 'StartDate'.  
            // 2012-11-11T12:08:42.797 Info Started serializing System.Collections.Generic.List`1[System.String]. Path 'Roles'.  
            // 2012-11-11T12:08:42.798 Info Finished serializing System.Collections.Generic.List`1[System.String]. Path 'Roles'.  
            // 2012-11-11T12:08:42.799 Info Finished serializing Newtonsoft.Json.Tests.Serialization.Staff. Path ''.  
            // 2013-05-18T21:38:11.255 Verbose Serialized JSON:   
            // {  
            //   "Name": "Arnie Admin",  
            //   "StartDate": new Date(  
            //     976623132000  
            //   ),  
            //   "Roles": [  
            //     "Administrator"  
            //   ]  
            // }  
        }
        #endregion
        public string TestReadJsonFromFile()
        {
            Linq2Json l2j = new Linq2Json();
            Newtonsoft.Json.Linq.JObject jarray = l2j.GetJObject4();
            return jarray.ToString();
        } 
        //...  
    }  
    public class Linq2Json
    {
        #region GetJObject

        //Parsing a JSON Object from text   
        public Newtonsoft.Json.Linq.JObject GetJObject()
        {
            string json = @"{  
                              CPU: 'Intel',  
                              Drives: [  
                                'DVD read/writer',  
                                '500 gigabyte hard drive'  
                              ]  
                            }";
            Newtonsoft.Json.Linq.JObject jobject = Newtonsoft.Json.Linq.JObject.Parse(json);
            return jobject;
        }

        /*  
         * //example:=> 
         *  
            Linq2Json l2j = new Linq2Json(); 
            Newtonsoft.Json.Linq.JObject jobject = l2j.GetJObject2(Server.MapPath("json/Person.json")); 
            //return Newtonsoft.Json.JsonConvert.SerializeObject(jobject, Newtonsoft.Json.Formatting.Indented); 
            return jobject.ToString(); 
         */
        //Loading JSON from a file  
        public Newtonsoft.Json.Linq.JObject GetJObject2(string jsonPath)
        {
            using (System.IO.StreamReader reader = System.IO.File.OpenText(jsonPath))
            {
                Newtonsoft.Json.Linq.JObject jobject = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.Linq.JToken.ReadFrom(new Newtonsoft.Json.JsonTextReader(reader));
                return jobject;
            }
        }

        //Creating JObject  
        public Newtonsoft.Json.Linq.JObject GetJObject3()
        {
            List<Post> posts = GetPosts();
            Newtonsoft.Json.Linq.JObject jobject = Newtonsoft.Json.Linq.JObject.FromObject(new
            {
                channel = new
                {
                    title = "James Newton-King",
                    link = "http://james.newtonking.com",
                    description = "James Newton-King's blog.",
                    item =
                        from p in posts
                        orderby p.Title
                        select new
                        {
                            title = p.Title,
                            description = p.Description,
                            link = p.Link,
                            category = p.Category
                        }
                }
            });

            return jobject;
        }
        /* 
            { 
                "channel": { 
                    "title": "James Newton-King", 
                    "link": "http://james.newtonking.com", 
                    "description": "James Newton-King's blog.", 
                    "item": [{ 
                        "title": "jewron", 
                        "description": "4546fds", 
                        "link": "http://www.baidu.com", 
                        "category": "jhgj" 
                    }, 
                    { 
                        "title": "jofdsn", 
                        "description": "mdsfan", 
                        "link": "http://www.baidu.com", 
                        "category": "6546" 
                    }, 
                    { 
                        "title": "jokjn", 
                        "description": "m3214an", 
                        "link": "http://www.baidu.com", 
                        "category": "hg425" 
                    }, 
                    { 
                        "title": "jon", 
                        "description": "man", 
                        "link": "http://www.baidu.com", 
                        "category": "goodman" 
                    }] 
                } 
            } 
         */
        //Creating JObject  
        public Newtonsoft.Json.Linq.JObject GetJObject4()
        {
            List<Post> posts = GetPosts();
            Newtonsoft.Json.Linq.JObject rss = new Newtonsoft.Json.Linq.JObject(
                    new Newtonsoft.Json.Linq.JProperty("channel",
                        new Newtonsoft.Json.Linq.JObject(
                            new Newtonsoft.Json.Linq.JProperty("title", "James Newton-King"),
                            new Newtonsoft.Json.Linq.JProperty("link", "http://james.newtonking.com"),
                            new Newtonsoft.Json.Linq.JProperty("description", "James Newton-King's blog."),
                            new Newtonsoft.Json.Linq.JProperty("item",
                                new Newtonsoft.Json.Linq.JArray(
                                    from p in posts
                                    orderby p.Title
                                    select new Newtonsoft.Json.Linq.JObject(
                                        new Newtonsoft.Json.Linq.JProperty("title", p.Title),
                                        new Newtonsoft.Json.Linq.JProperty("description", p.Description),
                                        new Newtonsoft.Json.Linq.JProperty("link", p.Link),
                                        new Newtonsoft.Json.Linq.JProperty("category",
                                            new Newtonsoft.Json.Linq.JArray(
                                                from c in p.Category
                                                select new Newtonsoft.Json.Linq.JValue(c)
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                );

            return rss;
        }
        /* 
            { 
                "channel": { 
                    "title": "James Newton-King", 
                    "link": "http://james.newtonking.com", 
                    "description": "James Newton-King's blog.", 
                    "item": [{ 
                        "title": "jewron", 
                        "description": "4546fds", 
                        "link": "http://www.baidu.com", 
                        "category": ["j", "h", "g", "j"] 
                    }, 
                    { 
                        "title": "jofdsn", 
                        "description": "mdsfan", 
                        "link": "http://www.baidu.com", 
                        "category": ["6", "5", "4", "6"] 
                    }, 
                    { 
                        "title": "jokjn", 
                        "description": "m3214an", 
                        "link": "http://www.baidu.com", 
                        "category": ["h", "g", "4", "2", "5"] 
                    }, 
                    { 
                        "title": "jon", 
                        "description": "man", 
                        "link": "http://www.baidu.com", 
                        "category": ["g", "o", "o", "d", "m", "a", "n"] 
                    }] 
                } 
            } 
         */

        public class Post
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Link { get; set; }
            public string Category { get; set; }
        }
        private List<Post> GetPosts()
        {
            List<Post> listp = new List<Post>()  
            {  
                new Post{Title="jon",Description="man",Link="http://www.baidu.com",Category="goodman"},  
                new Post{Title="jofdsn",Description="mdsfan",Link="http://www.baidu.com",Category="6546"},  
                new Post{Title="jewron",Description="4546fds",Link="http://www.baidu.com",Category="jhgj"},  
                new Post{Title="jokjn",Description="m3214an",Link="http://www.baidu.com",Category="hg425"}  
            };
            return listp;
        }

        #endregion

        #region GetJArray
        /* 
         * //example:=> 
         *  
            Linq2Json l2j = new Linq2Json(); 
            Newtonsoft.Json.Linq.JArray jarray = l2j.GetJArray(); 
            return Newtonsoft.Json.JsonConvert.SerializeObject(jarray, Newtonsoft.Json.Formatting.Indented); 
            //return jarray.ToString(); 
         */
        //Parsing a JSON Array from text   
        public Newtonsoft.Json.Linq.JArray GetJArray()
        {
            string json = @"[  
                              'Small',  
                              'Medium',  
                              'Large'  
                            ]";

            Newtonsoft.Json.Linq.JArray jarray = Newtonsoft.Json.Linq.JArray.Parse(json);
            return jarray;
        }

        //Creating JArray  
        public Newtonsoft.Json.Linq.JArray GetJArray2()
        {
            Newtonsoft.Json.Linq.JArray array = new Newtonsoft.Json.Linq.JArray();
            Newtonsoft.Json.Linq.JValue text = new Newtonsoft.Json.Linq.JValue("Manual text");
            Newtonsoft.Json.Linq.JValue date = new Newtonsoft.Json.Linq.JValue(new DateTime(2000, 5, 23));
            //add to JArray  
            array.Add(text);
            array.Add(date);

            return array;
        }

        #endregion

        //待续...  

    }   
}
