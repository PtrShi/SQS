using System;
//using System.Data;
//using System.Collections.Generic;
//using System.Text;
//using Amazon.Runtime;
//using Amazon.SQS;
using System.Data.SqlTypes;
//using System.Threading.Tasks;

public class CLRSQS
{
    public static void SendMessageToAWSSQS(SqlString accessKey, SqlString secretKey, SqlString QueueUrl, SqlString MessageBody,
        SqlString MessageGroupId, out SqlInt32 Result, out SqlString ResultMessage)
    {
        try
        {
            var awsCred = new Amazon.Runtime.BasicAWSCredentials(accessKey.ToString (), secretKey.ToString ());
            var sqsClient = new Amazon.SQS.AmazonSQSClient(awsCred, Amazon.RegionEndpoint.EUCentral1);
            var msg = new Amazon.SQS.Model.SendMessageRequest
            {
                MessageDeduplicationId = Guid.NewGuid().ToString(),
                MessageGroupId = MessageGroupId.ToString (),
                MessageBody = MessageBody.ToString(),
                QueueUrl = QueueUrl.ToString ()
            };
            var response = sqsClient.SendMessage(msg);
            if (response.HttpStatusCode.ToString () != "OK")
            {
                Result = -1;
                ResultMessage = "MessageId="+response.MessageId.ToString()+"; response="+response.HttpStatusCode.ToString();
            }
            else
            {
                Result = 0;
                ResultMessage = "OK";
            }
        }
        catch (Exception ex)
        {
            Result = -1;
            ResultMessage = "Exception.Message=" + ex.Message;
        }
    }
}
