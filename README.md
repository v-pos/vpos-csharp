# vPOS C#
[![Test](https://github.com/v-pos/vpos-csharp/actions/workflows/test.yml/badge.svg)](https://github.com/v-pos/vpos-csharp/actions/workflows/test.yml)
[![](https://img.shields.io/badge/vPOS-OpenSource-blue.svg)](https://www.vpos.ao)

This C# Package helps you easily interact with the vPos API
Allowing merchants apps/services to request a payment from a client through his/her mobile phone number.

The service currently works for solutions listed below:

 - EMIS GPO (Multicaixa Express)

Want to know more about how we are empowering merchants in Angola? See our [website](https://vpos.ao)

## Features
- Simple interface
- Uniform plain old objects are returned by all official libraries, so you don't have
to serialize/deserialize the JSON returned by our API.

## Documentation
Does interacting directly with our API service sound better to you? 
See our documentation on [developer.vpos.ao](https://developer.vpos.ao)

## Configuration
This C# library requires you set up the following environment variables on your machine before
interacting with the API using this library:

| Variable | Description | Required |
| --- | --- | --- |
| `GPO_POS_ID` | The Point of Sale ID provided by EMIS | true |
| `GPO_SUPERVISOR_CARD` | The Supervisor card ID provided by EMIS | true |
| `MERCHANT_VPOS_TOKEN` | The API token provided by vPOS | true |
| `PAYMENT_CALLBACK_URL` | The URL that will handle payment notifications | false |
| `REFUND_CALLBACK_URL` | The URL that will handle refund notifications | false |
| `VPOS_ENVIRONMENT` | The vPOS environment, leave empty for `sandbox` mode and use `"PRD"` for `production`.  | false |

Don't have this information? [Talk to us](suporte@vpos.ao)

Create an instance of `Vpos` (make sure to define the environment variables above to) to interact with our API. 
The constructor will be responsible for acquiring the tokens defined above to interact with the API.

## Use

### Create an instance
Create an instance of `Vpos` (make sure to define the environment variables above to) to interact with our API. 
The constructor will be responsible for acquiring the tokens defined above to interact with the API. 
```c#
Vpos merchant = new Vpos();
```

### Get all Transactions
This endpoint retrieves all transactions.

```c#
TransactionsResponse transactionsResponse = merchant.GetTransactions();
```

### Get a specific Transaction
Retrieves a transaction given a valid transaction ID.
```c#
TransactionResponse transactionResponse = merchant.GetTransaction("1jHXEbRTIbbwaoJ6w86");
```

| Argument | Description | Type |
| --- | --- | --- |
| `id` | An existing Transaction ID | `string`

### New Payment Transaction
Creates a new payment transaction given a valid mobile number associated with a `MULTICAIXA` account
and a valid amount.

```c#
LocationResponse locationResponse = merchant.NewPayment("900111222", "123.45");
```

| Argument | Description | Type |
| --- | --- | --- |
| `mobile` | The mobile number of the client who will pay | `string`
| `amount` | The amount the client should pay, eg. "259.99", "259000.00" | `string`

### Request Refund
Given an existing `parentTransactionId`, request a refund.

```c#
LocationResponse locationResponse = merchant.NewRefund("1jHXEbRTIbbwaoJ6w86");
```

| Argument | Description | Type |
| --- | --- | --- |
| `parentTransactionId` | The ID of transaction you wish to refund | `string`

### Poll Transaction Status
Poll the status of a transaction given a valid `requestId`.

Note: The `requestId` in this context is essentially the `transactionId` of an existing request. 

```c#
BaseResponse response = merchant.GetRequest("1jHXEbRTIbbwaoJ6w86");
if (response.status == 200)
{
    RequestResponse requestResponse = (RequestResponse)response;
}
else if(response.status == 303)
{
    LocationResponse locationresponse = (LocationResponse)response;
}
```

| Argument | Description | Type |
| --- | --- | --- |
| `requestId` | The ID of transaction you wish to poll | `string`


### Have any doubts?
In case of any doubts, bugs, or the like, please leave an issue. We would love to help.

License
----------------

The library is available as open source under the terms of the [MIT License](http://opensource.org/licenses/MIT).
