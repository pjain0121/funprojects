using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace ProjectRest
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        private static ArrayList users = new ArrayList();
        private static ArrayList messages = new ArrayList();
        public Service1() {
            users.Add("Preet");
            users.Add("Robin");
            users.Add("Luffy");
            users.Add("Raj");
        }
        // Add more operations here and mark them with [OperationContract]
        [WebGet(UriTemplate = "/getUsers")]
        public String GetUsers() {
            string strUser = string.Empty;
            for (int i = 0; i < users.Count; i++)
            {
                strUser = strUser + users[i].ToString() + "|";
            }
            return strUser;
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/addUser/{user}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public void AddUser(String user) {
            users.Add(user);
        }

        [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json,
    UriTemplate = "/removeUser/{userName}", ResponseFormat = WebMessageFormat.Json,
    BodyStyle = WebMessageBodyStyle.Wrapped)]

        public void RemoveUser(String userName)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].ToString() == userName)
                    users.RemoveAt(i);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
    UriTemplate = "/sendMessage", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        public void sendMessage(String strFromUser, String strToUser,String messg)
        {
            messages.Add(strToUser + ":" + strFromUser + ":" + messg);
        }

        [WebGet(UriTemplate = "/getMessages/{userName}")]
        public String RecieveMessages(String userName)
        {
    string strMess = string.Empty;
    for (int i = 0; i < messages.Count; i++)
    {
        string[] strTo = messages[i].ToString().Split(':');
        if (strTo[0].ToString() == userName)
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


    }
}
