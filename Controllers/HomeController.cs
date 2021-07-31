using IAccessSearch.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using X.PagedList;

namespace IAccessSearch.Controllers
{
    public class HomeController : Controller
    {
        public IPagedList<TSearchStringView> SearchResults; //Public to hold the data for one search and move through pages
        public static List<TSearchStringView> SearchResultsTemp = new List<TSearchStringView>();
        public int pageSize = 10;
        public int pageIndex = 1;

        private SearchStringContext _context;

        public HomeController(SearchStringContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateData()
        {
            ViewData["CreateMsg"] = "";
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateData(string? txtNoOfRecs)
        {
            string tmStart = System.DateTime.Now.ToString();
            int noOfRows = 10000;

            for (int iCount = 0; iCount < noOfRows; iCount++)
            {
                Random rand = new Random();
                //Random RNG = new Random();

                //Generate Key, 5 parts 3AE69BBF-52FB-42AF-8310-DFAAE0C6296A
                const string keyChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                string key1 = new string(Enumerable.Repeat(keyChars, 8).Select(s => s[rand.Next(s.Length)]).ToArray());
                string key2 = new string(Enumerable.Repeat(keyChars, 4).Select(s => s[rand.Next(s.Length)]).ToArray());
                string key3 = new string(Enumerable.Repeat(keyChars, 4).Select(s => s[rand.Next(s.Length)]).ToArray());
                string key4 = new string(Enumerable.Repeat(keyChars, 4).Select(s => s[rand.Next(s.Length)]).ToArray());
                string key5 = new string(Enumerable.Repeat(keyChars, 12).Select(s => s[rand.Next(s.Length)]).ToArray());
                string keyValue = key1 + "-" + key2 + "-" + key3 + "-" + key4 + "-" + key5;


                //get a random number for length of string

                string randomContent = "";
                int randomLength = rand.Next(1000, 2000); //Generate a number from 1000 to 2000

                for (var i = 0; i < randomLength; i++)
                {
                    randomContent += ((char)(rand.Next(1, 26) + 64)).ToString().ToLower();
                }

                TSearchString searchContent = new TSearchString();
                searchContent.ID = keyValue;
                searchContent.Content = randomContent.ToString();
                _context.TSearchStrings.Add(searchContent);
                if(iCount % 2000 == 0) //bulk commit for 10000 rows to run faster
                    _context.SaveChanges();

            }
            _context.SaveChanges(); //Commit the rest of the data
            ViewData["CreateMsg"] = "Insert Data started at " + tmStart +
                                    " <br> Successfully inserted " + noOfRows + " rows " +
                                    "<br> Completed at " + System.DateTime.Now.ToString();

            return View();
        }

        public IActionResult SearchData(int? page, string? search)
        {
            if (page != null)
                pageIndex = Convert.ToInt32(page);

            ViewData["SearchString"] = search;
            //create dummy null objectes for view to show empty page/no rows
            return View(SearchResultsTemp.ToPagedList(pageIndex, pageSize));

        }

        [HttpPost]
        public IActionResult SearchData(string search, int? pageNumber)
        {
            //declaration
           
            ViewData["SearchString"] = search;
            //for first request (page number null), get the data
            if (pageNumber == null && search != null)
            {
                pageIndex = pageNumber.HasValue ? Convert.ToInt32(pageNumber) : 1;
                SearchResults = null; // new IPagedList<TSearchStringView>();
                SearchResultsTemp.Clear();

                var vTSeatchContent = _context.TSearchStrings.Where(f => f.Content.Contains(search)).ToList();
                

                if (vTSeatchContent != null)
                    for (int i = 0; i <= vTSeatchContent.Count() - 1; i++)
                    {
                        int freq = Regex.Matches(vTSeatchContent[i].Content, search).Count;
                        TSearchStringView SearchResult = new TSearchStringView();

                        SearchResult.ID = vTSeatchContent[i].ID;
                        SearchResult.Content = vTSeatchContent[i].Content;
                        SearchResult.MatchNo = freq;
                        SearchResultsTemp.Add(SearchResult);
                    }
                //SearchResults = SearchResultsTemp.ToPagedList(pageIndex, pageSize);
                return View(SearchResultsTemp.ToPagedList(pageIndex, pageSize));
            }
            else
            {
                TSearchStringView SearchResult = new TSearchStringView();
                SearchResultsTemp = new List<TSearchStringView>();
                SearchResult.ID = "";
                SearchResult.Content = "";
                SearchResult.MatchNo = null;
                SearchResultsTemp.Add(SearchResult);
                return View(SearchResultsTemp.ToPagedList(1, 1));                
            }            

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
