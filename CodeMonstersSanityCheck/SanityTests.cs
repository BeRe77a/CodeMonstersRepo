using RestSharp.Authenticators;
using RestSharp;
using Xunit;
using CodeMonstersSanityCheck.Models.Response;
using CodeMonstersSanityCheck.Models.Request;
using static CodeMonstersSanityCheck.Credentials.UserCredentials;
using System.Net;


namespace CodeMonstersSanityCheck
{
    [Collection("SanityTests")]
    public class SanityTests
    {

        [Theory]
        [InlineData(Username, Password, HttpStatusCode.OK)]
        [InlineData(WrongUsername, WrongPassword, HttpStatusCode.Unauthorized)]
        [InlineData(Username, WrongPassword, HttpStatusCode.Unauthorized)]
        [InlineData(WrongUsername, Password, HttpStatusCode.Unauthorized)]
        public async Task TransactionReturnsProperStatusCode(string username, string password, HttpStatusCode statusCode)
        {

            var options = new RestClientOptions("http://localhost:3001")
            {
                Authenticator = new HttpBasicAuthenticator(username, password),

            };
            var client = new RestClient(options);
            var request = new RestRequest("/payment_transactions", Method.Post);
            request.AddJsonBody(
                new Payment

                {
                    payment_transaction = new Payment_transaction()
                    {
                        card_number = "4200000000000000",
                        cvv = "123",
                        expiration_date = "06/2019",
                        amount = 500,
                        usage = "Coffeemaker",
                        transaction_type = "sale",
                        card_holder = "Panda Panda",
                        email = "panda@example.com",
                        address = "Panda Street, China"
                    }
                });
            var response = await client.ExecuteAsync(request);
            Assert.Equal(statusCode, response.StatusCode);
        }
        [Fact]
        public async Task VoidTransactionIsSuccessuful()
        {
            var options = new RestClientOptions("http://localhost:3001")
            {
                Authenticator = new HttpBasicAuthenticator(Username, Password),


            };
            var client = new RestClient(options);
            var request = new RestRequest("/payment_transactions", Method.Post);
            request.AddJsonBody(
                new Payment

                {
                    payment_transaction = new Payment_transaction()
                    {
                        card_number = "4200000000000000",
                        cvv = "123",
                        expiration_date = "06/2019",
                        amount = 500,
                        usage = "Coffeemaker",
                        transaction_type = "sale",
                        card_holder = "Panda Panda",
                        email = "panda@example.com",
                        address = "Panda Street, China"
                    }
                });
            var response = await client.PostAsync<TransactionResponse>(request);
            var voidRequest = new RestRequest("/payment_transactions", Method.Post);
            voidRequest.AddJsonBody(
                new Payment

                {
                    payment_transaction = new Payment_transaction()
                    {
                        reference_id = response.unique_id,
                        transaction_type = "void"
                    }
                });
            var voidResponse = await client.PostAsync(voidRequest);
            ;
            Assert.True(voidResponse.IsSuccessful);
        }

        
        [Theory]
        [InlineData("6fce1269a42c6c205a5cece92a4468e0")]
        [InlineData("6fce1269a42c6c205a5cece92a443333")]
        public async Task InvalidReferencVoidTransactionsReturnsProperStatusCode(string refId)
        {
            var options = new RestClientOptions("http://localhost:3001")
            {
                Authenticator = new HttpBasicAuthenticator(Username, Password),


            };
            var client = new RestClient(options);

            var request = new RestRequest("/payment_transactions", Method.Post);
            request.AddJsonBody(
                new Payment

                {
                    payment_transaction = new Payment_transaction()
                    {
                        reference_id = "6fce1269a42c6c205a5cece92a4468e0",
                        transaction_type = "void"
                    }
                });
            var response = await client.ExecuteAsync(request);
            ;
            Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);

        }


    }
}

