using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;

namespace SOAPproject
{
    /// <summary>
    /// Summary description for ChatService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "SOAPChatroom")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ChatService1 : System.Web.Services.WebService
    {
        static protected ArrayList users = new ArrayList();
        static protected ArrayList messages = new ArrayList();
        
        public ChatService1() { 
        }

        // add user to the chatroom
        [WebMethod]
        public void AddUser(string strUser)
        {
            users.Add(strUser);
            Console.WriteLine();
        }

        [WebMethod]
        public string GetUsers()
        {
            string strUser = "";
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine(strUser);
                strUser = strUser + users[i].ToString() + "|";
            }
            return strUser;
        }

        [WebMethod]
        public void RemoveUser(string strUser)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].ToString() == strUser)
                    users.RemoveAt(i);
            }
        }

        [WebMethod]
        public void SendMessage(string strFromUser, string strToUser, string strMess)
        {
            messages.Add(strToUser + ":" + strFromUser + ":" + strMess);
            //ReceiveMessage(strToUser);
        }

        [WebMethod]
        public string ReceiveMessage(string strUser)
        {
            string strMess = string.Empty;
            for (int i = 0; i < messages.Count; i++)
            {
                string[] strTo = messages[i].ToString().Split(':');
                if (strTo[0].ToString() == strUser)
                {
                    for (int j = 1; j < strTo.Length; j++)
                    {
                        strMess = strMess + strTo[j] + ":";
                    }
                    messages.RemoveAt(i);
                    break;
                }
            }
            return strMess;
        }

        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}
        public static void Main(string[] args) {
            ChatService1 chat = new ChatService1();
            //Console.WriteLine();
        }
    }
}
